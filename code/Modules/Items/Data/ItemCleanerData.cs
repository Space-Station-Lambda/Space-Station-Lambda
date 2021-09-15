using Sandbox;

namespace ssl.Modules.Items.Data
{
    [Library("cleaner")]
    public class ItemCleanerData : ItemData
    {
        public int CleaningValue { get; set; } = 10;
    }
}