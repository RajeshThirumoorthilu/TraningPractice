using System.Net;
using System.Net.NetworkInformation;
using web.Models;

namespace web.Common
{
    public class ItemCategoryResponseModel
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<ItemCategory> Model { get; set; }
    }
    public class ItemResponseModel
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<Item> Model { get; set; }
    }
}
