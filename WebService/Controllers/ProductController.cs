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
            ProductDTO p = BLL.ProductFunctions.FunctionsToaddNewProduct(product, ParameterOfProductAreExist, NewParameters, NewParameterOfProduct);
            return Ok(p);
        }
        [Route("EditProduct")]
        [HttpPost]
        public IHttpActionResult EditProduct([FromBody] JObject dynamicObj)

        {

            List<BLL.customClasses.ParametersWithParametersOfProducts> parametersOfCategoryWithParametersOfProduct = dynamicObj["parametersOfCategoryWithParametersOfProduct"].ToObject<List<BLL.customClasses.ParametersWithParametersOfProducts>>();
            DTO.ProductDTO product = dynamicObj["product"].ToObject<ProductDTO>();
            List<DTO.ParameterOfProductDTO> ParameterOfProductAreExist = dynamicObj["ParameterOfProductAreExist"].ToObject<List<ParameterOfProductDTO>>();
            List<DTO.ParameterDTO> NewParameters = dynamicObj["NewParameters"].ToObject<List<ParameterDTO>>();
          
            List<DTO.ParameterOfProductDTO> NewParameterOfProduct = dynamicObj["NewParameterOfProduct"].ToObject<List<ParameterOfProductDTO>>();
            product = BLL.ProductFunctions.FunctionsToEditProduct(product, ParameterOfProductAreExist, NewParameters, NewParameterOfProduct, parametersOfCategoryWithParametersOfProduct);
            if (product != null)
            {
                return Ok(product);
            }
            return BadRequest("Edit Product failed");
        }
        [Route("getProduct")]
        [HttpGet]
        public IHttpActionResult getProduct(int ProductId)
        {
           ProductDTO u = BLL.ProductFunctions.getProduct(ProductId);
            if (u != null)
                return Ok(u);
            return BadRequest("the product wasn't found!"); ;
        }
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
        [Route("getMatches")]
        [HttpGet]
        public IHttpActionResult getMatches(int ProductId)
        {
            List<BLL.customClasses.ClassForMatches> m = BLL.ProductFunctions.search(ProductId);
            if (m != null)
                return Ok(m);
            return BadRequest("matches werent found");
        }

        [Route("getMatchesWithoutParameters")]
        [HttpPost]
        public IHttpActionResult getMatchesWithoutParameters(ProductDTO product)
        {
            List<BLL.customClasses.ClassForMatches> m = BLL.ProductFunctions.searchMatchesWithoutParameters(product);
            if (m != null)
                return Ok(m);
            return BadRequest("matches werent found");
        }
    }
}
