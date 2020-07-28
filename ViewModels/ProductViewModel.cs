using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceAPI.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Details { get; set; }
        public int Quantity { get; set; }
        public int OriginalQuantity { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
    }
}