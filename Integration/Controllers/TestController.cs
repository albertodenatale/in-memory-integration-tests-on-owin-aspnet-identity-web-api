using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Integration.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        [Authorize(Roles = "user")]
        public string This()
        {
            return "astring";
        }
    }
}