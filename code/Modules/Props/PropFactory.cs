using Sandbox;
using ssl.Commons;
using ssl.Modules.Props.Data;
using ssl.Modules.Props.Instances;
using Prop = ssl.Modules.Props.Instances.Prop;

namespace ssl.Modules.Props;

public class PropFactory : IFactory<Prop>
{
    private static PropFactory instance;

    private PropFactory() { }

    public static PropFactory Instance => instance ??= new PropFactory();

    public Prop Create(string id)
    {
        PropData propData = PropDao.Instance.FindById(id);

        Prop prop = propData switch
        {
            PropMachineData propMachineData => new PropMachine(),
            PropTrashBinData propTrashBinData => new PropTrashBin(),
            PropLightData propLightData => new PropLight
            {
                Color = propLightData.Color,
                Brightness = propLightData.Brightness,
                Range = propLightData.Range,
            },
			_ => new Prop()
		};

        prop.Id = propData.Id;
        prop.Name = propData.Name;
        prop.Model = Model.Load(propData.Model);
        prop.Health = propData.Health;
        prop.Destroyable = propData.Destroyable;
        return prop;
    }
}