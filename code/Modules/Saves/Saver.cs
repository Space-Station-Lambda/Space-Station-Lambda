using Sandbox;

namespace ssl.Modules.Saves;

public class Saver<T>
{
	private const string Extention = ".json";
	private readonly string path;

	public Saver( string name )
	{
		path = name + Extention;
	}

	public bool IsSaved => FileSystem.Data.FileExists(path);

	/// <summary>
	///     Save data to json file
	/// </summary>
	/// <param name="toSave">Data to save</param>
	public void Save( T toSave )
	{
		//Delete file before write on it
		if ( IsSaved )
		{
			FileSystem.Data.DeleteFile(path);
		}

		Log.Info($"[SAVE]{toSave} saved at {path}");
		FileSystem.Data.WriteAllText("player.txt", "hello world");
		FileSystem.Data.WriteJson(path, toSave);
	}

	/// <summary>
	///     Load data from json file
	/// </summary>
	/// <returns>The data</returns>
	public T Load()
	{
		T res = FileSystem.Data.ReadJson<T>(path);
		Log.Info($"[SAVE]{res} load from {path}");
		return res;
	}
}
