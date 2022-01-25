using ssl.Commons;
using ssl.Constants;
using ssl.Modules.Props.Data;

namespace ssl.Modules.Props;

public class PropDao : LocalDao<PropData>
{
    private static PropDao instance;

    private PropDao() { }

    public static PropDao Instance => instance ??= new PropDao();

    protected override void LoadAll()
    {
        Log.Info("Load props..");

        // Props
        Save(new PropData(Identifiers.Props.BALLOON_ID)
        {
            Name = "Ballon",
            Model = "models/citizen_props/balloonheart01.vmdl_c"
        });

        Save(new PropData(Identifiers.Props.BUCKET_ID)
        {
            Name = "Bucket",
            Model = "models/props/bucket/bucket.vmdl"
        });

        Save(new PropData(Identifiers.Props.STAIN_ID)
        {
            Name = "Stain",
            Model = ""
        });

        Save(new PropData(Identifiers.Props.WET_FLOOR_SIGN_ID)
        {
            Name = "Wet Floor sign",
            Model = "models/props/wet_floor_sign/wet_floor_sign.vmdl",
            IsPhysical = true
        });

        Log.Info($"{All.Count} props charged !");
    }
}