using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            var response = _clientCall.GetAsync(_baseUrl+ "GetItemCategory");
            ItemCatergory = JsonConvert.DeserializeObject<List<ItemCategory>>(response.Content.ReadAsStringAsync().Result);
            return View(ItemCatergory);
        }

        public IActionResult Add()
        {
            return View("ItemCategoeyAddEdit", new ItemCategory());
        }

        public IActionResult Edit(int id)
        {
            ItemCategory ItemCatergory = new ItemCategory();
            var response = _clientCall.GetAsync(_baseUrl + "GetItemCategoryById?id="+id);
            ItemCatergory = JsonConvert.DeserializeObject<ItemCategory>(response.Content.ReadAsStringAsync().Result);
            return View("ItemCategoeyAddEdit", ItemCatergory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveItemCategory([FromBody] ItemCategory itemCategory)
        {
            StringContent requestContent;
            var modifiedAssetJSON = JsonConvert.SerializeObject(itemCategory);
            requestContent = new StringContent(modifiedAssetJSON, Encoding.UTF8, "application/json");
            var response = _clientCall.PostAsync(_baseUrl+ "InsertItemCategory", requestContent);
            var result = response.Content.ReadAsStringAsync().Result;
            TempData["error"] = "Failed";
            TempData["success"] = "success";
            return RedirectToAction("Index", "ItemCategory");
        }
        public IActionResult UpdateItemCategory(ItemCategory itemCategory)
        {
            StringContent requestContent;
            var modifiedAssetJSON = JsonConvert.SerializeObject(itemCategory);
            requestContent = new StringContent(modifiedAssetJSON, Encoding.UTF8, "application/json");
            var response = _clientCall.PutAsync(_baseUrl+ "UpdateItemCategory", requestContent);
            var result = response.Content.ReadAsStringAsync().Result;
            TempData["error"] = "Failed";
            TempData["success"] = "success";
            return RedirectToAction("Index", "ItemCategory");
        }
    }
}
