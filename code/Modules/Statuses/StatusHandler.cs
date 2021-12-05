using System.Collections.Generic;
using System.Linq;
using Sandbox;
using ssl.Player;

namespace ssl.Modules.Statuses;

public partial class StatusHandler : EntityComponent<SslPlayer>
{
	[Net] public List<Status> Statuses { get; private set; }

	/// <summary>
	///     Add status. Create a new one if he is not affected by the status
	/// </summary>
	/// <param name="status">The status to add</param>
	public void ApplyStatus( Status status )
	{
		foreach ( Status s in Statuses.Where(s => !s.IsInfinite).Where(s => s.Id.Equals(status.Id)) )
		{
			s.TimeLeft += status.TimeLeft;
			return;
		}

		Statuses.Add(status);
		status.OnApply(Entity);
	}

	public void ResolveStatus( Status status )
	{
		foreach ( Status s in Statuses.Where(s => status.Id.Equals(s.Id)) )
		{
			if ( !s.IsInfinite )
			{
				s.TimeLeft -= status.TimeLeft;
			}

			if ( s.TimeLeft <= 0 || s.IsInfinite )
			{
				s.OnResolve(Entity);
				Statuses.Remove(s);
				return;
			}
		}
	}

	public void ResolveStatus<T>() where T : Status
	{
		foreach ( Status s in Statuses )
		{
			if ( s is T statusT )
			{
				s.OnResolve(Entity);
				Statuses.Remove(s);
				return;
			}
		}
	}

	public Status GetStatus( string id )
	{
		return Statuses.FirstOrDefault(s => s.Id.Equals(id));
	}

	public T GetStatus<T>() where T : Status
	{
		foreach ( Status status in Statuses )
		{
			if ( status is T statusT )
			{
				return statusT;
			}
		}

		return null;
	}

	/// <summary>
	///     Tick statuses and remove status if ended timer
	/// </summary>
	public void Tick()
	{
		HashSet<Status> statusesToTick = new(Statuses);
		foreach ( Status status in statusesToTick )
		{
			status.OnTick(Entity);
			if ( status.TimeLeft <= 0 && !status.IsInfinite )
			{
				status.OnEnd(Entity);
				Statuses.Remove(status);
			}
		}
	}
}
