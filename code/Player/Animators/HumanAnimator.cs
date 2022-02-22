using System;
using Sandbox;
using ssl.Modules.Items.Instances;

namespace ssl.Player.Animators;

public class HumanAnimator : PawnAnimator
{
    private const float TURN_SPEED = 0.01F;
    private const float MAX_TIME_SINCE_FOOT_SHUFFLE = 0.1F;
    private const float YAW_DIFF_NO_ITEM = 90;
    private const float YAW_DIFF_ITEM = 50;

    private const string NO_CLIP_TAG = "noclip";
    private const string GROUNDED_KEY = "b_grounded";
    private const string NO_CLIP_KEY = "b_noclip";
    private const string AIM_EYES_KEY = "aim_eyes";
    private const string AIM_HEAD_KEY = "aim_head";
    private const string AIM_BODY_KEY = "aim_body";
    private const string AIM_BODY_WEIGHT_KEY = "aim_body_weight";
    private const string HOLD_TYPE_KEY = "holdtype";
    private const string SHUFFLE_KEY = "b_shuffle";
    private const string JUMP_KEY = "b_jump";

    private const string JUMP_EVENT_NAME = "jump";

    private const string MOVE_DIRECTION_KEY = "move_direction";
    private const string MOVE_SPEED_KEY = "move_speed";
    private const string MOVE_GROUND_SPEED_KEY = "move_groundspeed";
    private const string MOVE_X_KEY = "move_x";
    private const string MOVE_Y_KEY = "move_y";
    private const string MOVE_Z_KEY = "move_z";

    private const string WISH_DIRECTION_KEY = "wish_direction";
    private const string WISH_SPEED_KEY = "wish_speed";
    private const string WISH_GROUND_SPEED_KEY = "wish_groundspeed";
    private const string WISH_X_KEY = "wish_x";
    private const string WISH_Y_KEY = "wish_y";
    private const string WISH_Z_KEY = "wish_z";

    private TimeSince timeSinceFootShuffle = 60F;
    
    private Vector3 AimPos => Pawn.EyePosition + Input.Rotation.Forward * 200F;

    private new Sandbox.Player Pawn => base.Pawn as Sandbox.Player;
    public override void Simulate()
    {
        Rotation idealRotation = Rotation.LookAt(Input.Rotation.Forward.WithZ(0), Vector3.Up);

        DoRotation(idealRotation);
        DoWalk();

        // Let the animation graph know some shit
        bool isNoClip = HasTag(NO_CLIP_TAG);
        bool isGrounded = GroundEntity != null || isNoClip;

        SetAnimParameter(GROUNDED_KEY, isGrounded);
        SetAnimParameter(NO_CLIP_KEY, isNoClip);

        // Look in the direction what the player's input is facing
        SetLookAt();

        if (Pawn.ActiveChild is Item carry)
        {
            carry.SimulateAnimator(this);
        }
        else
        {
            SetAnimParameter(HOLD_TYPE_KEY, (int) HoldType.None);
            SetAnimParameter(AIM_BODY_WEIGHT_KEY, 0.5f);
        }
    }

    protected virtual void DoRotation(Rotation idealRotation)
    {
        // Our ideal player model rotation is the way we're facing
        float allowYawDiff = Pawn.ActiveChild == null ? YAW_DIFF_NO_ITEM : YAW_DIFF_ITEM;

        // If we're moving, rotate to our ideal rotation
        Rotation = Rotation.Slerp(Rotation, idealRotation, WishVelocity.Length * Time.Delta * TURN_SPEED);

        // Clamp the foot rotation to within 120 degrees of the ideal rotation
        Rotation = Rotation.Clamp(idealRotation, allowYawDiff, out float change);

        // If we did restrict, and are standing still, add a foot shuffle
        if (change > 1 && WishVelocity.Length <= 1) timeSinceFootShuffle = 0;

        SetAnimParameter(SHUFFLE_KEY, timeSinceFootShuffle < MAX_TIME_SINCE_FOOT_SHUFFLE);
    }

    private void SetLookAt()
    {
        SetLookAt(AIM_EYES_KEY, AimPos);
        SetLookAt(AIM_HEAD_KEY, AimPos);
        SetLookAt(AIM_BODY_KEY, AimPos);
    }

    private void DoWalk()
    {
        // Move Speed
        {
            Vector3 dir = Velocity;
            float forward = Rotation.Forward.Dot(dir);
            float sideward = Rotation.Right.Dot(dir);

            float angle = MathF.Atan2(sideward, forward).RadianToDegree().NormalizeDegrees();

            SetAnimParameter(MOVE_DIRECTION_KEY, angle);
            SetAnimParameter(MOVE_SPEED_KEY, Velocity.Length);
            SetAnimParameter(MOVE_GROUND_SPEED_KEY, Velocity.WithZ(0).Length);
            SetAnimParameter(MOVE_X_KEY, forward);
            SetAnimParameter(MOVE_Y_KEY, sideward);
            SetAnimParameter(MOVE_Z_KEY, Velocity.z);
        }

        // Wish Speed
        {
            Vector3 dir = WishVelocity;
            float forward = Rotation.Forward.Dot(dir);
            float sideward = Rotation.Right.Dot(dir);

            float angle = MathF.Atan2(sideward, forward).RadianToDegree().NormalizeDegrees();

            SetAnimParameter(WISH_DIRECTION_KEY, angle);
            SetAnimParameter(WISH_SPEED_KEY, WishVelocity.Length);
            SetAnimParameter(WISH_GROUND_SPEED_KEY, WishVelocity.WithZ(0).Length);
            SetAnimParameter(WISH_X_KEY, forward);
            SetAnimParameter(WISH_Y_KEY, sideward);
            SetAnimParameter(WISH_Z_KEY, WishVelocity.z);
        }
    }

    public override void OnEvent(string name)
    {
        if (name == JUMP_EVENT_NAME) Trigger(JUMP_KEY);

        base.OnEvent(name);
    }
}