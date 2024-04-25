namespace OOPA2;


/// <summary>
/// Sevens and out, rules as set out by the assessment are below.
/// 2 x dice
/// Rules:
/// Roll the two dice, noting the total rolled each time.
///     If it is a 7 - stop.
///     If it is any other number - add it to your total.
///     If it is a double - add double the total to your score (3,3 would add 12 to your total)
/// </summary>
public class SevensOut : Game
{
	/// <summary>
	/// Controls if player 1 is out
	/// </summary>
	public bool Player1Out = false;
	
	/// <summary>
	/// Controls if player 2 is out
	/// </summary>
	public bool Player2Out = false;

    public SevensOut() : base(2) { }

    /// <summary>
    /// Starts the game into a loop
    /// </summary>
    public void StartGame()
    {
	    while (Player1Out == false || Player2Out == false)
	    {
		    if (Player1Out == false)
		    {
			    Console.WriteLine("Player 1 Press enter to roll your dice.");
			    Console.ReadLine();
			    RollDie();
			    CheckRolls(ref Player1Points);
		    }

		    if (Player2Out == false) //player 2 code
		    {
			    if (!CPUPlayer) // Automatically roll if playing against the computer.
			    {
				    Console.WriteLine("Player 2 Press enter to roll your dice.");
				    Console.ReadLine();
			    }
			    else
			    {
				    Console.WriteLine("\nPlayer 2's turn!");
			    }
			    RollDie();
			    CheckRolls(ref Player2Points);
		    }

		    Console.WriteLine($"P1 Points {Player1Points}\tP2 Points {Player2Points}");
	    }

		if (Player1Points > Player2Points) 
		{
			Console.WriteLine("Player 1 Wins");
			Statistics.Instance.SevensAndOutsP1Wins++;
		}
		else if (Player1Points < Player2Points) 
		{
			Console.WriteLine("Player 2 Wins");
			Statistics.Instance.SevensAndOutsP2Wins++;
        }
		else { Console.WriteLine("Its a tie!"); }
    }
    
    /// <summary>
    /// Rolls dice, implementing all rules as stated within the brief.   
    /// </summary>
    /// <param name="PlayPoints">Player ref to add points to</param>
    public override void CheckRolls(ref int PlayPoints)
    {
        int total = Dice[0].LastRoll + Dice[1].LastRoll;

        if (total == 7) // Total of seven, do nothing.
        {
			if (PlayPoints == Player1Points)
			{
				Player1Out = true;
				Console.WriteLine("\n\n\nPlayer 1 IS OUT!");

			}
			else
			{
				Player2Out = true;
				Console.WriteLine("\n\n\nPlayer 2 IS OUT!");
			}
		}
        else if (Dice[0].LastRoll == Dice[1].LastRoll) // Doubles
        {
			Console.WriteLine($"You scored {total * 2}");
            PlayPoints += total * 2;
        }
        else // Any other non-seven or non-double value
        {
	        Console.WriteLine($"You scored {total}");
			PlayPoints += total;
        }
    }
}