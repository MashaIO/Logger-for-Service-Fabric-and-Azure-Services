using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Logger.Base;

namespace sflogservice.Controllers
{
    [RoutePrefix("hello")]
    public class HelloController : ApiController
    {
        public HelloController(ILog log)
        {

        }

        [HttpGet]
        [Route("logmessage")]
        public HttpResponseMessage GetAsync()
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent("buena suerte !!!!")
            };
        }
    }
}
