using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using web.Common;
using web.Models;

namespace web.Controllers
{
    public class ItemCategoryController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string _baseUrl;
        HttpClientCall _clientCall = new HttpClientCall();
        public ItemCategoryController(IConfiguration config)
        {
            _config = config;
            _baseUrl = _config.GetValue<string>("BaseApiUrl").ToString();
        }
        public IActionResult Index()
        {
            List<ItemCategory> ItemCatergory = new List<ItemCategory>();
            var response = _clientCall.GetAsync(_baseUrl+ "ItemCategory/GetItemCategory");
            ItemCatergory = JsonConvert.DeserializeObject<List<ItemCategory>>(response.Content.ReadAsStringAsync().Result);
            return View(ItemCatergory);
        }

        public IActionResult Add()
        {
            return View("ItemCategoryAddEdit", new ItemCategory());
        }

        public IActionResult Edit(int id)
        {
            ItemCategory ItemCatergory = new ItemCategory();
            var response = _clientCall.GetAsync(_baseUrl + "ItemCategory/GetItemCategoryById?id=" + id);
            ItemCatergory = JsonConvert.DeserializeObject<ItemCategory>(response.Content.ReadAsStringAsync().Result);
            return View("ItemCategoryAddEdit", ItemCatergory);
        }
        public IActionResult Delete(int id)
        {
            ItemCategory ItemCatergory = new ItemCategory();
            var response = _clientCall.GetAsync(_baseUrl + "ItemCategory/GetItemCategoryById?id=" + id);
            ItemCatergory = JsonConvert.DeserializeObject<ItemCategory>(response.Content.ReadAsStringAsync().Result);
            return View("ItemCategoryDelete", ItemCatergory);
        }

        public IActionResult SaveItemCategory(ItemCategory itemCategory)
        {
            StringContent requestContent;
            var modifiedAssetJSON = JsonConvert.SerializeObject(itemCategory);
            requestContent = new StringContent(modifiedAssetJSON, Encoding.UTF8, "application/json");
            var response = _clientCall.PostAsync(_baseUrl+ "ItemCategory/InsertItemCategory", requestContent);
            var ItemCategoryResponseModel = JsonConvert.DeserializeObject< ItemCategoryResponseModel>(response.Content.ReadAsStringAsync().Result);
            if (ItemCategoryResponseModel != null)
            {
                if (ItemCategoryResponseModel.StatusCode == HttpStatusCode.OK)
                {
                    TempData["success"] = ItemCategoryResponseModel.Message;
                }
                else
                {
                    TempData["error"] = ItemCategoryResponseModel.Message;
                    return View("ItemCategoryAddEdit", itemCategory);
                }
            }
            return RedirectToAction("Index", "ItemCategory");
        }
        public IActionResult UpdateItemCategory(ItemCategory itemCategory)
        {
            StringContent requestContent;
            var modifiedAssetJSON = JsonConvert.SerializeObject(itemCategory);
            requestContent = new StringContent(modifiedAssetJSON, Encoding.UTF8, "application/json");
            var response = _clientCall.PutAsync(_baseUrl+ "ItemCategory/UpdateItemCategory", requestContent);
            var ItemCategoryResponseModel = JsonConvert.DeserializeObject<ItemCategoryResponseModel>(response.Content.ReadAsStringAsync().Result);

            if (ItemCategoryResponseModel != null)
            {
                if (ItemCategoryResponseModel.StatusCode == HttpStatusCode.OK)
                {
                    TempData["success"] = ItemCategoryResponseModel.Message;
                }
                else
                {
                    TempData["error"] = ItemCategoryResponseModel.Message;
                    return View("ItemCategoryAddEdit", itemCategory);
                }
            }
            return RedirectToAction("Index", "ItemCategory");
        }

        public IActionResult DeleteItemCategory(int id)
        {
            var response = _clientCall.DeleteAsync(_baseUrl + "ItemCategory/DeleteItemCategory?id=" + id);
            var ItemCategoryResponseModel = JsonConvert.DeserializeObject<ItemCategoryResponseModel>(response.Content.ReadAsStringAsync().Result);

            if (ItemCategoryResponseModel != null)
            {
                if (ItemCategoryResponseModel.StatusCode == HttpStatusCode.OK)
                {
                    TempData["success"] = ItemCategoryResponseModel.Message;
                }
                else
                {
                    TempData["error"] = ItemCategoryResponseModel.Message;
                    return RedirectToAction("Delete", id);
                }
            }
            return RedirectToAction("Index", "ItemCategory");
        }
        
    }
}
