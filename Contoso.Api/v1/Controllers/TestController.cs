using System;
using System.Web.Http;

namespace Contoso.Api.v1.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet, Route("test/echo")]
        public virtual string Echo(string paramter)
        {
            return paramter;
        }

        [HttpGet, Route("test/foo")]
        public virtual string Foo()
        {
            return "v1.foo";
        }

        [HttpGet, Route("test/bar")]
        public virtual string Bar(string paramater)
        {
            return $"v1.bar.{paramater}";
        }

        [HttpGet, Route("test/baz")]
        public virtual string Baz(string parameter)
        {
            return $"v1.baz.{parameter}";
        }

        [HttpGet, Route("test/quz")]
        public virtual int Quz(int a, int b)
        {
            return a > b ? a : b;
        }
    }
}
