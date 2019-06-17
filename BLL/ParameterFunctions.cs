using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public static class ParameterFunctions
    {
        static AshavatAvedaEntities db = new AshavatAvedaEntities();

        public static List<ParameterDTO> getParametersOfCategory(int CategoryId)
        {
            List<ParameterDTO> l1 = new List<ParameterDTO>();
            DAL.Category d = db.Categories.FirstOrDefault(p => p.CategoryId == CategoryId);
            int id=0;
            
            if(d == null)
            { id = d.ParentId.Value; }
             List<Parameter> l = db.Parameters.Where(p => p.CategoryId == CategoryId ||id!=0 &&  p.CategoryId ==id).ToList(); 
            foreach(var item in l)
            {
                l1.Add(BLL.Convertions.ParameterToDto(item));
            }
            return l1;
        }
    }
}
