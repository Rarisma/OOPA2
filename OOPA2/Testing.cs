using System.Diagnostics;

namespace OOPA2;

public class Testing : ITestData
{
	public bool SevensAndOutTestPassed { get; set; }
	public bool RollDicePassed { get; set; }
	public bool ThreeOrMoreTestPassed { get; set; }
	public DateTime TestsRan { get; set; }
	public ITestData RunAllTests()
	{
		RollDicePassed = TestDice(1000); //Test dice 1000 times
		SevensAndOutTestPassed= SevenOrMoreTotalTest(); //Test 7 total is out
		ThreeOrMoreTestPassed = ThreeOrMore20Test();
		TestsRan = DateTime.Now;
		return (ITestData)this;
	}
	
	
	private bool TestDice(int IterationCount)
	{
		Die D = new();
		try
		{
			for (int i = 0; i < IterationCount; i++)
			{
				Debug.Assert(D.RollDie() < 7, "Dice rolled higher than 6!");
				Debug.Assert(D.RollDie() >= 1, "Dice rolled lower than 1!");
			}

		}
		catch
		{
			Console.WriteLine("[Dice Test] Test Failed.");
			return false;
		}

		Console.WriteLine("[Dice Test] Test Passed.");
		return true;
	}

	/// <summary>
	/// Tests if the sevens or more code detects seven correctly
	/// </summary>
	private static bool SevenOrMoreTotalTest()
	{
		SevensOut S = new();

		try
		{
			//Check player 1 isn't already out.
			Debug.Assert(S.Player1Out == false, "Player 1 is already out even after instantiating.");
		}
		catch (Exception e) { return false; }

		
		//Provide list of dice that add to seven.
		S.Dice = new()
		{
			new(4),
			new(3)
		};
		
		//check rolls as player this should mark player 1 as out.
		//as player 1 has rolled a 3 and a 4, equaling 7
		S.CheckRolls(ref S.Player1Points);

		try
		{
			Debug.Assert(S.Player1Out, "Player 1 isn't out despite rolling a seven.");
		}
		catch (Exception e) { return false; }

		return S.Player1Out;
	}

	private bool ThreeOrMore20Test()
	{
		//Checks that player 1 has won
		ThreeOrMore T = new();
		var prev = Statistics.Instance.ThreeOrMoreP1Wins;
		T.Player1Points = 120; //20 points is required to win so this will force a P1 win
		T.Player2Points = 2;
		
		Debug.Assert(prev+1 == Statistics.Instance.ThreeOrMoreP1Wins);
		T.StartGame();
		return false;
	}
}