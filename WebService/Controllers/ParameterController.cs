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
       
    }
}
