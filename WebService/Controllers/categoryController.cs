using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using DTO;
namespace WebService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/category")]
    public class categoryController : ApiController
    {
        [Route("getAllCategories")]
        [HttpGet]
        public  List<CategoryDTO> getAllCategories()
        {
            return BLL.CategoryFunctions.getAllCategories();
        }


        [Route("getAllAllCategories")]
        [HttpGet]
        public List<CategoryDTO> getAllAllCategories()
        {
            return BLL.CategoryFunctions.getAllAllCategories();
        }
        //[Route("getSubCategories/{categoryId}")]
        //[HttpGet]
        //public  List<CategoryDTO> getSubCategories(int categoryId)
        //{
        //    return BLL.CategoryFunctions.getSubCategories(categoryId);
        //}

        //AddCategory
        [Route("AddCategory")]
        [HttpPost]
        public IHttpActionResult AddCategory(CategoryDTO category)
        {
            CategoryDTO c = BLL.CategoryFunctions.AddCategory(category);
            if (c != null)
                return Ok(c);
            return BadRequest("the category is already exist!");
        }
        //EditCategory-only change the name
        [Route("EditCategory")]
        [HttpPost]
        public IHttpActionResult EditCategory(CategoryDTO Category)
        {
            CategoryDTO c = BLL.CategoryFunctions.editCategory(Category.CategoryId, Category.CategoryName);
            if (c != null)
                return Ok(c);
            return BadRequest("the edit category failed!");
        }
        //ומחחזירה את שם הקטגוריה id פונקציה שמקבלת
        //מיועדת לעריכת קטגוריה להצגת שם האב קטגוריה
        [Route("getCategoryNameByID")]
        [HttpGet]
        public IHttpActionResult getCategoryNameByID(int CategoryId)
        {
            string categoryName = BLL.CategoryFunctions.getCategoryNameByID(CategoryId);
            if (categoryName != null)
                return Ok(categoryName);
            return BadRequest("the category wasnt found");
        }

        [Route("getCategoryById")]
        [HttpGet]
        public IHttpActionResult getCategoryById(int CategoryId)
        {
            CategoryDTO category = BLL.CategoryFunctions.getCategoryById(CategoryId);
            if (category != null)
                return Ok(category);
            return BadRequest("the category wasnt found");
        }

    }
}

