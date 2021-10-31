using System.Collections.Generic;
using Sandbox;
using ssl.Modules.Selection;

namespace ssl.Player.Controllers
{
    public partial class HumanController : BasePlayerController
    {
        private const float TopGroundDetect = 0.1F;
        private const float BottomGroundDetect = 2F;

        private const float BodyHeight = 72F;
        private const float EyeHeight = 64F;
        private const float BodyGirth = 16F;

        private const float StopSpeed = 100F;

        private const float StepSize = 20F;

        private const float MaxNonJumpVelocity = 200F;
        private const float JumpForce = 300F;
        private const float AirSpeed = 30F;
        private const float AirAcceleration = 500F;

        private const float MinSpeed = 1F;

        private const float GroundAngle = 46F;
        private const float StickGroundStartMultiplier = 2F;

        private const string JumpEventName = "jump";

        private readonly Dictionary<MovementState, Speed> speeds = new()
        {
            { MovementState.Idle, new Speed { Acceleration = 0f, MaxSpeed = 0f } },
            { MovementState.Walk, new Speed { Acceleration = 500f, MaxSpeed = 70f } },
            { MovementState.Run, new Speed { Acceleration = 1500f, MaxSpeed = 150f } },
            { MovementState.Sprint, new Speed { Acceleration = 2000f, MaxSpeed = 300f } },
        };

        private Vector3 maxs;
        private Vector3 mins;

        private MovementState state = MovementState.Idle;
        private Unstuck unstuck;

        public HumanController()
        {
            unstuck = new Unstuck(this);
        }
        
        public HumanController(MainPlayer player) : this()
        {
            if (Host.IsServer) Player = player;
        }


        [Net] private MainPlayer Player { get; set; }
        public Vector3 GravityVector { get; set; } = Vector3.Down * 981F;
        public float CurrentSpeed => Velocity.Length;

        public bool IsGrounded => GroundEntity != null;
        public Surface GroundSurface { get; set; }
        public float SurfaceFriction { get; set; } = 4;

        public bool IsSprinting { get; set; } = false;


        public override void Simulate()
        {
            if (!Player.IsRagdoll)
            {
                UpdateBBox();

                EyePosLocal = Vector3.Up * (EyeHeight * Pawn.Scale);
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
            else if (Player.CanStand)
            {
                Log.Info(((TimeSince)Player.TimeExitRagdoll).Absolute);
                bool wantExitRagdoll = Input.Pressed(InputButton.Jump) || Input.Forward + Input.Left > 0;
                
                if (wantExitRagdoll)
                {
                    Player.StopRagdoll();
                }
            }
        }

        /// <summary>
        /// Traces the current bbox and returns the result.
        /// liftFeet will move the start position up by this amount, while keeping the top of the bbox at the same
        /// position. This is good when tracing down because you won't be tracing through the ceiling above.
        /// </summary>
        public override TraceResult TraceBBox(Vector3 start, Vector3 end, float liftFeet = 0.0f)
        {
            return TraceBBox(start, end, mins, maxs, liftFeet);
        }

        /// <summary>
        /// Moves the player in the direction of their velocity if there is no colliding objects.
        /// </summary>
        protected virtual void TryPlayerMove()
        {
            if (Velocity.Length <= MinSpeed)
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
        /// Moves the player in the direction of their velocity and if there is colliding objects, checks if it can step.
        /// </summary>
        protected virtual void TryPlayerMoveWithStep()
        {
            if (Velocity.Length <= MinSpeed)
            {
                Velocity = Vector3.Zero;
                return;
            }

            MoveHelper mover = GetMoveHelper();

            mover.TryMoveWithStep(Time.Delta, StepSize);

            Position = mover.Position;
            Velocity = mover.Velocity;
        }

        /// <summary>
        /// BoundingBox (collision box)
        /// </summary>
        protected virtual void UpdateBBox()
        {
            mins = new Vector3(-BodyGirth, -BodyGirth, 0) * Pawn.Scale;
            maxs = new Vector3(+BodyGirth, +BodyGirth, BodyHeight) * Pawn.Scale;
        }

        private MoveHelper GetMoveHelper()
        {
            MoveHelper mover = new(Position, Velocity);
            mover.Trace = mover.Trace.Size(mins, maxs).Ignore(Pawn);
            mover.MaxStandableAngle = GroundAngle;

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

            if (Input.Down(InputButton.Run)) state = MovementState.Sprint;
            else if (Input.Down(InputButton.Walk)) state = MovementState.Walk;
            else if (WishVelocity.Length > 0) state = MovementState.Run;
            else state = MovementState.Idle;
        }

        /// <summary>
        /// Applies the Ground Movement logic to accelerate the player
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
            {
                if (((MainPlayer)Pawn).Dragger.Dragged == draggable)
                    return;
            }
            
            ClearGroundEntity();
            Velocity += Vector3.Up * JumpForce;
            AddEvent(JumpEventName);
        }

        private void Air()
        {
            WishVelocity *= AirAcceleration;
            Accelerate(WishVelocity, AirSpeed);
        }

        /// <summary>
        /// Try to keep a walking player on the ground when running down slopes
        /// </summary>
        private void StickToGround()
        {
            Vector3 start = Position + Vector3.Up * StickGroundStartMultiplier;
            Vector3 end = Position + Vector3.Down * StepSize;

            // See how far up we can go without getting stuck
            TraceResult trace = TraceBBox(Position, start);
            start = trace.EndPos;

            // Now trace down from a known safe position
            trace = TraceBBox(start, end);

            if (trace.Fraction <= 0) return;
            if (trace.Fraction >= 1) return;
            if (trace.StartedSolid) return;
            if (Vector3.GetAngle(Vector3.Up, trace.Normal) > GroundAngle) return;

            Position = trace.EndPos;
        }

        /// <summary>
        /// Adds the Gravity Vector if the player is not on ground
        /// </summary>
        private void ApplyGravity()
        {
            if (!IsGrounded)
            {
                Velocity += GravityVector * Time.Delta;
            }
            else
            {
                Velocity = Velocity.WithZ(0);
            }
        }

        /// <summary>
        /// Apply a specific amount of friction
        /// </summary>
        /// <param name="frictionAmount">Friction to apply</param>
        private void ApplyFriction(float frictionAmount = 1.0f)
        {
            if (CurrentSpeed < 0.1f) return;

            float usedSpeed = CurrentSpeed < StopSpeed ? StopSpeed : CurrentSpeed;
            float droppedSpeed = usedSpeed * Time.Delta * frictionAmount;
            float newSpeed = CurrentSpeed - droppedSpeed;

            if (newSpeed < 0) newSpeed = 0;

            newSpeed /= CurrentSpeed;
            Velocity *= newSpeed;
        }

        /// <summary>
        /// Updates the GroundEntity property and all the other related properties like ground normal, surface, etc.
        /// </summary>
        private void UpdateGroundEntity()
        {
            Vector3 startPos = Position + Vector3.Up * TopGroundDetect;
            Vector3 endPos = Position - Vector3.Up * BottomGroundDetect;

            TraceResult trace = TraceBBox(startPos, endPos, mins, maxs, 4.0F);

            if (trace.Hit && Velocity.z <= MaxNonJumpVelocity)
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
        /// Clear the grounded state of the player.
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
                {
                    projectedVelocity *= ((maxSpeed - CurrentSpeed) / projectedVelocity.Length).Clamp(0f, 1f);
                }

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
}