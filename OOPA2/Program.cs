namespace OOPA2;

public class Program
{
	/// <summary>
	/// This is the entry point, it shows the user a list of options
	/// and handles their options.
	/// </summary>
	static void Main(string[] args)
	{
		//Show menu
		Console.WriteLine("""
		                  Welcome to OOP Assessment 2, select an option:
		                    1) Play Sevens and out
		                    2) Play Three or more
		                    3) View statistics
		                    4) Run tests
		                  """);

		bool loop = true;
		while (loop)
		{
			try
			{
				loop = false;
				//Handle user input
				Console.WriteLine("Select a game:");
				
				//Example of error handling
				switch (Convert.ToInt16(Console.ReadLine()))
				{
					case 1: //sevens and out
						Statistics.Instance.SevensAndOutsPlays++;
						var game = new SevensOut();
						game.StartGame();
						break;
					case 2: //three or more
						Statistics.Instance.ThreeOrMorePlays++;
						new ThreeOrMore();
						break;
					case 3: //View stats
						Console.WriteLine($"""
						                   === Statistics ===
						                   General:
						                     Dice Rolled: {Statistics.Instance.DiceRolled}
						                     Player 1 Wins (Total): {Statistics.Instance.ThreeOrMoreP1Wins + Statistics.Instance.SevensAndOutsP1Wins}
						                     Player 2 Wins (Total): {Statistics.Instance.ThreeOrMoreP2Wins + Statistics.Instance.SevensAndOutsP2Wins}
						                     Games played (Total): {Statistics.Instance.SevensAndOutsPlays + Statistics.Instance.ThreeOrMorePlays}
						                     
						                   Three or More:
						                     Player 1 Wins: {Statistics.Instance.ThreeOrMoreP1Wins}
						                     Player 2 Wins: {Statistics.Instance.ThreeOrMoreP2Wins}
						                     Games Played: {Statistics.Instance.ThreeOrMorePlays}
						                     
						                   Sevens and out:
						                     Player 1 Wins: {Statistics.Instance.SevensAndOutsP1Wins}
						                     Player 2 Wins: {Statistics.Instance.SevensAndOutsP2Wins}
						                     Games Played: {Statistics.Instance.SevensAndOutsPlays}
						                   """);
						break;
					case 4: //Tests
						var TestResults = new Testing().RunAllTests();
						string ResultData = $"""
						                     Tests ran at {TestResults.TestsRan}
						                     Sevens and out test Passed {TestResults.SevensAndOutTestPassed}
						                     Three or more test Passed {TestResults.ThreeOrMoreTestPassed}
						                     Dice test Passed {TestResults.RollDicePassed}
						                     """;
						Console.WriteLine(ResultData);
						File.WriteAllText($"TestResultsAsOf{TestResults.TestsRan.Date}.txt", ResultData);
						Console.WriteLine($"Results saved to TestResultsAsOf{TestResults.TestsRan.Date}");
						break;
					default: // handle invalid input
						Console.WriteLine("That's not a valid option.");
						loop = true;
						break;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Invalid Input, try again.");
				loop = true;
			}
		}

		Console.WriteLine("""
	                  Would you like to go back to the menu or quit?
	                  1) Menu
	                  2) Quit
	                  """);

		string res = Console.ReadLine();
		if (res == "2")
		{
			Environment.Exit(0);
		}
		else if (res == "1") { Console.WriteLine("Going back to the main menu"); }
		else { Console.WriteLine("That's not a valid response, going back to the menu."); }
		Main(null);

	}
}