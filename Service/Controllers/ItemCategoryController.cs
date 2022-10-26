using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Common;
using Service.IRepository;
using Service.Models;
using Service.Repository;
using System.Net;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCategoryController : ControllerBase
    {
        private readonly IItemCategoryRepository _itemCategoryRepository;

        private readonly DatabaseContext _context;

        public ItemCategoryController(IItemCategoryRepository itemCategoryRepository)
        {
            _itemCategoryRepository = itemCategoryRepository;
               //_itemCategoryRepository =new ItemcategoryRepository(_context);
        }

        #region apis

        /// To get the item category list
        [HttpGet]
        [Route("GetItemCategory")]
        public IActionResult GetItemCategory()
        {
            try
            {
                var itemcategory = _itemCategoryRepository.GetItemCatergory();
                if(itemcategory == null)
                {
                    return NotFound();  
                }
                return Ok(itemcategory);
            }
            catch(Exception Ex)
            {
                return BadRequest();
            }
        }
        /// To get the item category list by id
        [HttpGet]
        [Route("GetItemCategoryById")]
        public IActionResult GetItemCategoryById(int id)
        {
            try
            {
                var itemcategory = _itemCategoryRepository.GetItemCatergory(id);
                if (itemcategory == null)
                {
                    return NotFound();
                }
                return Ok(itemcategory);
            }
            catch (Exception Ex)
            {
                return BadRequest();
            }
        }
        /// To insert or push data in item category
        [HttpPost]
        [Route("InsertItemCategory")]
        public ItemCategoryResponseModel InsertItemCategory(ItemCatergory itemCatergory)
        {
            try
            {
                var result = _itemCategoryRepository.InsertItemCategory(itemCatergory);
                if(result == null)
                {
                    return new ItemCategoryResponseModel() { Message= "Record Added Failed",StatusCode= HttpStatusCode.BadRequest };
                }
                return result;
            }
            catch(Exception Ex)
            {
                return new ItemCategoryResponseModel() { Message = "Record Added Failed", StatusCode = HttpStatusCode.BadRequest };
            }
        }
        /// To update data in item category
        [HttpPut]
        [Route("UpdateItemCategory")]
        public ItemCategoryResponseModel UpdateItemCategory(ItemCatergory itemCatergory)
        {
            try
            {
                var result = _itemCategoryRepository.UpdateItemCategory(itemCatergory);
                if (result == null)
                {
                    return new ItemCategoryResponseModel() { Message = "Record Updation Failed", StatusCode = HttpStatusCode.BadRequest };
                }
                return result;
            }
            catch (Exception Ex)
            {
                return new ItemCategoryResponseModel() { Message = "Record Updation Failed", StatusCode = HttpStatusCode.BadRequest };
            }
        }
        /// To delete data in item category
        [HttpDelete]
        [Route("DeleteItemCategory")]
        public ItemCategoryResponseModel DeleteItemCategory(int id)
        {
            try
            {
                var result = _itemCategoryRepository.DeleteItemCategory(id);
                if (result == null)
                {
                    return new ItemCategoryResponseModel() { Message = "Record Deletion Failed", StatusCode = HttpStatusCode.BadRequest };
                }
                return result;
            }
            catch (Exception Ex)
            {
                return new ItemCategoryResponseModel() { Message = "Record Deletion Failed", StatusCode = HttpStatusCode.BadRequest };
            }
        }
        #endregion
    }
}
