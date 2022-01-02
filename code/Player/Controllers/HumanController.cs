using System.Collections.Generic;
using Sandbox;
using ssl.Modules.Selection;

namespace ssl.Player.Controllers;

public partial class HumanController : BasePlayerController
{
    private const float TOP_GROUND_DETECT = 0.1F;
    private const float BOTTOM_GROUND_DETECT = 2F;

    private const float BODY_HEIGHT = 72F;
    private const float EYE_HEIGHT = 64F;
    private const float BODY_GIRTH = 16F;

    private const float STOP_SPEED = 100F;

    private const float STEP_SIZE = 20F;

    private const float MAX_NON_JUMP_VELOCITY = 200F;
    private const float JUMP_FORCE = 300F;
    private const float AIR_SPEED = 30F;
    private const float AIR_ACCELERATION = 500F;

    private const float MIN_SPEED = 1F;

    private const float GROUND_ANGLE = 46F;
    private const float STICK_GROUND_START_MULTIPLIER = 2F;

    private const string JUMP_EVENT_NAME = "jump";

    private readonly Dictionary<MovementState, Speed> speeds = new()
    {
        { MovementState.Idle, new Speed { Acceleration = 0f, MaxSpeed = 0f } },
        { MovementState.Walk, new Speed { Acceleration = 500f, MaxSpeed = 70f } },
        { MovementState.Run, new Speed { Acceleration = 1500f, MaxSpeed = 150f } },
        { MovementState.Sprint, new Speed { Acceleration = 2000f, MaxSpeed = 300f } }
    };

    private readonly Unstuck unstuck;

    private Vector3 maxs;
    private Vector3 mins;

    private MovementState state = MovementState.Idle;

    public HumanController()
    {
        unstuck = new Unstuck(this);
    }

    public HumanController(SslPlayer sslPlayer) : this()
    {
        if (Host.IsServer) SslPlayer = sslPlayer;
    }


    [Net] private SslPlayer SslPlayer { get; set;  }
    public Vector3 GravityVector { get; set; } = Vector3.Down * 981F;
    public float CurrentSpeed => Velocity.Length;

    public bool IsGrounded => GroundEntity != null;
    public Surface GroundSurface { get; set; }
    public float SurfaceFriction { get; set; } = 4;

    public bool IsFrozen { get; set; }


    public override void Simulate()
    {
        if (!SslPlayer.RagdollHandler.IsRagdoll && !IsFrozen)
        {
            UpdateBBox();

            EyePosLocal = Vector3.Up * (EYE_HEIGHT * Pawn.Scale);
            EyeRot = Input.Rotation;

            //If the player is stuck, fix and stop
            if (unstuck.TestAndFix()) return;

            UpdateGroundEntity();

            ProcessInputs();

            if (IsGrounded)
            {
                if (Input.Pressed(InputButton.Jump))
                {
                    Jump();
                }
                else
                {
                    ApplyFriction(GroundSurface.Friction * SurfaceFriction);
                    AccelerateGroundMovement();
                    TryPlayerMoveWithStep();
                    StickToGround();
                }
            }
            else
            {
                ApplyGravity();
                Air();
                TryPlayerMove();
            }
        }
        else if (SslPlayer.RagdollHandler.CanStand)
        {
            bool wantExitRagdoll = Input.Pressed(InputButton.Jump) || Input.Forward + Input.Left > 0;

            if (wantExitRagdoll) SslPlayer.RagdollHandler.StopRagdoll();
        }
    }

    /// <summary>
    ///     Traces the current bbox and returns the result.
    ///     liftFeet will move the start position up by this amount, while keeping the top of the bbox at the same
    ///     position. This is good when tracing down because you won't be tracing through the ceiling above.
    /// </summary>
    public override TraceResult TraceBBox(Vector3 start, Vector3 end, float liftFeet = 0.0f)
    {
        return TraceBBox(start, end, mins, maxs, liftFeet);
    }

    /// <summary>
    ///     Moves the player in the direction of their velocity if there is no colliding objects.
    /// </summary>
    protected virtual void TryPlayerMove()
    {
        if (Velocity.Length <= MIN_SPEED)
        {
            Velocity = Vector3.Zero;
            return;
        }

        MoveHelper mover = GetMoveHelper();

        mover.TryMove(Time.Delta);

        Position = mover.Position;
        Velocity = mover.Velocity;
    }

    /// <summary>
    ///     Moves the player in the direction of their velocity and if there is colliding objects, checks if it can step.
    /// </summary>
    protected virtual void TryPlayerMoveWithStep()
    {
        if (Velocity.Length <= MIN_SPEED)
        {
            Velocity = Vector3.Zero;
            return;
        }

        MoveHelper mover = GetMoveHelper();

        mover.TryMoveWithStep(Time.Delta, STEP_SIZE);

        Position = mover.Position;
        Velocity = mover.Velocity;
    }

    /// <summary>
    ///     BoundingBox (collision box)
    /// </summary>
    protected virtual void UpdateBBox()
    {
        mins = new Vector3(-BODY_GIRTH, -BODY_GIRTH, 0) * Pawn.Scale;
        maxs = new Vector3(+BODY_GIRTH, +BODY_GIRTH, BODY_HEIGHT) * Pawn.Scale;
    }

    private MoveHelper GetMoveHelper()
    {
        MoveHelper mover = new(Position, Velocity);
        mover.Trace = mover.Trace.Size(mins, maxs).Ignore(Pawn);
        mover.MaxStandableAngle = GROUND_ANGLE;

        return mover;
    }

    private void ProcessInputs()
    {
        WishVelocity = new Vector3(Input.Forward, Input.Left, 0);

        if (IsGrounded)
        {
            Angles eyeRotationYaw = Rotation.Angles().WithPitch(0);
            WishVelocity = Rotation.From(eyeRotationYaw) * WishVelocity.ClampLength(1);
        }
        else
        {
            WishVelocity = EyeRot * WishVelocity.ClampLength(1);
        }

        if (Input.Down(InputButton.Run))
            state = MovementState.Sprint;
        else if (Input.Down(InputButton.Walk))
            state = MovementState.Walk;
        else if (WishVelocity.Length > 0)
            state = MovementState.Run;
        else
            state = MovementState.Idle;
    }

    /// <summary>
    ///     Applies the Ground Movement logic to accelerate the player
    /// </summary>
    private void AccelerateGroundMovement()
    {
        float acceleration = speeds[state].Acceleration;
        float speed = speeds[state].MaxSpeed;

        WishVelocity *= acceleration;

        Accelerate(WishVelocity, speed);
    }

    private void Jump()
    {
        if (GroundEntity is IDraggable draggable)
            if (((SslPlayer) Pawn).Dragger.Dragged == draggable)
                return;

        ClearGroundEntity();
        Velocity += Vector3.Up * JUMP_FORCE;
        AddEvent(JUMP_EVENT_NAME);
    }

    private void Air()
    {
        WishVelocity *= AIR_ACCELERATION;
        Accelerate(WishVelocity, AIR_SPEED);
    }

    /// <summary>
    ///     Try to keep a walking player on the ground when running down slopes
    /// </summary>
    private void StickToGround()
    {
        Vector3 start = Position + Vector3.Up * STICK_GROUND_START_MULTIPLIER;
        Vector3 end = Position + Vector3.Down * STEP_SIZE;

        // See how far up we can go without getting stuck
        TraceResult trace = TraceBBox(Position, start);
        start = trace.EndPos;

        // Now trace down from a known safe position
        trace = TraceBBox(start, end);

        if (trace.Fraction <= 0) return;

        if (trace.Fraction >= 1) return;

        if (trace.StartedSolid) return;

        if (Vector3.GetAngle(Vector3.Up, trace.Normal) > GROUND_ANGLE) return;

        Position = trace.EndPos;
    }

    /// <summary>
    ///     Adds the Gravity Vector if the player is not on ground
    /// </summary>
    private void ApplyGravity()
    {
        if (!IsGrounded)
            Velocity += GravityVector * Time.Delta;
        else
            Velocity = Velocity.WithZ(0);
    }

    /// <summary>
    ///     Apply a specific amount of friction
    /// </summary>
    /// <param name="frictionAmount">Friction to apply</param>
    private void ApplyFriction(float frictionAmount = 1.0f)
    {
        if (CurrentSpeed < 0.1f) return;

        float usedSpeed = CurrentSpeed < STOP_SPEED ? STOP_SPEED : CurrentSpeed;
        float droppedSpeed = usedSpeed * Time.Delta * frictionAmount;
        float newSpeed = CurrentSpeed - droppedSpeed;

        if (newSpeed < 0) newSpeed = 0;

        newSpeed /= CurrentSpeed;
        Velocity *= newSpeed;
    }

    /// <summary>
    ///     Updates the GroundEntity property and all the other related properties like ground normal, surface, etc.
    /// </summary>
    private void UpdateGroundEntity()
    {
        Vector3 startPos = Position + Vector3.Up * TOP_GROUND_DETECT;
        Vector3 endPos = Position - Vector3.Up * BOTTOM_GROUND_DETECT;

        TraceResult trace = TraceBBox(startPos, endPos, mins, maxs, 4.0F);

        if (trace.Hit && Velocity.z <= MAX_NON_JUMP_VELOCITY)
        {
            GroundNormal = trace.Normal;
            GroundEntity = trace.Entity;
            GroundSurface = trace.Surface;
            BaseVelocity = GroundEntity.Velocity;
        }
        else
        {
            ClearGroundEntity();
        }
    }

    /// <summary>
    ///     Clear the grounded state of the player.
    /// </summary>
    private void ClearGroundEntity()
    {
        GroundNormal = Vector3.Zero;
        GroundEntity = null;
        GroundSurface = null;
        BaseVelocity = Vector3.Zero;
    }

    private void Accelerate(Vector3 acceleration, float maxSpeed)
    {
        if (!Velocity.IsNearZeroLength)
        {
            Vector3 projectedVelocity = Velocity.Dot(WishVelocity) / Velocity.Length * Velocity.Normal;
            Vector3 rejectedVelocity = WishVelocity - projectedVelocity;

            if (CurrentSpeed + projectedVelocity.Length * Time.Delta > maxSpeed && projectedVelocity.Length > 0f)
                projectedVelocity *= ((maxSpeed - CurrentSpeed) / projectedVelocity.Length).Clamp(0f, 1f);

            acceleration = projectedVelocity + rejectedVelocity;
        }

        Velocity += acceleration * Time.Delta;
    }

    private enum MovementState
    {
        Idle,
        Walk,
        Run,
        Sprint
    }
}