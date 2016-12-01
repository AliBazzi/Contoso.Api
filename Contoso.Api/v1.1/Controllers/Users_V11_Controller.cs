using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Contoso.Api.v11.Controllers
{
    [RoutePrefix("v1.1")]
    public class Users_V11_Controller : v1.Controllers.UsersController
    {
        private static List<Models.User> users = new List<Models.User>(new[]
            {
                new Models.User { Id = Guid.Parse("96e5de8a-b5d8-445d-b588-b25d40652f1d").ToString(), FirstName = "Bob", LastName = "Peter", IsActive = true, TenantId = Guid.Parse("02139cd8-5715-4436-b8b2-b4f35ec48add").ToString() },
                new Models.User { Id = Guid.Parse("964db780-f24b-4481-bbf4-517e774d1484").ToString(), FirstName = "Simon", LastName = "Smith", IsActive = true, TenantId = Guid.Parse("37025158-4647-45e6-ae5b-6643f26f0381").ToString() },
                new Models.User {Id = Guid.Parse("42e29c87-ffa4-4e22-9b2a-df6d85753645").ToString(), FirstName = "Dania", LastName = "Istwani", IsActive = true, TenantId = Guid.Parse("d02596f2-5bab-4c7c-9add-6d2f6e943fbf").ToString()}
            });

        #region Deprecated

        [Obsolete("deprecated, Provided new Api that takes fullname as a parameter"), NonAction]
        public override void UpdateUser(v1.Models.User user)
        {
            throw new NotImplementedException();
        }

        [Obsolete("deprecated, model conflict"), NonAction]
        public override void CreateUser(v1.Models.User user)
        {
            throw new NotImplementedException();
        }

        [Obsolete("deprecated, model conflict"), NonAction]
        public override IEnumerable<v1.Models.User> GetUsers()
        {
            throw new NotImplementedException();
        }

        #endregion

        [HttpGet, Route("users")]
        public IEnumerable<Models.User> _GetUsers()
        {
            return users;
        }

        [HttpPost, Route("users")]
        public void _CreateUser(Models.User user)
        {
            user.Id = Guid.NewGuid().ToString();
            users.Add(user);
        }

        [HttpPut, Route("users/{id}")]
        public virtual void _UpdateUser(string id, [FromBody]Models.User user)
        {
            var obj = users.FirstOrDefault(_ => _.Id == id);
            if (obj != null)
            {
                obj.TenantId = user.TenantId;
                obj.FirstName = user.FirstName;
                obj.LastName = user.LastName;
                obj.IsActive = user.IsActive;
            }
        }

        #region New

        [HttpGet, Route("users/{id}")]
        public virtual Models.User GetUser([FromUri]string id)
        {
            return users.FirstOrDefault(_ => _.Id == id);
        }

        [HttpDelete, Route("users/{id}")]
        public virtual void DeleteUser([FromUri]string id)
        {
            var obj = users.FirstOrDefault(_ => _.Id == id);
            if (obj != null)
                users.Remove(obj);
        }
        
        #endregion
    }
}
