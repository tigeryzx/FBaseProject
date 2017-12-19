using System.ComponentModel.DataAnnotations;
using Abp.Auditing;

namespace IFoxtec.WPF.Module.Users
{
    public class CreateUserDto
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }

        public string[] RoleNames { get; set; }

        public string Password { get; set; }
    }
}