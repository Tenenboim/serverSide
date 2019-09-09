using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.customClasses
{
    public class ClassForMatches
    {
        public int ProductId { get; set; }
        public string UserName { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public Nullable<double> AddressPointX { get; set; }
        public Nullable<double> AddressPointY { get; set; }
        public string AddressDescription { get; set; }
        public DateTime DateFound { get; set; }
        public bool LostOrFound { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }

    }
}
