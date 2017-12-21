using Abp.Application.Services.Dto;
using IFoxtec.WPF.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IFoxtec.Users.Dto;

namespace IFoxtec.WPF.Module.Users
{
    public class CreateUserViewModel : BaseFormViewModel<CreateUserModel>
    {
        private readonly IUserContract _userContract;

        public CreateUserViewModel(IUserContract userContract)
        {
            this._userContract = userContract;

            InitData();
        }

        private void InitData()
        {
            var rolesSource = this._userContract.GetRoles().Result;
            this.Roles = Mapper.Map<List<RoleItemModel>>(rolesSource.Items);
        }

        #region 属性

        public List<RoleItemModel> Roles
        {
            get
            {
                return GetProperty(() => this.Roles);
            }
            set
            {
                SetProperty(() => this.Roles, value);
            }
        }

        #endregion

        #region CURD实现

        protected override CreateUserModel OnSave()
        {
            if (this.FormStatus == Common.BaseModel.FormStatus.Create)
            {
                var userInfo = Mapper.Map<CreateUserDto>(SetCheckedItem(this.Entity));
                this._userContract.Create(userInfo);
                return this.Entity;
            }
            return null;
        }

        protected CreateUserModel SetCheckedItem(CreateUserModel model)
        {
            model.RoleNames = this.Roles.Where(x => x.IsChecked).Select(x => x.Name).ToArray();
            return model;
        }

        #endregion
    }
}
