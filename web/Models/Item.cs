using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace web.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public Int32 ItemCategoryId { get; set; }
        public string ItemCategoryName { get; set; }
        public IEnumerable<SelectListItem> ItemCategorydropdown { get; set; } = new List<SelectListItem>();
    }
}
