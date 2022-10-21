using System.ComponentModel.DataAnnotations;

namespace Service.Models
{
    public class ItemCatergory
    {
        [Key]
        public Int32 ItemCategoryId { get; set; }
        public string ItemCategoryName { get; set; }
        public string ItemDescription { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
