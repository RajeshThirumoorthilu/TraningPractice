using Service.Models;
using System.Net;

namespace Service.Common
{
    public class ItemCategoryResponseModel
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<ItemCatergory> Model { get; set; }
    }
    public class ItemResponseModel
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<Item> Model { get; set; }
    }
}
