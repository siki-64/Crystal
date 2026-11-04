using Crystal.Input;

namespace Crystal.Tests;

[TestClass]
public sealed class InputMapTests
{
    private enum TestAction
    {
        MoveForward,
        Jump,
        Unbound,
    }

    private static MouseState MouseAt(int x, int y, int scroll = 0, ButtonState right = ButtonState.Released) =>
        new(x, y, scroll, ButtonState.Released, ButtonState.Released, right, ButtonState.Released, ButtonState.Released);

    [TestMethod]
    public void FrameReportsEdgesMovementAndScrollNotches()
    {
        var frame = new InputFrame();
        frame.Advance(new KeyboardState(Keys.W), MouseAt(10, 20), default);
        frame.Advance(new KeyboardState(Keys.W, Keys.Space), MouseAt(13, 16, 2 * Mouse.WheelDelta, ButtonState.Pressed), default);

        Assert.IsTrue(frame.IsKeyPressed(Keys.Space));
        Assert.IsFalse(frame.IsKeyPressed(Keys.W));
        Assert.IsTrue(frame.IsMouseButtonPressed(MouseButton.Right));
        Assert.AreEqual(new Point(3, -4), frame.MouseMovement);
        Assert.AreEqual(2f, frame.ScrollNotches);
        Assert.AreEqual(Point.Zero, frame.RelativeMovement);

        frame.Advance(new KeyboardState(Keys.Space), MouseAt(13, 16, 2 * Mouse.WheelDelta), default);

        Assert.IsTrue(frame.IsKeyReleased(Keys.W));
        Assert.IsTrue(frame.IsMouseButtonReleased(MouseButton.Right));
        Assert.AreEqual(0f, frame.ScrollNotches);
    }

    [TestMethod]
    public void RelativeMovementIsTheDifferenceOfTheAccumulatedMotion()
    {
        var frame = new InputFrame();
        frame.Advance(default, new MouseState() with { RelativeX = 40, RelativeY = -10 }, default);
        frame.Advance(default, new MouseState() with { RelativeX = 55, RelativeY = -30 }, default);

        Assert.AreEqual(new Point(15, -20), frame.RelativeMovement);
    }

    [TestMethod]
    public void ActionIsDownWhileAnyBindingIsHeldAndEdgesOnlyOnce()
    {
        var map = new InputMap<TestAction>();
        map.Bind(TestAction.MoveForward, Keys.W);
        map.Bind(TestAction.MoveForward, Keys.Up);
        map.Bind(TestAction.MoveForward, Keys.W);
        var frame = new InputFrame();

        frame.Advance(new KeyboardState(Keys.W), default, default);
        Assert.IsTrue(map.WasPressed(frame, TestAction.MoveForward));

        frame.Advance(new KeyboardState(Keys.W, Keys.Up), default, default);
        Assert.IsTrue(map.IsDown(frame, TestAction.MoveForward));
        Assert.IsFalse(map.WasPressed(frame, TestAction.MoveForward));

        frame.Advance(new KeyboardState(Keys.Up), default, default);
        Assert.IsFalse(map.WasReleased(frame, TestAction.MoveForward));

        frame.Advance(default, default, default);
        Assert.IsTrue(map.WasReleased(frame, TestAction.MoveForward));
        Assert.AreEqual(2, map.BindingsOf(TestAction.MoveForward).Count);
    }

    [TestMethod]
    public void BindsMouseAndGamePadButtonsAndUnbinds()
    {
        var map = new InputMap<TestAction>();
        map.Bind(TestAction.Jump, MouseButton.Right);
        map.Bind(TestAction.Jump, Buttons.A);
        var frame = new InputFrame();

        frame.Advance(default, MouseAt(0, 0, right: ButtonState.Pressed), default);
        Assert.IsTrue(map.IsDown(frame, TestAction.Jump));

        frame.Advance(default, default, new GamePadState(true, Buttons.A, default, default));
        Assert.IsTrue(map.IsDown(frame, TestAction.Jump));
        Assert.IsFalse(map.WasPressed(frame, TestAction.Jump));

        Assert.IsTrue(map.Unbind(TestAction.Jump, Buttons.A));
        Assert.IsFalse(map.IsDown(frame, TestAction.Jump));
        Assert.IsFalse(map.IsDown(frame, TestAction.Unbound));
        Assert.AreEqual(0, map.BindingsOf(TestAction.Unbound).Count);
        Assert.ThrowsExactly<ArgumentException>(() => map.Bind(TestAction.Jump, default));
    }
}
