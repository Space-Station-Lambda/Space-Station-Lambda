using System;
using ssl.Commons;

namespace ssl.Modules.Statuses;

public sealed class StatusFactory : IFactory<Status>
{
    private static StatusFactory instance;

    private StatusFactory()
    {
    }

    public static StatusFactory Instance => instance ??= new StatusFactory();

    public Status Create(string id)
    {
        StatusData statusData = StatusDao.Instance.FindById(id);

        Status status = statusData switch
        {
            _ => throw new ArgumentException($"There's no corresponding status for {id}")
        };

        status.Id = statusData.Id;
        status.Name = statusData.Name;
        status.Description = statusData.Description;

        return status;
    }
}