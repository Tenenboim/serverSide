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
    }
}
