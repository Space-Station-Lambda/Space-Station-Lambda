namespace ssl.Modules.Props.Instances;

/// <summary>
///     A machine is a basic classs for all machines in the map. A machine have a health and it is reparaible by the
///     mecanic.
///     It's not instanciable because a machine have to had a purpose.
/// </summary>
public class PropMachine : Prop
{

	/// <summary>
	///     When the machine get hits
	/// </summary>
	/// <param name="damage">The damage taken</param>
	public void OnDamage( int damage )
	{
		Health -= damage;
		if ( damage <= 0 )
		{
			Destroy();
		}
	}
	
	public int Durability { get; set; }
	public int Complexity { get; set; }

	/// <summary>
	///     When the machine get hits
	/// </summary>
	/// <param name="value">The damage reapaired</param>
	public void OnRepair( int value )
	{
		Health += value;
		if ( Health > Durability )
		{
			Health = Durability;
		}
	}

	/// <summary>
	///     Destroy the machine.
	/// </summary>
	public void Destroy()
	{
		//TODO SSL-385: In first time a machine can't be destroyed because we don't handle the build options
	}
}
