using IFoxtec.Common.WPF.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Module.Users
{
    public class CreateUserModel
    {
        public CreateUserModel()
        {
            this.IsActive = true;
        }

        const string NameGroup = "<Name>";
        const string PasswordGroup = "<Password>";

        [Display(Name = "用户名", Order = 0)]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "名", GroupName = NameGroup, Order = 1)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "姓", GroupName = NameGroup, Order = 2)]
        [Required]
        public string Surname { get; set; }

        [Display(Name = "邮箱地址", Order = 3)]
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "是否激活", Order = 6)]
        public bool IsActive { get; set; }

        public string[] RoleNames { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "密码", GroupName = PasswordGroup, Order = 4)]
        [Required]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "确认密码", GroupName = PasswordGroup, Order = 5)]
        public string ConfirmPassword { get; set; }

    }
}
