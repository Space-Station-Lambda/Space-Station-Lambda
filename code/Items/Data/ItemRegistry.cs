using System;
using System.Collections.Generic;

namespace ssl.Items.Data
{
	/// <summary>
	/// Stores all references of item archetypes.
	/// </summary>
	public class ItemRegistry
	{
		private readonly Dictionary<string, Item> registry;

		public ItemRegistry()
		{
			registry = new Dictionary<string, Item>();
			RegisterItems();
		}

		public void Add(Item item)
		{
			registry.Add(item.Id, item);
		}

		public Item GetItemById(string id)
		{
			return ContainsItem(id) ? registry[id] : null;
		}

		/// <summary>
		/// Checks if the specified id is registered
		/// </summary>
		public bool ContainsItem(string id)
		{
			return registry.ContainsKey(id);
		}

		/// <summary>
		/// Register all the game items in the registry.
		/// In the future, this should disappear and should load items from an XML / JSON file.
		/// </summary>
		private void RegisterItems()
		{
			Add(new ItemFood("food.apple", "Apple", 10)); //Item registration Example
		}
	}
}
