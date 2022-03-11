using Sandbox;
using ssl.Commons;

namespace ssl.Modules;

/// <summary>
///     SSL wrapper for entities that can be spawned in the game.
///     Inheriting classes are for example Prop or Item.
/// </summary>
[Hammer.Model]
public abstract partial class WorldEntity : AnimEntity
{
    [Net, Property] public string Id { get; set; }

    internal void RegisterDao()
    {
        if (IsFromMap)
        {
            SaveToDao();
        }
    }

    /// <summary>
    ///     This method is used when the entity has been created from Hammer to register the entity in the corresponding
    ///     DAO if it has not been registered before.
    ///     Such a case would be for example a special item only on a specific map.
    ///     <br/>
    ///     Don't forget to expose properties needed for the Data object to Hammer with [Property].
    ///     <br/>
    ///     <b>WARNING: base method should NOT be called</b>
    /// </summary>
    private protected abstract void SaveToDao();

    /// <summary>
    ///     Checks if an entity can be added to the given DAO.
    /// </summary>
    /// <param name="dao">The destination DAO.</param>
    /// <param name="entity">The entity willing to register itself.</param>
    /// <typeparam name="T">The type of DAO's Data object.</typeparam>
    /// <returns>true if entity can be added, false otherwise.</returns>
    private protected virtual bool CanSaveToDao<T>(IDao<T> dao, WorldEntity entity) where T : BaseData
    {
        if (Id is null) return false;
        if (string.IsNullOrWhiteSpace(Id)) return false;
        return !dao.ContainsId(entity.Id);
    }
    // TODO: This system leads to a lot of repeating code, maybe it should change if we find a better way
}