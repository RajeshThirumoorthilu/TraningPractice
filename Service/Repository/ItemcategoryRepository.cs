using NLog;
using Service.Common;
using Service.IRepository;
using Service.Models;
using System;
using System.Net;
using System.Net.WebSockets;

namespace Service.Repository
{
    public class ItemcategoryRepository : IItemCategoryRepository
    {
        private DatabaseContext _context;
        private Logger _logger = LogManager.GetCurrentClassLogger();
        public ItemcategoryRepository(DatabaseContext context) 
        {
            _context = context;
        }
        public List<ItemCatergory> GetItemCatergory()
        {
            _logger.Info(" ------- GetItemCatergory Api repository starts ------- ");
            var list = new List<ItemCatergory>(); 
            try
            {
                throw new Exception();
                list = _context.Set<ItemCatergory>().ToList();
                _logger.Info($" ------- GetItemCatergory Api repository ends returns {list.Count} records ------- ");
                return list;
            }
            catch(Exception Ex)
            {
                _logger.Error($" ------- GetItemCatergory Api repository ends returns InnerException {Ex.InnerException} ------- ");
                _logger.Error($" ------- GetItemCatergory Api repository ends returns Message {Ex.Message} ------- ");
                return list;
            }
        }
        public ItemCatergory GetItemCatergory(int id)
        {
            ItemCatergory itemCatergory = new ItemCatergory(); 
            try
            {
                itemCatergory = _context.Find<ItemCatergory>(id);
                return itemCatergory;
            }
            catch (Exception Ex)
            {
                return itemCatergory;
            }
        } 

        public ItemCategoryResponseModel InsertItemCategory(ItemCatergory itemCatergory)
        {
            ItemCategoryResponseModel itemCategoryResponseModel = new ItemCategoryResponseModel(); 
            try
            {
                if (itemCatergory == null)
                {
                    itemCategoryResponseModel.StatusCode = HttpStatusCode.BadRequest;
                    itemCategoryResponseModel.Message = "Record Added Failed";
                    return itemCategoryResponseModel;
                }
                _context.Add(itemCatergory);
                var res = _context.SaveChanges();
                if (res == 0)
                {
                    itemCategoryResponseModel.StatusCode = HttpStatusCode.BadRequest;
                    itemCategoryResponseModel.Message = "Record Added Failed";
                    return itemCategoryResponseModel;
                }
                itemCategoryResponseModel.StatusCode = HttpStatusCode.OK;
                itemCategoryResponseModel.Message = "Record Added Successfully";
                return itemCategoryResponseModel;
            }
            catch(Exception Ex)
            {
                itemCategoryResponseModel.StatusCode = HttpStatusCode.BadRequest;
                itemCategoryResponseModel.Message = "Record Added Failed";
                return itemCategoryResponseModel;
            }
        }
        public ItemCategoryResponseModel UpdateItemCategory(ItemCatergory itemCatergory)
        {
            ItemCategoryResponseModel itemCategoryResponseModel = new ItemCategoryResponseModel();
            try
            {
                if (itemCatergory == null || itemCatergory.ItemCategoryId == 0)
                {
                    itemCategoryResponseModel.StatusCode = HttpStatusCode.BadRequest;
                    itemCategoryResponseModel.Message = "Record Updated Failed";
                    return itemCategoryResponseModel;
                }
                var itemcat = _context.itemCatergories.Find(itemCatergory.ItemCategoryId);
                if (itemcat == null)
                {
                    itemCategoryResponseModel.StatusCode = HttpStatusCode.BadRequest;
                    itemCategoryResponseModel.Message = "Record Updated Failed";
                    return itemCategoryResponseModel;
                }
                itemcat.ItemDescription = itemcat.ItemDescription;
                itemcat.ItemCategoryName = itemCatergory.ItemCategoryName;
                itemcat.CreatedOn = itemCatergory.CreatedOn;
                _context.itemCatergories.Update(itemcat);
                var res = _context.SaveChanges();
                if (res == 0)
                {
                    itemCategoryResponseModel.StatusCode = HttpStatusCode.BadRequest;
                    itemCategoryResponseModel.Message = "Record Updated Failed";
                    return itemCategoryResponseModel;
                }
                itemCategoryResponseModel.StatusCode = HttpStatusCode.OK;
                itemCategoryResponseModel.Message = "Record Updated Successfully";
                return itemCategoryResponseModel;
            }
            catch (Exception Ex)
            {
                itemCategoryResponseModel.StatusCode = HttpStatusCode.BadRequest;
                itemCategoryResponseModel.Message = "Record Updated Failed";
                return itemCategoryResponseModel;
            }
        }
        public ItemCategoryResponseModel DeleteItemCategory(int id)
        {
            ItemCategoryResponseModel itemCategoryResponseModel = new ItemCategoryResponseModel();
            try
            {
                if (id == 0)
                {
                    itemCategoryResponseModel.StatusCode = HttpStatusCode.BadRequest;
                    itemCategoryResponseModel.Message = "Record Deletion Failed";
                    return itemCategoryResponseModel;
                }
                var itemcategory = _context.Find<ItemCatergory>(id);
                if (itemcategory == null)
                {
                    itemCategoryResponseModel.StatusCode = HttpStatusCode.BadRequest;
                    itemCategoryResponseModel.Message = "Record Deletion Failed";
                    return itemCategoryResponseModel;
                }
                _context.itemCatergories.Remove(itemcategory);
                var res = _context.SaveChanges();
                if (res == 0)
                {
                    itemCategoryResponseModel.StatusCode = HttpStatusCode.BadRequest;
                    itemCategoryResponseModel.Message = "Record Deletion Failed";
                    return itemCategoryResponseModel;
                }
                itemCategoryResponseModel.StatusCode = HttpStatusCode.OK;
                itemCategoryResponseModel.Message = "Record Deleted Successfully";
                return itemCategoryResponseModel;
            }
            catch(Exception Ex)
            {
                itemCategoryResponseModel.StatusCode = HttpStatusCode.BadRequest;
                itemCategoryResponseModel.Message = "Record Deletion Failed";
                return itemCategoryResponseModel;
            }
        }
    }
}
