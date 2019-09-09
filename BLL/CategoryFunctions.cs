using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BLL
{
    public static class CategoryFunctions
    {
        static Entities db = new Entities();
        public static List<DTO.CategoryDTO> getAllCategories()
        {
            //פןנקציה שמחזירה את כל הקטגוריות מלבד הקטגוריה ששמה כל הקטגוריות

            List<DTO.CategoryDTO> categories =new List<CategoryDTO>();
            foreach(var item in db.Categories.ToList())
            {
                if(item.CategoryName!= "כל הקטגוריות")
                categories.Add(BLL.Convertions.CategoryToDto(item));
            }
            return categories;
        }

        public static List<DTO.CategoryDTO> getAllAllCategories()
        {
            //פןנקציה שמחזירה את כל הקטגוריות מיועד עבור הוספת פרמטר חדש עבור המנהל

            List<DTO.CategoryDTO> categories = new List<CategoryDTO>();
            foreach (var item in db.Categories.ToList())
            {
                    categories.Add(BLL.Convertions.CategoryToDto(item));
            }
            return categories;
        }
    
        public static DTO.CategoryDTO AddCategory(DTO.CategoryDTO category)
        {
            //פונקצית הוספת קטגוריה(רק למנהל ניתנת אפשרות)ז

            DAL.Category c1 = BLL.Convertions.CategoryDtoToDAL(category);
            DAL.Category c2 = db.Categories.FirstOrDefault(c => c.CategoryName == c1.CategoryName);
            if (c2 == null)
            {
               
                db.Categories.Add(c1);
                db.SaveChanges();
                return BLL.Convertions.CategoryToDto(c1);

            }
            else
            {
                return null;
            }

        }
        public static DTO.CategoryDTO editCategory(int CategoryId,string CategoryName)
        {
            DAL.Category c = db.Categories.FirstOrDefault(t => t.CategoryId == CategoryId);
            c.CategoryName = CategoryName;
            db.SaveChanges();
            return BLL.Convertions.CategoryToDto(c);
        }

        public static CategoryDTO getCategoryById(int categoryId)
        {
            return BLL.Convertions.CategoryToDto( db.Categories.First(p => p.CategoryId == categoryId));
        }

        public static string getCategoryNameByID(int categoryId)
        {
            return db.Categories.FirstOrDefault(p => p.CategoryId == categoryId).CategoryName;
        }

       
    }
}
