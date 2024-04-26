namespace OOPA2;

/// <summary>
/// a basic interface for storing test results
/// </summary>
public interface ITestData
{
    /// <summary>
    /// Has the sevens and out test passed
    /// </summary>
    public bool SevensAndOutTestPassed { get; set; }

    /// <summary>
    /// has the dice roll test passed
    /// (doesn't roll higher than 7)
    /// </summary>
    public bool RollDicePassed { get; set; }

    /// <summary>
    /// Has the three or more tests passed?
    /// </summary>
    public bool ThreeOrMoreTestPassed { get; set; }

    /// <summary>
    /// When were these tests ran?
    /// </summary>
    public DateTime TestsRan { get; set; }
}