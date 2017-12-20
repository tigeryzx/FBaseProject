using IFoxtec.Common.WPF.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Module.Users
{
    public class UserModel : BaseEntity<long>
    {
        [Display(Name="用户名")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "名")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "姓")]
        [Required]
        public string Surname { get; set; }

        [Display(Name = "邮箱地址")]
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "激活")]
        public bool IsActive { get; set; }

        [Display(Name = "全名")]
        public string FullName { get; set; }

        [Display(Name = "最后登录时间")]
        public DateTime? LastLoginTime { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreationTime { get; set; }

    }
}
