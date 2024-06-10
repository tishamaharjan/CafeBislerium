using System.Text.Json;

namespace CafeBislerium.Data
{
    public static class ItemDetails
    {
        //Method for saving item details in .json file
        public static void SaveItemDetails(List<Items> items)
        {
            string itemFile = Utils.GetItemFile();

            var jsonItem = JsonSerializer.Serialize(items);
            File.WriteAllText(itemFile, jsonItem);
        }

        //Method for getting and listing item details from .json file
        public static List<Items> GetItemDetails()
        {
            string itemFile = Utils.GetItemFile();

            if (!File.Exists(itemFile))
            {
                return new List<Items>();
            }

            var json = File.ReadAllText(itemFile);
            var items = JsonSerializer.Deserialize<List<Items>>(json);
            return items;
        }

        //Method for adding new item in items.json file
        public static List<Items> AddNewItem(string itemName, float price, ItemCategory itemCat)
        {
            List<Items> items = GetItemDetails();
            bool itemExists = items.Any(x => x.ItemName == itemName);

            if (itemExists)
            {
                throw new Exception($"{itemName} already exists!");
            }
            items.Add(
                new Items
                {
                    ItemName = itemName,
                    Price = price,
                    Category = itemCat
                }
            );
            SaveItemDetails(items);
            return items;
        }

        //Method for editing or updating price of item
        public static List<Items> UpdatePrice(string itemName, float price)
        {
            List<Items> items = GetItemDetails();
            Items item = items.FirstOrDefault(items => items.ItemName == itemName);

            if (item == null)
            {
                throw new Exception("Cannot find this item!");
            }
            item.Price = price;
            SaveItemDetails(items);
            return items;
        }

        //Method for deleting of item from items.json file
        public static List<Items> DeleteItem(string itemName)
        {
            List<Items> items = GetItemDetails();
            Items item = items.FirstOrDefault(items => items.ItemName == itemName);

            if (item == null)
            {
                throw new Exception("Cannot find this item!");
            }
            items.Remove(item);
            SaveItemDetails(items);
            return items;
        }
    }
}
