using Sandbox;
using ssl.Modules.Items.Carriables;
using ssl.Player;

namespace ssl.Modules.Props.Types
{
    public class Stain : Prop
    {
        public override string Id => "stain";
        public override string Name => "Stain";
        public override string Model => "models/citizen_props/wheel02/wheel02_gib01_lod01.vmdl_c";

        public override void OnAction(MainPlayer player, Item item)
        {
            if (item is ItemMop)
            {
                Delete();
            }
        }
    }
}