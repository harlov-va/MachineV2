using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Machine.BLL.Interfaces;
using Machine.BLL.DTO;
using AutoMapper;
using Machine.WEB.Models;

namespace Machine.WEB.Controllers
{
    public class AdminController : Controller
    {
        IOrderService orderService;
        List<DrinkViewModel> drinks;
        List<CoinViewModel> coins;
        public AdminController(IOrderService serv)
        {
            orderService = serv;
            IEnumerable<DrinkDTO> drinkDtos = orderService.GetDrinks();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DrinkDTO, DrinkViewModel>()).CreateMapper();
            drinks = mapper.Map<IEnumerable<DrinkDTO>, List<DrinkViewModel>>(drinkDtos);
            IEnumerable<CoinDTO> coinDtos = orderService.GetCoins();
            var mappercoin = new MapperConfiguration(cfg => cfg.CreateMap<CoinDTO, CoinViewModel>()).CreateMapper();
            coins = mappercoin.Map<IEnumerable<CoinDTO>, List<CoinViewModel>>(coinDtos);
        }
        public ViewResult Index()
        {
            return View();
        }
        public ViewResult Drink()
        {
            return View(drinks);
        }
        public ViewResult Edit(int productId)
        {
            DrinkViewModel drink = drinks.FirstOrDefault(p => p.ProductID == productId);
            return View(drink);
        }
        [HttpPost]
        public ActionResult Edit(DrinkViewModel drink, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    drink.ImageMimeType = image.ContentType;
                    drink.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(drink.ImageData, 0, image.ContentLength);
                }
                orderService.SaveDrinkDTO(new DrinkDTO
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
                TempData["message"] = string.Format("{0} has been saved", drink.Name);
                return RedirectToAction("Drink");
            }
            else
            {
                // there is something wrong with the data values
                return View(drink);
            }
        }
        [HttpPost]
        public ActionResult Delete(int productId)
        {
            DrinkDTO deletedProduct = orderService.DeleteDrinkDTO(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedProduct.Name);
            }
            return RedirectToAction("Drink");
        }
        public ViewResult Create()
        {
            return View("Edit", new DrinkViewModel());
        }
        public ViewResult Coin()
        {
            return View(coins);
        }

        public ViewResult EditCoin(int CoinID)
        {
            CoinViewModel coin = coins
            .FirstOrDefault(p => p.CoinID == CoinID);
            return View(coin);
        }
        [HttpPost]
        public ActionResult EditCoin(CoinViewModel coin)
        {
            if (ModelState.IsValid)
            {
                orderService.SaveCoinDTO(new CoinDTO
                {
                    CoinID = coin.CoinID,
                    SNameCoin = coin.SNameCoin,
                    SNameNumberCoin = coin.SNameNumberCoin,
                    iCountCoin = coin.iCountCoin,
                    BDontCoin = coin.BDontCoin
                });
                TempData["message"] = string.Format("{0} has been saved", coin.SNameCoin);
                return RedirectToAction("Coin");
            }
            else
            {
                // there is something wrong with the data values
                return View(coin);
            }
        }
    }
}