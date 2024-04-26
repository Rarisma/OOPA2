namespace OOPA2;

/// <summary>
/// This class provides a basic implementation of a game,
/// it provides common code for dice game such as initialising a
/// number of dice, storing player points, etc.
/// </summary>
public abstract class Game
{
    /// <summary>
    /// Amount of points player 1 has
    /// </summary>
    public int Player1Points = 0;

    /// <summary>
    /// amount of points player 2 has
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
    /// <param name="TestMode">Skip computer dialog</param>
    protected Game(int DieCount, bool TestMode = false)
    {
		//Initialise required amount of dice.
        for (int i = 0; DieCount > i; i++)
        {
            Dice.Add(new Die());
        }

		//Don't ask who we are playing against if test mode is enabled.
        if (TestMode)
        {
	        CPUPlayer = true;
	        return;
        }

        //Ask user if they are playing the computer or another person
        int Result = Program.LoopedInput("""
                                      Who are you playing against
                                      1) Another Person
                                      2) The Computer
                                      """, 2);

        //Set to computer user if second option pressed
        CPUPlayer = Result == 2;
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
	/// <param name="PlayerPoints"></param>
    public abstract void CheckRolls(ref int PlayerPoints);
}