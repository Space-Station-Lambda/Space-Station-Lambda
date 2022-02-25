namespace ssl.Modules.Props.Data;

public class PropLightData : PropData
{
    public PropLightData(string id) : base(id) { }

    public float Range { get; set; } = 100F;
    public Color Color { get; set; } = Color.White;
    public float Brightness { get; set; } = 100F;
    
}