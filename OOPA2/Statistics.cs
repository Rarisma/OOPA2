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
    public int SevensAndOutsPlays { get; set; }
    
    /// <summary>
    /// Player 1 wins in 7s and out
    /// </summary>
    public int SevensAndOutsP1Wins { get; set; }
    
    /// <summary>
    /// Player 2 wins in 7s and out
    /// </summary>
    public int SevensAndOutsP2Wins { get; set; }

    /// <summary>
    /// Plays in three or more
    /// </summary>
    public int ThreeOrMorePlays { get; set; }
    
    /// <summary>
    /// Player 1 wins in three or more
    /// </summary>
    public int ThreeOrMoreP1Wins { get; set; }
    
    /// <summary>
    /// Player 2 wins in three or more
    /// </summary>
    public int ThreeOrMoreP2Wins { get; set; }

    /// <summary>
    /// Number of dice rolled
    /// </summary>
    public int DiceRolled { get; set; }

    /// <summary>
    /// 'Singleton' of statistics
    /// </summary>
    public static Statistics Instance;


    /// <summary>
    /// Sets Instance from a file named stats.json.
    /// </summary>
    public static void LoadInstance()
    {
        try
        {
            //Check file exists first
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "//stats.json"))
            {
                string Content = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "//stats.json");
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
        catch (Exception e) {  Console.WriteLine($"Failed to load statistics due to {e.Message} {e.StackTrace}"); }


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
            string Content = JsonSerializer.Serialize<Statistics>(Instance);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "//stats.json", Content);
        }
        //Catch any errors like IO or serialisation issues
        catch (Exception e) { Console.WriteLine($"Failed to save file due to: {e.Message} {e.StackTrace}"); }

    }

    /// <summary>
    /// Shows all the statistics.
    /// </summary>
    public static void ShowStats()
    {
        //Display stats in nice neat way
        Console.WriteLine($"""
                            === Statistics ===
                            General:
                            Dice Rolled: {Instance.DiceRolled}
                            Player 1 Wins (Total): {Instance.ThreeOrMoreP1Wins + Instance.SevensAndOutsP1Wins}
                            Player 2 Wins (Total): {Instance.ThreeOrMoreP2Wins + Instance.SevensAndOutsP2Wins}
                                Games played (Total): {Instance.SevensAndOutsPlays + Instance.ThreeOrMorePlays}
                            
                            Three or More:
                            Player 1 Wins: {Instance.ThreeOrMoreP1Wins}
                            Player 2 Wins: {Instance.ThreeOrMoreP2Wins}
                                Games Played: {Instance.ThreeOrMorePlays}
                                
                            Sevens and out:
                            Player 1 Wins: {Instance.SevensAndOutsP1Wins}
                            Player 2 Wins: {Instance.SevensAndOutsP2Wins}
                                Games Played: {Instance.SevensAndOutsPlays}
                            """);
    }
}