namespace ssl.Modules.Roles
{
    /// <summary>
    /// Factions of the game, the main scenario is Protagonists vs Traitors.
    /// The game stops if a faction wins and a role can have multiple factions.
    /// </summary>
    public enum Faction
    {
        /// <summary>
        /// Good guys who maintain the station, the main faction
        /// </summary>
        Protagonists, 
        /// <summary>
        /// Want to distrub the station
        /// </summary>
        Traitors,
        /// <summary>
        /// Want to kill everyone
        /// </summary>
        Killer, 
    }
}