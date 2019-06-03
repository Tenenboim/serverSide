using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double AddressPointX { get; set; }
        public double AddressPointY { get; set; }
        public System.DateTime DateFound { get; set; }
        public bool LostOrFound { get; set; }
        public bool WasDone { get; set; }
        public bool CleverAgent { get; set; }

    }
}
