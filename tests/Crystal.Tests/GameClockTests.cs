namespace Crystal.Tests;

[TestClass]
public sealed class GameClockTests
{
    private static readonly TimeSpan Step = TimeSpan.FromMilliseconds(10);

    [TestMethod]
    public void FixedStepWaitsUntilATargetStepHasAccumulated()
    {
        var clock = new GameClock(Step, isFixedTimeStep: true);
        clock.Accumulate(TimeSpan.FromMilliseconds(4));

        Assert.AreEqual(TimeSpan.FromMilliseconds(6), clock.TimeUntilNextUpdate);
        Assert.AreEqual(0, clock.TakeUpdateSteps());
    }

    [TestMethod]
    public void FixedStepRunsOneUpdatePerTargetStepAndKeepsTheRemainder()
    {
        var clock = new GameClock(Step, isFixedTimeStep: true);
        clock.Accumulate(TimeSpan.FromMilliseconds(25));

        Assert.AreEqual(2, clock.TakeUpdateSteps());
        Assert.AreEqual(Step, clock.UpdateTime.ElapsedGameTime);
        Assert.IsTrue(clock.UpdateTime.IsRunningSlowly);
        Assert.AreEqual(TimeSpan.FromMilliseconds(20), clock.DrawTime.ElapsedGameTime);
        Assert.AreEqual(TimeSpan.FromMilliseconds(5), clock.TimeUntilNextUpdate);
    }

    [TestMethod]
    public void UpdateStepsAdvanceTotalGameTime()
    {
        var clock = new GameClock(Step, isFixedTimeStep: true);
        clock.Accumulate(TimeSpan.FromMilliseconds(30));

        var steps = clock.TakeUpdateSteps();
        for (var step = 0; step < steps; step++) clock.BeginUpdateStep();

        Assert.AreEqual(TimeSpan.FromMilliseconds(30), clock.UpdateTime.TotalGameTime);
        Assert.AreEqual(TimeSpan.FromMilliseconds(30), clock.DrawTime.TotalGameTime);
    }

    [TestMethod]
    public void VariableStepConsumesAllAccumulatedTime()
    {
        var clock = new GameClock(Step, isFixedTimeStep: false);
        clock.Accumulate(TimeSpan.FromMilliseconds(7));

        Assert.AreEqual(TimeSpan.Zero, clock.TimeUntilNextUpdate);
        Assert.AreEqual(1, clock.TakeUpdateSteps());
        Assert.AreEqual(TimeSpan.FromMilliseconds(7), clock.UpdateTime.ElapsedGameTime);
        Assert.IsFalse(clock.UpdateTime.IsRunningSlowly);
    }

    [TestMethod]
    public void AccumulatedTimeIsClampedAfterLongStalls()
    {
        var clock = new GameClock(Step, isFixedTimeStep: true);
        clock.Accumulate(TimeSpan.FromSeconds(5));

        Assert.AreEqual((int)(GameClock.MaxElapsedTime / Step), clock.TakeUpdateSteps());
    }
}
