using System;
using Sandbox;
using ssl.Modules.Props.Data;
using ssl.Modules.Props.Types;
using Prop = ssl.Modules.Props.Types.Prop;

namespace ssl.Modules.Props
{
    public class PropFactory : InstanceFactory<PropData, Prop>
    {
        private const string PropPrefix = "prop";

        private const string StainPrefix = "stain";
        protected override string BasePath => "data/props";

        public override Prop Create(string prefix, string name)
        {
            string filePath = GetFilePath(prefix, name);
            switch (prefix)
            {
                case StainPrefix:
                    return new Stain(TryLoad<StainData>(filePath));
                case PropPrefix:
                    PropData propData = TryLoad<PropData>(filePath);
                    return name switch
                    {
                        _ => propData.IsPhysical ? new PhysicalProp(propData) : new Prop(propData)
                    };
                default:
                    throw new ArgumentOutOfRangeException($"The prefix does not exist for {prefix}.{name}");
            }
        }
    }
}
