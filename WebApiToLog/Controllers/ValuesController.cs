using System.Net.Http;
using System.Web.Http;
using Logger.AppService;
using Logger.Base;

namespace WebApiToLog.Controllers
{
    [RoutePrefix("log")]
    public class ValuesController : ApiController
    {
        public ILog Log { get; set; }

        public ValuesController(ILog logStrategy)
        {
            Log = logStrategy;
        }

        [HttpGet]
        [Route("logmessage")]
        public HttpResponseMessage Get()
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
