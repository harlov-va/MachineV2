using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Machine.WEB.Models
{
    public class CoinViewModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int CoinID { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string SNameCoin { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string SNameNumberCoin { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a positive count")]
        public int iCountCoin { get; set; }
        public bool BDontCoin { get; set; }
    }
}