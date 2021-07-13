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

		public Item Apple => registry["item.food.apple"]; //Item shortcut example
		
		public void Add(Item item)
		{
			registry.Add(item.Id, item);
		}

		public Item GetItemById(string id)
		{
			if (!registry.ContainsKey(id))
			{
				throw new NotImplementedException( "This item does not exist.");
			}

			return registry[id];
		}

		/// <summary>
		/// Register all the game items in the registry.
		/// In the future, this should disappear and should load items from an XML / JSON file.
		/// </summary>
		private void RegisterItems()
		{
			Add(new ItemFood("item.food.apple", "Apple", 10)); //Item registration Example
		}
	}
}
