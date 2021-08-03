using Sandbox;

namespace ssl.Items.Data
{
    public partial class WeaponData : StackData
    {
        [Net] public TimeSince TimeSincePrimaryAttack { get; set; }
    }
}