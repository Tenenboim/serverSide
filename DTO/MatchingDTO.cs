using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MatchingDTO
    {
        public int LostProductId { get; set; }
        public int FindProductId { get; set; }
        public bool Relevant { get; set; }
    }
}
