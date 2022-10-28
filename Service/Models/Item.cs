using System.ComponentModel.DataAnnotations;

namespace Service.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public Int32 ItemCategoryId { get; set; }
    }
}
