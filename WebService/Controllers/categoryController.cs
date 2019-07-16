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
    }
}

