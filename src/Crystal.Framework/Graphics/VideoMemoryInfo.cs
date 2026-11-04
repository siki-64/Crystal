namespace Crystal.Graphics;

public readonly record struct VideoMemoryInfo(long LocalBudget, long LocalUsage, long NonLocalBudget, long NonLocalUsage);
