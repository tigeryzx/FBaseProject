using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace IFoxtec.WPF.Module.Users
{
    public class UpdateUserDto: EntityDto<long>
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }

        public string[] RoleNames { get; set; }
    }
}