using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Machine.BLL.DTO
{
    public class DrinkDTO
    {
        //[Key]
        
        public int ProductID { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public int Price { get; set; }
        
        public int iCount { get; set; }
        public bool BThereIsDrink { get; set; }
        public byte[] ImageData { get; set; }
        
        public string ImageMimeType { get; set; }
    }
}