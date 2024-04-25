namespace OOPA2;

/// <summary>
/// This class provides a simulation of a die.
/// It can roll between one and six.
/// </summary>
public class Die
{
    /// <summary>
    /// Creates a new Die Object
    /// </summary>
    public Die(){}

    /// <summary>
    /// Create a new die object with but allows the last roll
    /// to be set for testing
    /// </summary>
    /// <param name="LastRoll">Value of last roll.</param>
    public Die(int LastRoll)
    {
        roll = LastRoll;
    }
    
    /// <summary>
    /// Provides randomness, static so multiple die instances don't return
    /// the same exact value as other die.
    /// </summary>
    private static Random rand = new();

    /// <summary>
    /// Encapsulated value of the die roll.
    /// </summary>
    private int roll;
    
    /// <summary>
    /// Die roll value, its get only so it can't be modified
    /// outside of this class.
    /// (Example of encapsilation)
    /// </summary>
    public int LastRoll { get => roll; } 

    
    /// <summary>
    /// Rolls die, returning a value between one and six.
    /// </summary>
    public int RollDie()
    {
        roll = rand.Next(1, 7); //roll die

		Statistics.Instance.DiceRolled++;
        
        //return die roll.
        return LastRoll;
    }
}