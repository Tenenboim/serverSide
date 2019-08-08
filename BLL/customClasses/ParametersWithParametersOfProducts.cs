using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL.customClasses
{
    public class ParametersWithParametersOfProducts
    {
        public int CategoryId { get; set; }
        public int ParameterId { get; set; }
        public string ParameterName { get; set; }
        public string ParameterOfProductValue { get; set; }
    }
}
