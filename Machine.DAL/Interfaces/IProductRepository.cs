using System.Linq;
using Machine.DAL.Entities;

namespace Machine.DAL.Interfaces
{
    public interface IProductRepository
    {
        IQueryable<Drink> Drinks { get; }
        IQueryable<Coin> Coins { get; }
        void SaveProduct(Drink drink);
        void SaveCoin(Coin coin);
        Drink DeleteDrink(int productID);
    }
}