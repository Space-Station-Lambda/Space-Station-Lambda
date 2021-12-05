using System;
using System.Linq;

namespace ssl.Modules.Locking;

/// <summary>
///     A lock is a system for locked stuff. We sue characters for implement mini games later.
/// </summary>
public class Lock
{
	private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
	private const int BaseComplexity = 10;

	/// <summary>
	///     Create a lock with a specific key to open it
	/// </summary>
	/// <param name="key">The key used to lock</param>
	public Lock( string key )
	{
		Key = key;
		IsLocked = true;
	}

	/// <summary>
	///     Create a lock with a specific complexity
	/// </summary>
	/// <param name="complexity">Length of the key</param>
	public Lock( int complexity ) : this(GenerateKey(complexity))
	{
	}

	public Lock() : this(BaseComplexity)
	{
	}

	/// <summary>
	///     Key to unlock
	/// </summary>
	public string Key { get; }

	/// <summary>
	///     Status of the lock
	/// </summary>
	public bool IsLocked { get; private set; }

	/// <summary>
	///     Open the lock without a key
	/// </summary>
	public void Open()
	{
		IsLocked = false;
	}

	/// <summary>
	///     Open the lock with a specific key
	/// </summary>
	/// <param name="key">The key used to open</param>
	/// <returns>Returns true if the lock is openned</returns>
	public bool Open( string key )
	{
		if ( Key.Equals(key) )
		{
			IsLocked = false;
			return true;
		}

		return false;
	}

	/// <summary>
	///     Close the lock
	/// </summary>
	public void Close()
	{
		IsLocked = true;
	}

	/// <summary>
	///     Close the lock with the key
	/// </summary>
	/// <param name="key">The key used to close</param>
	/// <returns>Returns true if the lock is closed</returns>
	public bool Close( string key )
	{
		if ( Key.Equals(key) )
		{
			IsLocked = true;
			return false;
		}

		return false;
	}

	/// <summary>
	///     Compare the lock with a key
	/// </summary>
	/// <param name="key"></param>
	/// <returns>Returns the ratio of good character</returns>
	public float CompareKey( string key )
	{
		//Compare each character and calculate the number of same chars
		int goodCharacters = Key.TakeWhile(( t, i ) => i < key.Length).Where(( t, i ) => t.Equals(key[i])).Count();

		return (float)goodCharacters / Key.Length;
	}

	/// <summary>
	///     Create a random code with a specific complexity
	/// </summary>
	/// <param name="complexity">Length of the code</param>
	/// <returns>The key</returns>
	public static string GenerateKey( int complexity )
	{
		Random random = new();
		string key = "";
		for ( int i = 0; i < complexity; i++ )
		{
			int characterIndex = random.Next(Characters.Length);
			char character = Characters[characterIndex];
			key += character;
		}

		return key;
	}
}
