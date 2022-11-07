using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Service.Common;
using Service.IRepository;
using Service.Models;
using System.Net;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;

        private readonly DatabaseContext _context;
        
        private Logger _logger = LogManager.GetCurrentClassLogger();
        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
            //_itemRepository =new ItemRepository(_context);
        }

        #region apis

        /// To get the item  list
        [HttpGet]
        [Route("GetItem")]
        public IActionResult GetItem()
        {
            try
            {
                _logger.Info(" ------- GetItem Api hit starts ------- ");
                var item = _itemRepository.GetItem();
                if (item == null)
                {
                    _logger.Info($" ------- GetItem Api hit ends return status code {HttpStatusCode.NotFound} ------- ");
                    return NotFound();
                }
                _logger.Info($" ------- GetItem Api hit ends return status code {HttpStatusCode.OK} ------- ");
                return Ok(item);
            }
            catch (Exception Ex)
            {
                return BadRequest();
            }
        }
        /// To get the item  list by id
        [HttpGet]
        [Route("GetItemById")]
        public IActionResult GetItemById(int id)
        {
            try
            {
                var item = _itemRepository.GetItem(id);
                if (item == null)
                {
                    return NotFound();
                }
                return Ok(item);
            }
            catch (Exception Ex)
            {
                return BadRequest();
            }
        }
        /// To insert or push data in item 
        [HttpPost]
        [Route("InsertItem")]
        public ItemResponseModel InsertItem(Item item)
        {
            try
            {
                var result = _itemRepository.InsertItem(item);
                if (result == null)
                {
                    return new ItemResponseModel() { Message = "Record Added Failed", StatusCode = HttpStatusCode.BadRequest };
                }
                return result;
            }
            catch (Exception Ex)
            {
                return new ItemResponseModel() { Message = "Record Added Failed", StatusCode = HttpStatusCode.BadRequest };
            }
        }
        /// To update data in item 
        [HttpPut]
        [Route("UpdateItem")]
        public ItemResponseModel UpdateItem(Item item)
        {
            try
            {
                var result = _itemRepository.UpdateItem(item);
                if (result == null)
                {
                    return new ItemResponseModel() { Message = "Record Updation Failed", StatusCode = HttpStatusCode.BadRequest };
                }
                return result;
            }
            catch (Exception Ex)
            {
                return new ItemResponseModel() { Message = "Record Updation Failed", StatusCode = HttpStatusCode.BadRequest };
            }
        }
        /// To delete data in item 
        [HttpDelete]
        [Route("DeleteItem")]
        public ItemResponseModel DeleteItem(int id)
        {
            try
            {
                var result = _itemRepository.DeleteItem(id);
                if (result == null)
                {
                    return new ItemResponseModel() { Message = "Record Deletion Failed", StatusCode = HttpStatusCode.BadRequest };
                }
                return result;
            }
            catch (Exception Ex)
            {
                return new ItemResponseModel() { Message = "Record Deletion Failed", StatusCode = HttpStatusCode.BadRequest };
            }
        }
        #endregion
    }
}
