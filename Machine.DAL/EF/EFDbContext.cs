using System.Data.Entity;
using Machine.DAL.Entities; 
 
namespace Machine.DAL.EF
{
    public class EFDbContext : DbContext
    {
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Coin> Coins { get; set; }
    }
}