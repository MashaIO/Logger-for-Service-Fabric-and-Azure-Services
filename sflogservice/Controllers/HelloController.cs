using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Logger.Base;
using Logger.ServiceFabric;

namespace sflogservice.Controllers
{
    [RoutePrefix("hello")]
    public class HelloController : ApiController
    {
        public ILog Log { get; set; }

        public HelloController(ILog logStrategy)
        {
            Log = logStrategy;
        }

        [HttpGet]
        [Route("logmessage")]
        public HttpResponseMessage GetAsync()
        {
            Log.Verbose("Log Verbose");
            Log.Critical(("Log Critical"));
            Log.Error("Log Error");
            Log.Information("Log Information");
            Log.Warning("Log Warning");

            return new HttpResponseMessage()
            {
                Content = new StringContent("buena suerte !!!!")
            };
        }
    }
}
