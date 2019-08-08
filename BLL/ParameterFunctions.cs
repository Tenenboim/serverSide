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

        //פונקציה שמחזירה את כל הפרמטרים הקשורים לקוד הקטגוריה שהתקבלה
        //"כולל פרמטרים הקשורים לקטגוריה "כל הקטגוריות
        public static List<ParameterDTO> getParametersOfCategory(int CategoryId)
        {
            List<ParameterDTO> l1 = new List<ParameterDTO>();
            DAL.Category d = new Category();
            d = db.Categories.First(p => p.CategoryId == CategoryId);
            int id = 0;

            if (d != null&&d.ParentId!=null)
            {
                id = d.ParentId.Value;
            }

            //p.CategoryId==1 קוד הקטגוריה ששמה- כל הקטגוריות
            List<Parameter> l = db.Parameters.Where(p => p.CategoryId == CategoryId || id != 0 && p.CategoryId == id||p.CategoryId==1).ToList();
            foreach (var item in l)
            {
                l1.Add(BLL.Convertions.ParameterToDto(item));
            }
            return l1;
        }

        //פונקציה המחזירה את כל הפרמטרים
        public static List<ParameterDTO> getAllParameters()
        {
            List<DTO.ParameterDTO> parameters = new List<ParameterDTO>();
            db.Parameters.ToList().ForEach(item => parameters.Add(BLL.Convertions.ParameterToDto(item)));
            return parameters;
        }

        public static string deleteParameter(int ParameterId)
        {
            DAL.Parameter parameter = db.Parameters.FirstOrDefault(p => p.ParameterId == ParameterId);
            if (parameter != null)
            { //הקשורים לפרמטר ההולך להימחק parameterOfProduct  מחיקת כל ה
                db.ParameteOfProducts.RemoveRange(db.ParameteOfProducts.Where(p => p.ParameterId == ParameterId));
                db.SaveChanges();
                db.Parameters.Remove(parameter);
                db.SaveChanges();
                return "ok";
            }
            return null;
        }
    }
}
