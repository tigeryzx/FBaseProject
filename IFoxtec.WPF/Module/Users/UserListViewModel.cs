using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using IFoxtec.Common.WPF.BaseModel;
using IFoxtec.WPF.Common.ViewModels;
using Abp.Application.Services.Dto;
using AutoMapper;
using IFoxtec.WPF.Common.BaseModel;

namespace IFoxtec.WPF.Module.Users
{
    public class UserListViewModel : BaseListViewModel<UserModel,long>
    {
        private readonly IUserContract _UserContract;

        public UserListViewModel(IUserContract userContract)
        {
            
            this._UserContract = userContract;
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        public override ObservableCollection<UserModel> LoadEntities()
        {
            var pageData = this._UserContract.GetAll(new PagedResultRequestDto() { SkipCount = 0, MaxResultCount = 100 }).Result;
            return new ObservableCollection<UserModel>(Mapper.Map<List<UserModel>>(pageData.Items));
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        protected override void OnDelete(IList<UserModel> entitys)
        {
            var ids = entitys.ToList().Select(x => x.Id).ToList();
            foreach (var id in ids)
                this._UserContract.Delete(new EntityDto<long>() { Id = id });
        }

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <returns></returns>
        protected override CreateDocParam GetCreateDocParam()
        {
            return new CreateDocParam()
            {
                DocumentType = PageViewIndex.CreateUserView
            };
        }

        /// <summary>
        /// 编辑参数
        /// </summary>
        /// <returns></returns>
        protected override CreateDocParam GetEditDocParam()
        {
            return new CreateDocParam()
            {
                DocumentType = "",
                Parameters = this.SelEntity.Id
            };
        }
    }
}
