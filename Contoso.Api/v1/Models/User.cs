namespace Contoso.Api.v1.Models
{
    public class User
    {
        public virtual int Id { get; set; }

        public virtual string FullName { get; set; }

        public virtual int OrganizationId { get; set; }

        public virtual bool IsActive { get; set; }
    }
}