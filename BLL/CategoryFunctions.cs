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
        static AshavatAvedaEntities db = new AshavatAvedaEntities();
        //פןנקציה שמחזירה את כל הקטגוריות
        public static List<DTO.CategoryDTO> getAllCategories()
        {
            List<DTO.CategoryDTO> categories =new List<CategoryDTO>();
            foreach(var item in db.Categories.ToList())
            {
                categories.Add(BLL.Convertions.CategoryToDto(item));
            }
            return categories;
        }
        //פןנקציה שמחזירה את כל תתי הקטגוריות
        //public static List<DTO.CategoryDTO> getSubCategories(int categoryId)
        //{


        //    List<DTO.CategoryDTO> categories = new List<CategoryDTO>();
        //    foreach (var item in db.Categories.Where(c=>c.ParentId==categoryId).ToList())
        //    {
        //        categories.Add(BLL.Convertions.CategoryToDto(item));
        //    }
        //    return categories;
        //}

        //פונקצית הוספת קטגוריה(רק למנהל ניתנת אפשרות)ז
        public static DTO.CategoryDTO AddCategory(DTO.CategoryDTO category)
        {
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
    }
}
