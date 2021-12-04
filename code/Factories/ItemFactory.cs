using System;
using ssl.Dao;
using ssl.Modules.Elements.Items.Carriables;
using ssl.Modules.Elements.Items.Data;

namespace ssl.Factories;

public sealed class ItemFactory
{
	private ItemDao itemDao = new ItemDao();
	private ItemFactory()
	{ }  
	private static ItemFactory instance;  
	public static ItemFactory Instance {  
		get
		{
			return instance ??= new ItemFactory();
		}  
	}
	
	public Item Create(string id)
	{
		ItemData itemData = itemDao.FindById(id);
		Item item = itemData switch
		{
			ItemFoodData itemFoodData => new ItemFood {FeedingValue = itemFoodData.FeedingValue},
			_ => throw new ArgumentException("Item type not supported")
		};

		item.Id = itemData.Id;
		item.Name = itemData.Name;
		item.Model = itemData.Model;
		item.HoldType = itemData.HoldType;
		item.WasteId = itemData.WasteId;
		return item;
	}
	
	public Item Create2(string id)
	{
		ItemData itemData = itemDao.FindById(id);
		Item item = new()
		{
			Id = itemData.Id,
			Name = itemData.Name,
			Model = itemData.Model,
			HoldType = itemData.HoldType,
			WasteId = itemData.WasteId
		};
		((ItemFood)item).FeedingValue = itemData switch
		{
			ItemFoodData itemFoodData => itemFoodData.FeedingValue,
			_ => throw new ArgumentException("Item type not supported")
		};
		return item;
	}
}
