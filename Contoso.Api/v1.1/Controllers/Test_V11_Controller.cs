using System;
using System.Web.Http;

namespace Contoso.Api.v11.Controllers
{
    [RoutePrefix("v1.1")]
    public class Test_V11_Controller : v1.Controllers.TestController
    {
        [Route("echo")]
        public override string Echo(string paramter)
        {
            return base.Echo(paramter);
        }

        [Obsolete("deprecated"), NonAction]
        public override string Bar(string paramater)
        {
            throw new NotImplementedException();
        }

        [Obsolete("deprecated, use new Foo Api"), NonAction]
        public override string Foo()
        {
            throw new NotImplementedException();
        }

        [HttpGet, Route("foo")]
        public virtual int _Foo()
        {
            return 1;
        }

        [Obsolete("deprecated, change parameter name to value of type int"), NonAction]
        public override string Baz(string paramater)
        {
            throw new NotImplementedException();
        }

        [HttpGet, Route("test/baz")]
        public virtual string _Baz(int value)
        {
            return $"v1.1.baz.{value}";
        }
    }
}
