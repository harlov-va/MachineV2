using Machine.DAL.Interfaces;
using Machine.DAL.Entities;
using Machine.DAL.EF;
using System.Linq;

namespace Machine.DAL.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Drink> Drinks
        {
            get { return context.Drinks; }
        }
        public IQueryable<Coin> Coins
        {
            get { return context.Coins; }
        }
        public void SaveProduct(Drink drink)
        {
            if (drink.ProductID == 0)
            {
                context.Drinks.Add(drink);
            }
            else
            {
                Drink dbEntry = context.Drinks.Find(drink.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = drink.Name;
                    dbEntry.Description = drink.Description;
                    dbEntry.Price = drink.Price;
                    dbEntry.iCount = drink.iCount;
                    dbEntry.BThereIsDrink = drink.BThereIsDrink;
                    dbEntry.ImageData = drink.ImageData;
                    dbEntry.ImageMimeType = drink.ImageMimeType;
                }
            }
            context.SaveChanges();
        }
        public void SaveCoin(Coin coin)
        {
            if (coin.CoinID == 0)
            {
                context.Coins.Add(coin);
            }
            else
            {
                Coin dbEntry = context.Coins.Find(coin.CoinID);
                if (dbEntry != null)
                {
                    dbEntry.SNameCoin = coin.SNameCoin;
                    dbEntry.iCountCoin = coin.iCountCoin;
                    
                    dbEntry.BDontCoin = coin.BDontCoin;
                    dbEntry.SNameNumberCoin = coin.SNameNumberCoin;
                }
            }
            context.SaveChanges();
        }
        public Drink DeleteDrink(int productID)
        {
            Drink dbEntry = context.Drinks.Find(productID);
            if (dbEntry != null)
            {
                context.Drinks.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
        }
}