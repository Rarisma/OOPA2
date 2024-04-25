namespace OOPA2;

/// <summary>
/// Three or More, rules defined by the assessment are below.
///
/// Rules:
/// Roll 5 dice hoping for a 3-of-a-kind or better.
/// If 2-of-a-kind is rolled,
/// player may choose to rethrow all, or the remaining dice.
/// 
/// 3-of-a-kind: 3 points
/// 4-of-a-kind: 6 points
/// 5-of-a-kind: 12 points
/// First to a total of 20.
/// </summary>
public class ThreeOrMore : Game
{
	/// <summary>
	/// Prevents the user from re-rolling multiple times in a single turn.
	/// </summary>
	private bool AllowReroll = true;

	/// <summary>
	/// Creates a new three or more object
	/// </summary>
	public ThreeOrMore() : base(5) { }
	
	/// <summary>
	/// Creates a new Three or more object
	/// </summary>
	/// <param name="TestMode">set to true to skip player or computer opponent dialog</param>
	public ThreeOrMore(bool TestMode) : base(5, TestMode) { }

	/// <summary>
	/// Starts the game loop
	/// </summary>
	public void StartGame()
	{
		while (Player1Points < 20 && Player2Points < 20)
		{
			//Player 1
			Console.WriteLine("Player 1, press enter to roll your dice.");
			Console.ReadLine();
			AllowReroll = true;
			RollDie();
			CheckRolls(ref Player1Points);

			//Player 2
			if (!CPUPlayer) //only ask non-computer players to roll their dice.
			{
				Console.WriteLine("\n\nPlayer 2, press enter to roll your dice.");
				Console.ReadLine();
			}
			else 
			{
				//delay before roll.
				Console.WriteLine("\n\nPlayer 2 is rolling...");
                Thread.Sleep(300);
            }
            RollDie();
			AllowReroll = true;
			CheckRolls(ref Player2Points);

			Console.WriteLine($"P1 Points {Player1Points}\tP2 Points {Player2Points}");
		}

		//Declare winner
		if (Player1Points >= 20)
		{
			Statistics.Instance.ThreeOrMoreP1Wins++;
			Console.WriteLine("Player 1 Wins");
		}
		else if (Player2Points >= 20)
		{
			Statistics.Instance.ThreeOrMoreP2Wins++;
			Console.WriteLine("Player 2 Wins");
		}
        else
        {
            Console.WriteLine("Its a tie!");
        }
    }

	/// <summary>
	/// Checks Rolls and applies appropriate rules.
	/// </summary>
	/// <param name="PlayerPoints">PlayPoints variable to add points to</param>
	public override void CheckRolls(ref int PlayerPoints)
    {
        for (int i = 0; i <= 6; i++)
        {
            int IdenticalRolls = Dice.Count(d => d.LastRoll == i);
			if (IdenticalRolls == 0) {continue;}
            Console.WriteLine($"Number of {i} Rolls: {IdenticalRolls}");

            if (IdenticalRolls == 2 && AllowReroll)
            {
	            //Only ask user if they want to re-roll, the computer will just 
	            //always re-roll the remaining die.
	            if (CPUPlayer == false || PlayerPoints == Player1Points)
	            {
					int Res = Program.LoopedInput("""
                                  You rolled doubles!
                                  Would you like to:
                                    1) Rethrow all die
                                    2) Rethrow remaining die
                                  """, 2);

					if (Res == 1) //re roll all die
					{
						Console.WriteLine("Re-rolling all die.");
						RollDie();
						AllowReroll = false;
						CheckRolls(ref PlayerPoints);
						return;
					}
					else if (Res == 2) //Re-roll remaining die
                    {
						Console.WriteLine("Re-rolling remaining die.");

                        for (int e = Dice.IndexOf(Dice.First(d => d.LastRoll == i)); e < 5; e++)
                        {
                            Dice[e].RollDie();
                        }
                        AllowReroll = false;
                        CheckRolls(ref PlayerPoints);
                    }
                }				
            }
            
            //Check identical rolls and assign points.
            if (IdenticalRolls == 3) { PlayerPoints += 3; }
            else if (IdenticalRolls == 4) { PlayerPoints += 6; }
            else if (IdenticalRolls == 5) { PlayerPoints += 12; }
        }
    }
}