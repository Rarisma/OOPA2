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
		int result = LoopedInput("""
		                  Welcome to OOP Assessment 2, select an option:
		                    1) Play Sevens and out
		                    2) Play Three or more
		                    3) View statistics
		                    4) Run tests

		                  Select a game:
		                  """, 4);

        switch (result)
        {
            case 1: //sevens and out
                Statistics.Instance.SevensAndOutsPlays++;
                var game = new SevensOut();
                game.StartGame();
                break;
            case 2: //three or more
                Statistics.Instance.ThreeOrMorePlays++;
                var g = new ThreeOrMore();
                g.StartGame();
				break;
            case 3: //View stats
				Statistics.ShowStats();
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
                File.WriteAllText($"TestResults.txt", ResultData);
                Console.WriteLine($"Results saved to TestResults.txt");
                break;
        }

		//save stats
		Statistics.SaveInstance();

		// Ask if user wants to quit or go back to the main menu
		result = LoopedInput("""
	                  Would you like to go back to the menu or quit?
	                  1) Menu
	                  2) Quit
	                  """, 2);
		if (result == 1) { Main(null); }
		else if (result == 2) { Environment.Exit(0); }
	}

	/// <summary>
	/// Ask a user a question and loop until they provide a valid response.
	/// </summary>
	/// <param name="Message"></param>
	/// <param name="OptionsCount"></param>
	/// <returns></returns>
	public static int LoopedInput(string Message, int OptionsCount)
	{
		int Option = -1;
		//Loop until an option within the accepted range is entered.
		while (Option <= 0 || Option > OptionsCount)
		{
			//Print Message
            Console.WriteLine(Message);

			try
			{
				//Read line and convert to int
				Option = Convert.ToInt16(Console.ReadLine());
			}
			catch //Catch invalid input 
			{
				Console.WriteLine("That's not a valid answer!");
				Option = -1;
			}
        }

		//return answer in range
		return Option;
    }
}