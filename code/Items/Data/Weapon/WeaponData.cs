using Sandbox;

namespace ssl.Items.Data
{
    public partial class WeaponData : StackData
    {
        [Net, Predicted] public TimeSince TimeSincePrimaryAttack { get; set; }
    }
}