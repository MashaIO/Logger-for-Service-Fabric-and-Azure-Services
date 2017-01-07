using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Logger.Base;
using Logger.Infrastructure;
using Logger.ServiceFabric;

namespace sflogservice.Controllers
{
    [RoutePrefix("hello")]
    public class HelloController : ApiController
    {
        private readonly ILogStrategy _log;

        public HelloController(ILogStrategy log)
        {
            _log = log;
        }

        [HttpGet]
        [Route("logmessage")]
        public HttpResponseMessage GetAsync()
        {
            _log.CreateLog(typeof(ConcreteFactoryServiceEventSource)).LogVerbose("hi");
            return new HttpResponseMessage()
            {
                Content = new StringContent("buena suerte !!!!")
            };
        }
    }
}
