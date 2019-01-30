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
    public class HomeController : Controller
    {
        IOrderService orderService;
        public HomeController(IOrderService serv)
        {
            orderService = serv;
        }
        public void DefaultViewBag()
        {
            ViewBag.BDontOne = false;
            ViewBag.BDontTwo = false;
            ViewBag.BDontFive = false;
            ViewBag.BDontTen = false;
            foreach (var c in orderService.GetCoins())
            {
                if ((c.SNameCoin == "One") & (c.BDontCoin)) ViewBag.BDontOne = true;

                if ((c.SNameCoin == "Two") & (c.BDontCoin)) ViewBag.BDontTwo = true;

                if ((c.SNameCoin == "Five") & (c.BDontCoin)) ViewBag.BDontFive = true;

                if ((c.SNameCoin == "Ten") & (c.BDontCoin)) ViewBag.BDontTen = true;
            }
        }
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.SumMoney = int.Parse("0");
            ViewBag.RestOfMoney = 0;
            DefaultViewBag();
            if (Request.QueryString["id"] == "secret")
                return Redirect(Url.Action("Index", "Admin"));
            else
            {
                IEnumerable<DrinkDTO> drinkDtos = orderService.GetDrinks();
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DrinkDTO, DrinkViewModel>()).CreateMapper();
                var drinks = mapper.Map<IEnumerable<DrinkDTO>, List<DrinkViewModel>>(drinkDtos);
                return View(drinks);
            }
        }
        [HttpPost]
        public ActionResult Index(int SumMoney, string clickonbutton, string clickbuttoncoin, string buttonrestofmoney)
        {
            //ViewBag.Title = buttonrestofmoney;
            //int iSumInController = SumMoney;
            //int.TryParse(SumMoney, out iSumInController);
            ViewBag.RestOfMoney = SumMoney;
            ViewBag.SumMoney = SumMoney;
                //int.Parse(SumMoney);
            DefaultViewBag();
            if (!string.IsNullOrEmpty(clickbuttoncoin)) orderService.SaveCoinDTO(clickbuttoncoin.Split(' ')[0]);
            if (!string.IsNullOrEmpty(clickonbutton))
            {
                orderService.SaveDrinkDTO(clickonbutton);
                ViewBag.RestOfMoney = ViewBag.SumMoney - orderService.GetDrink(clickonbutton).Price;
                ViewBag.SumMoney = ViewBag.RestOfMoney;
            }
            if (!string.IsNullOrEmpty(buttonrestofmoney))
            {
                orderService.CalculateRestOfMoney(int.Parse(buttonrestofmoney.Split(' ')[0]));
                ViewBag.SumMoney = 0;
                ViewBag.RestOfMoney = 0;
            }

            IEnumerable<DrinkDTO> drinkDtos = orderService.GetDrinks();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DrinkDTO, DrinkViewModel>()).CreateMapper();
            var drinks = mapper.Map<IEnumerable<DrinkDTO>, List<DrinkViewModel>>(drinkDtos);
            return View(drinks);
        }
        public FileContentResult GetImage(int productId)
        {
            DrinkDTO prod = orderService.GetDrink(productId);
            if (prod != null)
            {
                DrinkViewModel prodTemp = new DrinkViewModel
                {
                    ProductID = prod.ProductID,
                    Name = prod.Name,
                    Description = prod.Description,
                    Price = prod.Price,
                    iCount = prod.iCount,
                    BThereIsDrink = prod.BThereIsDrink,
                    ImageData = prod.ImageData,
                    ImageMimeType = prod.ImageMimeType
                };
                return File(prodTemp.ImageData, prodTemp.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}