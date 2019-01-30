using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Machine.DAL.Entities
{
    public class Drink
    {
        [Key]
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