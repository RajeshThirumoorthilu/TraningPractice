namespace web.Models
{
    public class ItemCategory
    {
        public Int32 ItemCategoryId { get; set; }
        public string ItemCategoryName { get; set; }
        public string ItemDescription { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
