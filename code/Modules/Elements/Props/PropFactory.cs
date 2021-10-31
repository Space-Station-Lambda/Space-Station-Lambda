using System;
using ssl.Modules.Elements.Props.Data;
using ssl.Modules.Elements.Props.Types;
using Prop = ssl.Modules.Elements.Props.Types.Prop;

namespace ssl.Modules.Elements.Props
{
    public class PropFactory : InstanceFactory<PropData, Types.Prop>
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
