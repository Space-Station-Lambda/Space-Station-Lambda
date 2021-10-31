using System;
using Sandbox;
using ssl.Modules.Elements.Props.Data;
using ssl.Modules.Elements.Props.Data.Stains;
using ssl.Modules.Elements.Props.Types;
using ssl.Modules.Elements.Props.Types.Stains;

using Prop = ssl.Modules.Elements.Props.Types.Prop;

namespace ssl.Modules.Elements.Props
{
    public class PropFactory : InstanceFactory<PropData, Prop>
    {
        private const string PropPrefix = "prop";
        private const string StainPrefix = "stain";
        private const string SlippyStain = "slippystain";
        protected override string BasePath => "data/props";

        public override Prop Create(string prefix, string name)
        {
            string filePath = GetFilePath(prefix, name);
            switch (prefix)
            {
                case StainPrefix:
                    return new Stain(TryLoad<StainData>(filePath));
                
                case SlippyStain:
                    return new SlippyStain(TryLoad<SlippyStainData>(filePath));
                
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
