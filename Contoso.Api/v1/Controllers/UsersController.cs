using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Contoso.Api.v1.Controllers
{
    public class UsersController : ApiController
    {
        private static List<Models.User> users = new List<Models.User>(new[]
            {
                new Models.User { Id = 1, FullName = "Bob Peter", IsActive = true, OrganizationId = 1 },
                new Models.User { Id = 2, FullName = "Simon Smith", IsActive = true, OrganizationId = 1 },
                new Models.User { Id = 3, FullName = "Dania Istwani", IsActive = true, OrganizationId = 2}
            });

        [HttpGet, Route("users")]
        public virtual IEnumerable<Models.User> GetUsers()
        {
            return users;
        }

        [HttpPost, Route("users")]
        public virtual void CreateUser([FromBody]Models.User user)
        {
            user.Id = users.Max(_ => _.Id) + 1;
            users.Add(user);
        }

        [HttpPut, Route("users")]
        public virtual void UpdateUser([FromBody]Models.User user)
        {
            var obj = users.FirstOrDefault(_ => _.Id == user.Id);
            if (obj != null)
            {
                obj.IsActive = user.IsActive;
                obj.OrganizationId = user.OrganizationId;
                obj.FullName = user.FullName;
            }
        }
    }
}
