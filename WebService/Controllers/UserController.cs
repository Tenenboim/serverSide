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
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        //Login
        [Route("Login")]
        [HttpGet]
        public IHttpActionResult Login(string UserPhone)
        {
            UserDTO u = BLL.UserFunctions.Login(UserPhone);
            if (u != null)
                return Ok(u);
            return BadRequest("the user isnt exist!");
        }
        //Register
        [Route("Register")]
        [HttpPost]
        public IHttpActionResult Register(UserDTO user)
        {
            user.RoleId = 3;
            UserDTO u = BLL.UserFunctions.Register(user);
            if (u != null)
                return Ok(u);
            return BadRequest("the user is already exist!");
        }
        //UserList
        [Route("UserList")]
        [HttpGet]
        public IHttpActionResult UserList()
        {
            List<UserDTO> u = BLL.UserFunctions.UserList();
            if (u != null)
                return Ok(u);
            return BadRequest("there aren't users!"); ;
        }
        //UpdateEditUser
        [Route("UpdateEditUser")]
        [HttpPost]
        public UserDTO UpdateEditUser(UserDTO user)
        {
            UserDTO u= BLL.UserFunctions.UpdateEditUser(user);
            return u;
        }
        //AddMokdanOrUser
        [Route ("AddMokdanOrUser")]
        [HttpPost]
        public IHttpActionResult AddMokdanOrUser(UserDTO user)
        {
            //user.RoleId = 3;
            UserDTO u = BLL.UserFunctions.AddMokdanOrUser(user);
            if (u != null)
                return Ok(u);
            return BadRequest("the user is already exist!");
        }
    }
}
