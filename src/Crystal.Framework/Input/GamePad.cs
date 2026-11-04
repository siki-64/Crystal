using System.Numerics;
using Crystal.Platform;

namespace Crystal.Input;

public static class GamePad
{
    public const int MaximumGamePadCount = 8;
    public const float LeftThumbDeadZone = 7849f / 32767f;
    public const float RightThumbDeadZone = 8689f / 32767f;
    public const float TriggerThreshold = 30f / 255f;

    public static GamePadState GetState(int playerIndex, GamePadDeadZone deadZone = GamePadDeadZone.IndependentAxes)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(playerIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(playerIndex, MaximumGamePadCount);
        var raw = SdlInput.ReadGamePad(playerIndex);
        if (!raw.IsConnected || deadZone == GamePadDeadZone.None) return raw;

        var left = ApplyDeadZone(raw.ThumbSticks.Left, LeftThumbDeadZone, deadZone);
        var right = ApplyDeadZone(raw.ThumbSticks.Right, RightThumbDeadZone, deadZone);
        var leftTrigger = ApplyDeadZone(raw.Triggers.Left, TriggerThreshold);
        var rightTrigger = ApplyDeadZone(raw.Triggers.Right, TriggerThreshold);
        var buttons = raw.PressedButtons & ~(Buttons.LeftTrigger | Buttons.RightTrigger);
        if (leftTrigger > 0) buttons |= Buttons.LeftTrigger;
        if (rightTrigger > 0) buttons |= Buttons.RightTrigger;
        return new GamePadState(true, buttons, new GamePadThumbSticks(left, right), new GamePadTriggers(leftTrigger, rightTrigger));
    }

    private static Vector2 ApplyDeadZone(Vector2 stick, float deadZoneSize, GamePadDeadZone deadZone)
    {
        if (deadZone == GamePadDeadZone.IndependentAxes) return new Vector2(ApplyDeadZone(stick.X, deadZoneSize), ApplyDeadZone(stick.Y, deadZoneSize));
        var length = stick.Length();
        if (length <= deadZoneSize) return Vector2.Zero;
        return stick / length * (MathF.Min(length, 1f) - deadZoneSize) / (1f - deadZoneSize);
    }

    private static float ApplyDeadZone(float value, float deadZoneSize)
    {
        var magnitude = MathF.Abs(value);
        if (magnitude <= deadZoneSize) return 0f;
        return MathF.CopySign((MathF.Min(magnitude, 1f) - deadZoneSize) / (1f - deadZoneSize), value);
    }
}
