using System.Text.Json;

namespace OOPA2;
/// <summary>
/// Tracks important metrics such as plays, wins etc.
/// </summary>
public class Statistics
{

    /// <summary>
    /// Creates a new statistics object.
    /// (calls LoadInstance so the variable Instance is populated)
    /// </summary>
    static Statistics() => LoadInstance();
    
    /// <summary>
    /// Tracks 7s and out plays
    /// </summary>
    public int SevensAndOutsPlays = 0;
    
    /// <summary>
    /// Player 1 wins in 7s and out
    /// </summary>
    public int SevensAndOutsP1Wins = 0;
    
    /// <summary>
    /// Player 2 wins in 7s and out
    /// </summary>
    public int SevensAndOutsP2Wins = 0;

    /// <summary>
    /// Plays in three or more
    /// </summary>
    public int ThreeOrMorePlays = 0;
    
    /// <summary>
    /// Player 1 wins in three or more
    /// </summary>
    public int ThreeOrMoreP1Wins = 0;
    
    /// <summary>
    /// Player 2 wins in three or more
    /// </summary>
    public int ThreeOrMoreP2Wins = 0;

    /// <summary>
    /// Number of dice rolled
    /// </summary>
    public int DiceRolled;

    /// <summary>
    /// 'Singleton' of statistics
    /// </summary>
    public static Statistics Instance;


    /// <summary>
    /// Sets Instance from a a file named stats.json.
    /// </summary>
    public static void LoadInstance()
    {
        try
        {
            //Check file exists first
            if (File.Exists("stats.json"))
            {
                string Content = File.ReadAllText("stats.json");
                Instance = JsonSerializer.Deserialize<Statistics>(Content);   
            }
            else
            {
                //Create instance and correspond
                Console.WriteLine("Statistics file doesn't exist, creating one.");
                Instance = new();
                SaveInstance();
            }
        }
        //Catch any errors that could happen like IO or serialisation issues.
        catch (Exception e) {  Console.WriteLine($"Failed to load statistics due to {e.Message}"); }


    }
    /// <summary>
    /// Preserves Instance member via JSON serialisation to a file
    /// named stats.json
    /// (JSON serialisation is used since its simpler than XML Serialisation.)
    /// </summary>
    public static void SaveInstance()
    {
        try
        {
            // write file via JSON serialisation
            string Content = JsonSerializer.Serialize(Instance);
            File.WriteAllText("stats.json", Content);
        }
        //Catch any errors like IO or serialisation issues
        catch (Exception e) { Console.WriteLine($"Failed to save file due to: {e.Message}"); }

    }
}