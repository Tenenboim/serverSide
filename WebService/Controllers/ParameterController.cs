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
        public IHttpActionResult tryr()
        {
            var l = trygoogle.GetDistanceAndDuration(32.078899, 34.835156, 32.084932, 34.835226000000034);
            if (l != null)
                return Ok(l);
            return BadRequest("delete parametr failed");
        }

    }
}
