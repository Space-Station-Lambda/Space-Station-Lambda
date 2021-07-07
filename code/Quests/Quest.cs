namespace ssl.Quests
{
    /// <summary>
    /// A quest is an objective for the player
    /// </summary>
    public abstract class Quest
    {
        /// <summary>
        /// Name of the quest
        /// </summary>
        public abstract string Libelle { get; }
        /// <summary>
        /// Description of the question
        /// </summary>
        public abstract string Description { get; }
        /// <summary>
        /// When the quest is started
        /// </summary>
        public abstract void OnStart();
        /// <summary>
        /// Used when you want to check something each tick of the quest
        /// </summary>
        public abstract void OnUpdate();
        /// <summary>
        /// When the quest is completed with a success
        /// </summary>
        public abstract void OnCompete();
        /// <summary>
        /// When the quest is failed
        /// </summary>
        public abstract void OnFail();
        /// <summary>
        /// When the quest is canceled
        /// </summary>
        public abstract void OnCancel();
    }
}