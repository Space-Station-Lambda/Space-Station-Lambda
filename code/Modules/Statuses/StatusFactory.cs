using System;
using ssl.Commons;
using ssl.Modules.Statuses.Data;
using ssl.Modules.Statuses.Types;

namespace ssl.Modules.Statuses;

public sealed class StatusFactory : IFactory<Status>
{
    private static StatusFactory instance;

    private StatusFactory() { }

    public static StatusFactory Instance => instance ??= new StatusFactory();

    public Status Create(string id)
    {
        StatusData statusData = StatusDao.Instance.FindById(id);

        Status status = statusData switch
        {
            SicknessData sicknessData => new Sickness(statusData.Duration),
            RestrainedData restrainedData => new Restrained(),
            _ => throw new ArgumentException($"There's no corresponding status for {id}")
        };

        status.Id = statusData.Id;
        status.Name = statusData.Name;
        status.Description = statusData.Description;
        status.IsInfinite = statusData.Duration == 0;
        return status;
    }
}