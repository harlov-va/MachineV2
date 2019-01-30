using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Machine.BLL.DTO
{
    public class CoinDTO
    {
        [Key]
        
        public int CoinID { get; set; }
        
        public string SNameCoin { get; set; }
        
        public string SNameNumberCoin { get; set; }
        public int iCountCoin { get; set; }
        public bool BDontCoin { get; set; }
    }
}