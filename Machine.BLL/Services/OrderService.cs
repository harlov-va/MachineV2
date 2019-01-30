using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Machine.BLL.DTO;
using Machine.DAL.Entities;
using Machine.BLL.BusinessModels;
using Machine.DAL.Interfaces;

using Machine.BLL.Interfaces;
using AutoMapper;

namespace Machine.BLL.Services
{
    public class OrderService : IOrderService
    {
        private IProductRepository Database;

        public OrderService(IProductRepository productRepository)
        {
            this.Database = productRepository;
        }
        public IEnumerable<DrinkDTO> GetDrinks()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Drink, DrinkDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Drink>, List<DrinkDTO>>(Database.Drinks);
        }
        public IEnumerable<CoinDTO> GetCoins()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Coin, CoinDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Coin>, List<CoinDTO>>(Database.Coins);
        }
        public DrinkDTO GetDrink(int? id)
        {
            var drink = Database.Drinks.FirstOrDefault(p => p.ProductID == id.Value);
            return new DrinkDTO
            {
                ProductID = drink.ProductID,
                Name = drink.Name,
                Description = drink.Description,
                Price = drink.Price,
                iCount = drink.iCount,
                BThereIsDrink = drink.BThereIsDrink,
                ImageData = drink.ImageData,
                ImageMimeType = drink.ImageMimeType
            };
        }
        public DrinkDTO GetDrink(string name)
        {
            var drink = Database.Drinks.FirstOrDefault(p => p.Name == name);
            return new DrinkDTO
            {
                ProductID = drink.ProductID,
                Name = drink.Name,
                Description = drink.Description,
                Price = drink.Price,
                iCount = drink.iCount,
                BThereIsDrink = drink.BThereIsDrink,
                ImageData = drink.ImageData,
                ImageMimeType = drink.ImageMimeType
            };
        }
        public CoinDTO GetCoin(int? id)
        {
            var coin = Database.Coins.FirstOrDefault(p => p.CoinID == id.Value);
            return new CoinDTO
            {
                CoinID = coin.CoinID,
                SNameCoin = coin.SNameCoin,
                SNameNumberCoin = coin.SNameNumberCoin,
                iCountCoin = coin.iCountCoin,
                BDontCoin = coin.BDontCoin
            };
        }
        public void SaveDrinkDTO(string NameDrink)
        {
            Drink Drink = Database.Drinks.FirstOrDefault(d => d.Name == NameDrink);
            Drink.iCount--;
            if (Drink.iCount<=0)
            {
                Drink.iCount = 0;
                Drink.BThereIsDrink = false;
            }
            Database.SaveProduct(Drink);
        }
        public void SaveDrinkDTO(DrinkDTO drink)
        {
            if (drink.iCount <= 0)
            {
                drink.iCount = 0;
                drink.BThereIsDrink = false;
            }
            Database.SaveProduct(new Drink
            {
                ProductID = drink.ProductID,
                Name = drink.Name,
                Description = drink.Description,
                Price = drink.Price,
                iCount = drink.iCount,
                BThereIsDrink = drink.BThereIsDrink,
                ImageData = drink.ImageData,
                ImageMimeType = drink.ImageMimeType
            });
        }
        public DrinkDTO DeleteDrinkDTO(int? id)
        {
            if (id != null)
            {
                Drink drink = Database.DeleteDrink(id.Value);
                return (new DrinkDTO
                {
                    ProductID = drink.ProductID,
                    Name = drink.Name,
                    Description = drink.Description,
                    Price = drink.Price,
                    iCount = drink.iCount,
                    BThereIsDrink = drink.BThereIsDrink,
                    ImageData = drink.ImageData,
                    ImageMimeType = drink.ImageMimeType
                });
            }
            else return null;
        }
        public void SaveCoinDTO(string NameNumberCoin)
        {
            Coin coin = Database.Coins.FirstOrDefault(d => d.SNameNumberCoin == NameNumberCoin);
            coin.iCountCoin++;
            Database.SaveCoin(coin);
        }
        public void SaveCoinDTO(string NameCoin, int ValueCountCoin)
        {
            Coin coin = Database.Coins.FirstOrDefault(d => d.SNameCoin == NameCoin);
            coin.iCountCoin += ValueCountCoin;
            if (coin.iCountCoin <= 0)
            {
                coin.iCountCoin = 0;
                coin.BDontCoin = true;
            }
            Database.SaveCoin(coin);
        }
        public void SaveCoinDTO(CoinDTO coin)
        {
            if (coin.iCountCoin <= 0)
            {
                coin.iCountCoin = 0;
                coin.BDontCoin = true;
            }
            Database.SaveCoin(new Coin
            {
                CoinID = coin.CoinID,
                SNameCoin = coin.SNameCoin,
                SNameNumberCoin = coin.SNameNumberCoin,
                iCountCoin = coin.iCountCoin,
                BDontCoin = coin.BDontCoin
            });
        }
        public void CalculateRestOfMoney(int NumberCoin)
        {
            RestOfMoney rest = new RestOfMoney();
            foreach (KeyValuePair<int, int> item in rest.CalculateChange(NumberCoin))
            {
                switch (item.Key)
                {
                    case 1:
                        SaveCoinDTO("One", -item.Value);
                        break;
                    case 2:
                        SaveCoinDTO("Two", -item.Value);
                        break;
                    case 5:
                        SaveCoinDTO("Five", -item.Value);
                        break;
                    case 10:
                        SaveCoinDTO("Ten", -item.Value);
                        break;
                }
            }
        }
    }
}