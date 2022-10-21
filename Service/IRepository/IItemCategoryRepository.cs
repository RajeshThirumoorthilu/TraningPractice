using Service.Common;
using Service.Models;

namespace Service.IRepository
{
    public interface IItemCategoryRepository
    {
        // To get the list of item category
        List<ItemCatergory> GetItemCatergory();

        // To get the single row from item category by id 
        ItemCatergory GetItemCatergory(int id);

        // To push the item category 
        ItemCategoryResponseModel InsertItemCategory(ItemCatergory itemCatergory);

        // To update the item Category
        ItemCategoryResponseModel UpdateItemCategory(ItemCatergory itemCatergory);

        // To delete the item Category
        ItemCategoryResponseModel DeleteItemCategory(int id);

    }
}
