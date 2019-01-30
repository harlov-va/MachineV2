using Machine.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Machine.BLL.Interfaces
{
    public interface IOrderService
    {
        DrinkDTO GetDrink(int? id);
        DrinkDTO GetDrink(string name);
        CoinDTO GetCoin(int? id);
        IEnumerable<DrinkDTO> GetDrinks();
        IEnumerable<CoinDTO> GetCoins();
        void SaveDrinkDTO(string NameDrink);
        void SaveDrinkDTO(DrinkDTO drink);
        DrinkDTO DeleteDrinkDTO(int? id);
        void SaveCoinDTO(string NameNumberCoin);
        void SaveCoinDTO(string NameCoin, int ValueCountCoin);
        void SaveCoinDTO(CoinDTO coin);
        void CalculateRestOfMoney(int NumberCoin);

    }
}