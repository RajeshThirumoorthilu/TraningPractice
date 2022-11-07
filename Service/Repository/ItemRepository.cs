using NLog;
using Service.Common;
using Service.IRepository;
using Service.Models;
using System.Net;

namespace Service.Repository
{
    public class ItemRepository : IItemRepository
    {
        private DatabaseContext _context;
        private Logger _logger = LogManager.GetCurrentClassLogger();
        public ItemRepository(DatabaseContext context)
        {
            _context = context;
        }
        public List<Item> GetItem()
        {
            _logger.Info(" ------- GetItem Api repository starts ------- ");

            var list = new List<Item>();
            try
            {
                list = _context.Set<Item>().ToList();
                _logger.Info($" ------- GetItem Api repository ends returns {list.Count} records ------- ");
                return list;
            }
            catch (Exception Ex)
            {
                _logger.Info($" ------- GetItem Api repository ends returns InnerException {Ex.InnerException} ------- ");
                _logger.Info($" ------- GetItem Api repository ends returns Message {Ex.Message} ------- ");
                return list;
            }
        }
        public Item GetItem(int id)
        {
            Item item = new Item();
            try
            {
                item = _context.Find<Item>(id);
                return item;
            }
            catch (Exception Ex)
            {
                return item;
            }
        }

        public ItemResponseModel InsertItem(Item item)
        {
            ItemResponseModel itemResponseModel = new ItemResponseModel();
            try
            {
                if (item == null)
                {
                    itemResponseModel.StatusCode = HttpStatusCode.BadRequest;
                    itemResponseModel.Message = "Record Added Failed";
                    return itemResponseModel;
                }
                _context.Add(item);
                var res = _context.SaveChanges();
                if (res == 0)
                {
                    itemResponseModel.StatusCode = HttpStatusCode.BadRequest;
                    itemResponseModel.Message = "Record Added Failed";
                    return itemResponseModel;
                }
                itemResponseModel.StatusCode = HttpStatusCode.OK;
                itemResponseModel.Message = "Record Added Successfully";
                return itemResponseModel;
            }
            catch (Exception Ex)
            {
                itemResponseModel.StatusCode = HttpStatusCode.BadRequest;
                itemResponseModel.Message = "Record Added Failed";
                return itemResponseModel;
            }
        }
        public ItemResponseModel UpdateItem(Item item)
        {
            ItemResponseModel itemResponseModel = new ItemResponseModel();
            try
            {
                if (item == null || item.ItemId == 0)
                {
                    itemResponseModel.StatusCode = HttpStatusCode.BadRequest;
                    itemResponseModel.Message = "Record Updated Failed";
                    return itemResponseModel;
                }
                var items = _context.items.Find(item.ItemId);
                if (items == null)
                {
                    itemResponseModel.StatusCode = HttpStatusCode.BadRequest;
                    itemResponseModel.Message = "Record Updated Failed";
                    return itemResponseModel;
                }
                items.ItemDescription = item.ItemDescription;
                items.ItemName = item.ItemName;
                items.CreatedOn = item.CreatedOn;
                items.ItemCategoryId = item.ItemCategoryId;
                _context.items.Update(items);
                var res = _context.SaveChanges();
                if (res == 0)
                {
                    itemResponseModel.StatusCode = HttpStatusCode.BadRequest;
                    itemResponseModel.Message = "Record Updated Failed";
                    return itemResponseModel;
                }
                itemResponseModel.StatusCode = HttpStatusCode.OK;
                itemResponseModel.Message = "Record Updated Successfully";
                return itemResponseModel;
            }
            catch (Exception Ex)
            {
                itemResponseModel.StatusCode = HttpStatusCode.BadRequest;
                itemResponseModel.Message = "Record Updated Failed";
                return itemResponseModel;
            }
        }
        public ItemResponseModel DeleteItem(int id)
        {
            ItemResponseModel itemResponseModel = new ItemResponseModel();
            try
            {
                if (id == 0)
                {
                    itemResponseModel.StatusCode = HttpStatusCode.BadRequest;
                    itemResponseModel.Message = "Record Deletion Failed";
                    return itemResponseModel;
                }
                var item = _context.Find<Item>(id);
                if (item == null)
                {
                    itemResponseModel.StatusCode = HttpStatusCode.BadRequest;
                    itemResponseModel.Message = "Record Deletion Failed";
                    return itemResponseModel;
                }
                _context.items.Remove(item);
                var res = _context.SaveChanges();
                if (res == 0)
                {
                    itemResponseModel.StatusCode = HttpStatusCode.BadRequest;
                    itemResponseModel.Message = "Record Deletion Failed";
                    return itemResponseModel;
                }
                itemResponseModel.StatusCode = HttpStatusCode.OK;
                itemResponseModel.Message = "Record Deleted Successfully";
                return itemResponseModel;
            }
            catch (Exception Ex)
            {
                itemResponseModel.StatusCode = HttpStatusCode.BadRequest;
                itemResponseModel.Message = "Record Deletion Failed";
                return itemResponseModel;
            }
        }
    }
}
