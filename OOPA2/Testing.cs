using System.Diagnostics;

namespace OOPA2;

public class Testing : ITestData
{
	/// <summary>
	/// Has the sevens and out test passed
	/// </summary>
	public bool SevensAndOutTestPassed { get; set; }

	/// <summary>
	/// Has the Dice test passed
	/// </summary>
	public bool RollDicePassed { get; set; }

	/// <summary>
	/// Has the three or more test passed
	/// </summary>
	public bool ThreeOrMoreTestPassed { get; set; }

	/// <summary>
	/// When were the tests last ran?
	/// </summary>
	public DateTime TestsRan { get; set; }

	/// <summary>
	/// Runs all tests
	/// </summary>
	/// <returns>ITestData Object containing test result data.</returns>
	public ITestData RunAllTests()
	{
		RollDicePassed = TestDice(1000); //Test dice 1000 times
		SevensAndOutTestPassed= SevenOrMoreTotalTest(); //Test 7 total is out
		ThreeOrMoreTestPassed = ThreeOrMore20Test();
		TestsRan = DateTime.Now;
		return (ITestData)this;
	}
	
	/// <summary>
	/// Test dice rolling
	/// </summary>
	/// <param name="IterationCount">how many times to loop (1000 recomended)</param>
	/// <returns>False if failed, true if otherwise</returns>
	private bool TestDice(int IterationCount)
	{
		Die D = new();
        for (int i = 0; i < IterationCount; i++)
        {
            D.RollDie(); //roll die

            //Bounds check
            Debug.Assert(D.LastRoll < 7, "Dice rolled higher than 6!");
            Debug.Assert(D.LastRoll >= 1, "Dice rolled lower than 1!");
            if (D.LastRoll >= 1 == false || D.LastRoll < 7 == false)
            {
                Console.WriteLine("[Dice Test] Test Failed.");
				return false;
            }
        }

		Console.WriteLine("[Dice Test] Test Passed.");
		return true;
	}

    /// <summary>
    /// Tests if the sevens or more code detects seven correctly
    /// </summary>
    /// <returns>True if test passed, false otherwise.</returns>
    private static bool SevenOrMoreTotalTest()
	{
        SevensOut S = new(true);

        //Check player 1 isn't already out.
        Debug.Assert(S.Player1Out == false, "Player 1 is already out even after instantiating.");
        if (S.Player1Out != false) { return false; }
		
		//Provide list of dice that add to seven.
		S.Dice = new() { new(4), new(3) };
		
		//check rolls as player this should mark player 1 as out.
		//as player 1 has rolled a 3 and a 4, equaling 7
		S.CheckRolls(ref S.Player1Points);

        Debug.Assert(S.Player1Out, "Player 1 isn't out despite rolling a seven.");

        //Test passed if player 1 is out.
        Console.WriteLine($"[Sevens and out Test] Test {(S.Player1Out == true ? "passed" : "failed" )}");
        return S.Player1Out;
	}

	/// <summary>
	/// Checks three more win condition logic
	/// </summary>
	/// <returns>True if test passed, false otherwise.</returns>
	private bool ThreeOrMore20Test()
	{
		//Checks that player 1 has won
		ThreeOrMore T = new(true);
		var prev = Statistics.Instance.ThreeOrMoreP1Wins;
		T.Player1Points = 120; //20 points is required to win so this will force a P1 win
		T.Player2Points = 2;

        T.StartGame();

		//Check player 1 has won
		bool res = prev + 1 == Statistics.Instance.ThreeOrMoreP1Wins;
        Debug.Assert(prev+1 == Statistics.Instance.ThreeOrMoreP1Wins);

		if (res) //Remove win point if one has been assigned.
		{
            //Decrease win stat so we don't mess with statistics.
            Statistics.Instance.ThreeOrMoreP1Wins--; 
        }

		Console.WriteLine($"[Three or more Test ] Test {(res ? "passed" : "failed")}");
        return res;
	}
}