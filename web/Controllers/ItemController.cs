using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NLog;
using NLog.Web;
using System.Net;
using System.Text;
using web.Common;
using web.Models;

namespace web.Controllers
{
    public class ItemController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string _baseUrl;
        private Logger _logger = LogManager.GetCurrentClassLogger();    
        HttpClientCall _clientCall = new HttpClientCall();
        public ItemController(IConfiguration config)
        {
            _config = config;
            _baseUrl = _config.GetValue<string>("BaseApiUrl").ToString();
        }
        public IActionResult Index()
        {
            
            _logger.Info(" ------------ ItemController Index Method Starts ------------ ");
            
            List<Item> Item = new List<Item>();
            var response = _clientCall.GetAsync(_baseUrl + "Item/GetItem");
            Item = JsonConvert.DeserializeObject<List<Item>>(response.Content.ReadAsStringAsync().Result);
            
            if (Item != null)
            {
                foreach (var i in Item)
                {
                    _clientCall = new HttpClientCall();
                    ItemCategory ItemCatergory = new ItemCategory();

                    var response1 = _clientCall.GetAsync(_baseUrl + "ItemCategory/GetItemCategoryById?id=" + i.ItemCategoryId);
                    ItemCatergory = JsonConvert.DeserializeObject<ItemCategory>(response1.Content.ReadAsStringAsync().Result);
                    i.ItemCategoryName = ItemCatergory.ItemCategoryName;
                }

                _logger.Info(" ------------ ItemController Index Method ends ------------ ");

                return View(Item);
            }
            
            _logger.Info(" ------------ ItemController Index Method ends ------------ ");
            
            return View(Item);
        }
        public IActionResult Add()
        {
            Item item = new Item();

            List<ItemCategory> ItemCatergory = new List<ItemCategory>();
            var response = _clientCall.GetAsync(_baseUrl + "ItemCategory/GetItemCategory");
            ItemCatergory = JsonConvert.DeserializeObject<List<ItemCategory>>(response.Content.ReadAsStringAsync().Result);

            if (ItemCatergory != null)
            {
                item.ItemCategorydropdown = ItemCatergory.Select(m => new SelectListItem { Text = m.ItemCategoryName, Value = m.ItemCategoryId.ToString() });
            }
            else
            {
                item.ItemCategorydropdown = new List<SelectListItem>();
            }
            return View("ItemAddEdit", item);
        }
        public IActionResult Edit(int id)
        {
            Item Item = new Item();
            var response = _clientCall.GetAsync(_baseUrl + "Item/GetItemById?id=" + id);
            Item = JsonConvert.DeserializeObject<Item>(response.Content.ReadAsStringAsync().Result);

            List<ItemCategory> ItemCatergory = new List<ItemCategory>();
            _clientCall = new HttpClientCall();

            var response1 = _clientCall.GetAsync(_baseUrl + "ItemCategory/GetItemCategory");
            ItemCatergory = JsonConvert.DeserializeObject<List<ItemCategory>>(response1.Content.ReadAsStringAsync().Result);

            if (ItemCatergory != null)
            {
                Item.ItemCategorydropdown = ItemCatergory.Select(m => new SelectListItem { Text = m.ItemCategoryName, Value = m.ItemCategoryId.ToString() });
            }
            else
            {
                Item.ItemCategorydropdown = new List<SelectListItem>();
            }

            return View("ItemAddEdit", Item);
        }
        public IActionResult Delete(int id)
        {
            Item Item = new Item();
            var response = _clientCall.GetAsync(_baseUrl + "Item/GetItemById?id=" + id);
            Item = JsonConvert.DeserializeObject<Item>(response.Content.ReadAsStringAsync().Result);

            List<ItemCategory> ItemCatergory = new List<ItemCategory>();
            _clientCall = new HttpClientCall();

            var response1 = _clientCall.GetAsync(_baseUrl + "ItemCategory/GetItemCategory");
            ItemCatergory = JsonConvert.DeserializeObject<List<ItemCategory>>(response1.Content.ReadAsStringAsync().Result);

            if (ItemCatergory != null)
            {
                Item.ItemCategorydropdown = ItemCatergory.Select(m => new SelectListItem { Text = m.ItemCategoryName, Value = m.ItemCategoryId.ToString() });
            }
            else
            {
                Item.ItemCategorydropdown = new List<SelectListItem>();
            }
            return View("ItemDelete", Item);
        }
        public IActionResult SaveItem(Item item)
        {
            StringContent requestContent;
            var modifiedAssetJSON = JsonConvert.SerializeObject(item);
            requestContent = new StringContent(modifiedAssetJSON, Encoding.UTF8, "application/json");
            var response = _clientCall.PostAsync(_baseUrl + "Item/InsertItem", requestContent);
            var ItemResponseModel = JsonConvert.DeserializeObject<ItemResponseModel>(response.Content.ReadAsStringAsync().Result);
            if (ItemResponseModel != null)
            {
                if (ItemResponseModel.StatusCode == HttpStatusCode.OK)
                {
                    TempData["success"] = ItemResponseModel.Message;
                }
                else
                {
                    TempData["error"] = ItemResponseModel.Message;
                    return View("ItemAddEdit", item);
                }
            }
            return RedirectToAction("Index", "Item");
        }
        public IActionResult UpdateItem(Item item)
        {
            StringContent requestContent;
            var modifiedAssetJSON = JsonConvert.SerializeObject(item);
            requestContent = new StringContent(modifiedAssetJSON, Encoding.UTF8, "application/json");
            var response = _clientCall.PutAsync(_baseUrl + "Item/UpdateItem", requestContent);
            var ItemResponseModel = JsonConvert.DeserializeObject<ItemResponseModel>(response.Content.ReadAsStringAsync().Result);

            if (ItemResponseModel != null)
            {
                if (ItemResponseModel.StatusCode == HttpStatusCode.OK)
                {
                    TempData["success"] = ItemResponseModel.Message;
                }
                else
                {
                    TempData["error"] = ItemResponseModel.Message;
                    return View("ItemAddEdit", item);
                }
            }
            return RedirectToAction("Index", "Item");
        }

        public IActionResult DeleteItem(int id)
        {
            var response = _clientCall.DeleteAsync(_baseUrl + "Item/DeleteItem?id=" + id);
            var ItemResponseModel = JsonConvert.DeserializeObject<ItemResponseModel>(response.Content.ReadAsStringAsync().Result);

            if (ItemResponseModel != null)
            {
                if (ItemResponseModel.StatusCode == HttpStatusCode.OK)
                {
                    TempData["success"] = ItemResponseModel.Message;
                }
                else
                {
                    TempData["error"] = ItemResponseModel.Message;
                    return RedirectToAction("Delete", id);
                }
            }
            return RedirectToAction("Index", "Item");
        }
    }
}
