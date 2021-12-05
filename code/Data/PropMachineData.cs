namespace ssl.Data;

public class PropMachineData : PropData
{
    public PropMachineData(string id) : base("machine." + id)
    {
    }
    
    /// <summary>
    /// Durability of the machine. More durability = harder to destroy and mode time to repair
    /// </summary>
    public int Durability { get; set; }
    /// <summary>
    /// Determine the skill needed for repair the machine
    /// </summary>
    public int Complexity { get; set; }
}
