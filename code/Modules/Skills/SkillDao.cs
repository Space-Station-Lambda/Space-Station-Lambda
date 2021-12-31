using ssl.Commons;
using ssl.Constants;

namespace ssl.Modules.Skills;

public class SkillDao : LocalDao<SkillData>
{
    private static SkillDao instance;

    private SkillDao() { }

    public static SkillDao Instance => instance ??= new SkillDao();

    /// <summary>
    ///     Load all skills data from disk files.
    /// </summary>
    protected override void LoadAll()
    {
        Log.Info("Load skills..");

        Save(new SkillData(Identifiers.AIM_ID)
        {
            Name = "Aim",
            Description = "Aim of the player"
        });

        Save(new SkillData(Identifiers.SPEED_ID)
        {
            Name = "Speed",
            Description = "Speed of the player"
        });
        Save(new SkillData(Identifiers.CLEANING_ID)
        {
            Name = "Cleaning",
            Description = "cleaning of the player"
        });
        Save(new SkillData(Identifiers.STRENGTH_ID)
        {
            Name = "Strength",
            Description = "Strength of the player"
        });

        Log.Info($"{All.Count} skills charged !");
    }
}