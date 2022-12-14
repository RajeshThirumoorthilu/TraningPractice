using Microsoft.EntityFrameworkCore;
using Service.Models;

namespace Service.Common
{
    public class DatabaseContext : DbContext
    {
        //public DatabaseContext()
        //{

        //}
        public DatabaseContext(DbContextOptions options) : base(options) { }
        public virtual DbSet<ItemCatergory> itemCatergories{get;set;}
        public virtual DbSet<Item> items{get;set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=INDRJVM\\Thirumr;Database=Sample;Integrated Security=True");
            }
        }
    }
}
