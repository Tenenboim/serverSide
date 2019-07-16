using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BLL;
using DTO;

namespace WebService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        [Route("AddProduct")]
        [HttpPost]
        public IHttpActionResult AddProduct([FromBody] JObject productAndListsOfParameters)

        {


            DTO.ProductDTO product = productAndListsOfParameters["product"].ToObject<ProductDTO>();
            List<DTO.ParameterOfProductDTO> ParameterOfProductAreExist = productAndListsOfParameters["ParameterOfProductAreExist"].ToObject<List<ParameterOfProductDTO>>();
            List<DTO.ParameterDTO> NewParameters = productAndListsOfParameters["NewParameters"].ToObject<List<ParameterDTO>>();

            List<DTO.ParameterOfProductDTO> NewParameterOfProduct = productAndListsOfParameters["NewParameterOfProduct"].ToObject<List<ParameterOfProductDTO>>();
            string result = BLL.ProductFunctions.FunctionsToaddNewProduct(product, ParameterOfProductAreExist, NewParameters, NewParameterOfProduct);
            return Ok(result);
        }
        //[Route("EditProduct")]
        //[HttpPost]
        //public IHttpActionResult EditProduct([FromBody] JObject productAndListsOfParameters)

        //{

        //    List<BLL.customClasses.ParametersWithParametersOfProducts> parametersOfCategoryWithParametersOfProduct=
        //    DTO.ProductDTO product = productAndListsOfParameters["product"].ToObject<ProductDTO>();
        //    List<DTO.ParameterOfProductDTO> ParameterOfProductAreExist = productAndListsOfParameters["ParameterOfProductAreExist"].ToObject<List<ParameterOfProductDTO>>();
        //    List<DTO.ParameterDTO> NewParameters = productAndListsOfParameters["NewParameters"].ToObject<List<ParameterDTO>>();

        //    List<DTO.ParameterOfProductDTO> NewParameterOfProduct = productAndListsOfParameters["NewParameterOfProduct"].ToObject<List<ParameterOfProductDTO>>();
        //    string result = BLL.ProductFunctions.FunctionsToaddNewProduct(product, ParameterOfProductAreExist, NewParameters, NewParameterOfProduct);
        //    return Ok(result);
        //}
        //getLosts
        [Route("getLosts")]
        [HttpGet]
        public IHttpActionResult getLosts(int userId)
        {
            List<ProductDTO> u = BLL.ProductFunctions.getLosts(userId);
            if (u != null)
                return Ok(u);
            return BadRequest("there aren't losts!"); ;
        }
        //getFounds
        [Route("getFounds")]
        [HttpGet]
        public IHttpActionResult getFounds(int userId)
        {
            List<ProductDTO> u = BLL.ProductFunctions.getFounds(userId);
            if (u != null)
                return Ok(u);
            return BadRequest("there aren't Founds!"); ;
        }

        //getFounds
        [Route("getParametersWithValue/{productID}")]
        [HttpGet]
        public IHttpActionResult getParametersWithValue(int productID)
        {
            List<BLL.customClasses.ParametersWithParametersOfProducts> u = BLL.ParametersWithValues.getParametersWithValue(productID);
            if (u != null)
                return Ok(u);
            return BadRequest("there aren't Founds!"); ;
        }
    }
}
