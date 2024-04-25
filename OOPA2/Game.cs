namespace OOPA2;

/// <summary>
/// This class provides a basic implementation of a game,
/// it provides common code for dice game such as initalising a
/// number of dice, storing player points, etc.
/// </summary>
public abstract class Game
{
    /// <summary>
    /// Amount of points player 1 has
    /// </summary>
    public int Player1Points = 0;

    /// <summary>
    /// ammount of points player 2 has
    /// </summary>
    public int Player2Points = 0;

	/// <summary>
	/// True if user is playing against the computer
	/// </summary>
    public bool CPUPlayer = false;

    /// <summary>
    /// Dice used in game
    /// </summary>
    public List<Die> Dice = new();

    /// <summary>
    /// Initializes the game, creates die objects
    /// </summary>
    /// <param name="DieCount">Amount of die the game uses.</param>
    protected Game(int DieCount)
    {
        for (int i = 0; DieCount > i; i++)
        {
            Dice.Add(new Die());
        }

        //Ask user if they are playing the computer or another person
        //Loop until valid input is entered.
        while (true)
        {
	        Console.WriteLine("""
	                          Who are you playing against
	                          1) Another Person
	                          2) The Computer
	                          """);
	        
	        var Result = Console.ReadLine();
	        if (Result == "1")
	        {
		        CPUPlayer = false;
		        break;
	        }
	        else if (Result == "2")
	        {
		        CPUPlayer = true;
		        break;
	        }
	        else
	        {
		        Console.WriteLine("That's not quite right, try entering a 2 or a 1.");
	        }
        }

	}

    /// <summary>
    /// Rolls every dice.
    /// </summary>
    protected void RollDie()
    {
        Console.WriteLine("Rolling all dice...");
        foreach (var die in Dice)
        {
            die.RollDie();
            Console.Write($" rolled a {die.LastRoll}");
        }
        Console.WriteLine("");
    }


	/// <summary>
	/// Implement roll checks that apply the scoring rules for that game
	/// </summary>
	/// <param name="Playerpoints"></param>
    public abstract void CheckRolls(ref int Playerpoints);
}