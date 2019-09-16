using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using DTO;
using BLL;

namespace WebService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/parameter")]
    public class ParameterController : ApiController
    {
        //פונקצית שליפת פרמטרים מתאימים לקטגוריה/תת קטגוריה
        [Route("getParametersOfCategory")]
        [HttpGet]
        public IHttpActionResult getParametersOfCategory(int categoryId)
        {
            List<ParameterDTO> l = BLL.ParameterFunctions.getParametersOfCategory(categoryId);
            if (l != null)
                return Ok(l);
            return BadRequest();
        }
        [Route("getAllParameters")]
        [HttpGet]
        public IHttpActionResult getAllParameters()
        {
            List<ParameterDTO> l = BLL.ParameterFunctions.getAllParameters();
            if (l != null)
                return Ok(l);
            return BadRequest();
        }
        [Route("deleteParameter")]
        [HttpGet]
        public IHttpActionResult deleteParameter(int ParameterId)
        {
            string l = ParameterFunctions.deleteParameter(ParameterId);
            if (l != null)
                return Ok(l);
            return BadRequest("delete parametr failed");
        }
        [Route("tryr")]
        [HttpGet]
        public IHttpActionResult tryr(double fromX, double fromY, double toX, double toY)
        {
            var l = BLL.ProductFunctions.GetDistanceAndDuration(fromX,fromY,toX,toY);
            int space1 = l.Duration.IndexOf(' ');
            int space2 = l.Duration.IndexOf(' ', space1 + 1);
            if (space2 < 0)
                space2 = l.Duration.IndexOf("", space1 + 1);
            string firstPart = l.Duration.Substring(0, space1);
            string secondPart = l.Duration.Substring(space1 + 1, space2);
            if (l != null)
                return Ok(l);
            return BadRequest("delete parametr failed");
        }
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
