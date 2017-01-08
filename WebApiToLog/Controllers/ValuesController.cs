using System.Net.Http;
using System.Web.Http;
using Logger.AppService;
using Logger.Base;

namespace WebApiToLog.Controllers
{
    [RoutePrefix("log")]
    public class ValuesController : ApiController
    {
        private readonly ILogStrategy _log;

        public ValuesController(ILogStrategy log)
        {
            _log = log;
        }

        [HttpGet]
        [Route("logmessage")]
        public HttpResponseMessage Get()
        {
            _log.CreateLog(typeof(ConcreteFactoryAppEventSource)).LogVerbose("Log Verbose");
            _log.CreateLog(typeof(ConcreteFactoryAppEventSource)).LogCritical(("Log Critical"));
            _log.CreateLog(typeof(ConcreteFactoryAppEventSource)).LogError("Log Error");
            _log.CreateLog(typeof(ConcreteFactoryAppEventSource)).LogInformation("Log Information");
            _log.CreateLog(typeof(ConcreteFactoryAppEventSource)).LogWarning("Log Warning");

            return new HttpResponseMessage()
            {
                Content = new StringContent("buena suerte !!!!")
            };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
