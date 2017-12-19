using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace IFoxtec.Facade.Users.Dto
{
    public class UserDto : EntityDto<long>
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }

        public string FullName { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public DateTime CreationTime { get; set; }

        public string[] Roles { get; set; }
    }
}
