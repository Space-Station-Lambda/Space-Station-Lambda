using System.Collections.Generic;
using Sandbox;

namespace ssl.Player.Cameras;

public class SpectatorCamera : CameraMode
{
    private const float SPECTATOR_FIELD_OF_VIEW = 80F;
    private const float FOCUS_DISTANCE = 100F;
    private const float MAX_SPEED = 75F;
    private const float SPEED_CHANGE_FACTOR = 3F;

    private bool isClearTargetPress;
    private int playerIndex;

    private IList<SslPlayer> Players => Gamemode.Current.RoundManager.CurrentRound.Players;
    public float MoveSpeed { get; private set; } = 50F;
    public Entity Target { get; private set; }


    public override void Activated()
    {
        base.Activated();

        Target = null;
        FieldOfView = SPECTATOR_FIELD_OF_VIEW;
    }

    public override void Update()
    {
        if (Target != null)
            TargetCamMove();
        else
            FreeCamMove();

        if (Input.Pressed(InputButton.Attack1))
            PreviousTarget();
        else if (Input.Pressed(InputButton.Attack2)) NextTarget();
    }

    private void TargetCamMove()
    {
        Position = Target.Position + Input.Rotation.Backward * FOCUS_DISTANCE;
        Rotation = Input.Rotation;

        if (Input.Pressed(InputButton.Jump))
        {
            Target = null;
            isClearTargetPress = true;
        }
    }

    private void FreeCamMove()
    {
        float up = 0;

        if (!isClearTargetPress)
        {
            if (Input.Down(InputButton.Jump))
                up = 1;
            else if (Input.Down(InputButton.Duck)) up = -1;
        }
        else if (Input.Released(InputButton.Jump))
        {
            isClearTargetPress = false;
        }

        UpdateMoveSpeed();

        Vector3 wishDir = new Vector3(Input.Forward, Input.Left, 0).Normal * Rotation;
        wishDir += Vector3.Up * up;

        Position += wishDir * MoveSpeed;
        Rotation = Input.Rotation;
    }

    private void UpdateMoveSpeed()
    {
        MoveSpeed += Input.MouseWheel * SPEED_CHANGE_FACTOR;
        MoveSpeed = MoveSpeed.Clamp(0, MAX_SPEED);
    }

    private void NextTarget()
    {
        playerIndex = Players.Count > 0 ? (playerIndex + 1) % Players.Count : -1;
        UpdateTarget();
    }

    private void PreviousTarget()
    {
        playerIndex = playerIndex - 1 < 0 ? Players.Count - 1 : playerIndex - 1;
        UpdateTarget();
    }

    private void UpdateTarget()
    {
        if (0 <= playerIndex && playerIndex < Players.Count)
        {
            Target = Players[playerIndex];
            Log.Trace(Target);
        }
    }
}