using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace Machine.DAL.Entities
{
    public class Coin
    {
        [Key]
        public int CoinID { get; set; }
        
        public string SNameCoin { get; set; }
        
        public string SNameNumberCoin { get; set; }
        public int iCountCoin { get; set; }
        public bool BDontCoin { get; set; }
    }
}