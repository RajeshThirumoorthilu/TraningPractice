using Service.Common;
using Service.Models;

namespace Service.IRepository
{
    public interface IItemRepository
    {
        // To get the list of item 
        List<Item> GetItem();

        // To get the single row from item  by id 
        Item GetItem(int id);

        // To push the item  
        ItemResponseModel InsertItem(Item item);

        // To update the item 
        ItemResponseModel UpdateItem(Item item1);

        // To delete the item 
        ItemResponseModel DeleteItem(int id);
    }
}
