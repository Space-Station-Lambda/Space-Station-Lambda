using ssl.Modules.Roles;

namespace ssl.Modules.Scenarios;

public readonly struct ScenarioConstraint
{
	public ScenarioConstraint( string roleId, int min = -1, int max = -1 )
	{
		RoleId = roleId;
		Min = min;
		Max = max;
	}

	/// <summary>
	///     Role to constraint
	/// </summary>
	public string RoleId { get; }

	/// <summary>
	///     Minimum amount of this role, -1 means infinite.
	/// </summary>
	public int Min { get; }

	/// <summary>
	///     Maximum amount of this role, -1 means infinite.
	/// </summary>
	public int Max { get; }

	public override string ToString()
	{
		return $"[{RoleId}]{Min}|{Max}";
	}
}
