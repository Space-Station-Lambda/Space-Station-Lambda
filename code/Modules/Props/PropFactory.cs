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

        private const string StainName = "stain";
        protected override string BasePath => "base/items";

        public override Prop Create(string prefix, string name)
        {
            string filePath = GetFilePath(prefix, name);
            switch (prefix)
            {
                case PropPrefix:
                    PropData propData = Resource.FromPath<PropData>(filePath);
                    return name switch
                    {
                        StainName => new Stain(propData),
                        _ => new Prop(propData)
                    };
                default:
                    throw new ArgumentOutOfRangeException($"The prefix does not exist for {prefix}.{name}");
            }
        }
    }
}