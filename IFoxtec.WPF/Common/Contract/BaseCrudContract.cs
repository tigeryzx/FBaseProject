using Abp.Application.Services.Dto;
using IFoxtec.Facade.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Common.Contract
{
    #region 全定义

    public class BaseCrudContract<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, TDeleteInput> : BaseContract, ICrudContract<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, TDeleteInput>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
        where TGetInput : IEntityDto<TPrimaryKey>
        where TDeleteInput : IEntityDto<TPrimaryKey>
    {
        protected ApiHelper _api;

        public BaseCrudContract(string apiRootPath)
        {
            this._api = new ApiHelper(apiRootPath);
        }

        public Task<TEntityDto> Create(TCreateInput input)
        {
            return Task.Run(() => { return this._api.Post<TEntityDto>("Create", input); });
        }

        public Task Delete(TDeleteInput input)
        {
            return Task.Run(() => { this._api.Post("Delete", input); });
        }

        public Task<TEntityDto> Get(TGetInput input)
        {
            return Task.Run(() => { return this._api.Post<TEntityDto>("Get", input); });
        }

        public Task<PagedResultDto<TEntityDto>> GetAll(TGetAllInput input)
        {
            return Task.Run(() => { return this._api.Post<PagedResultDto<TEntityDto>>("GetAll", input); });
        }

        public Task<TEntityDto> Update(TUpdateInput input)
        {
            return Task.Run(() => { return this._api.Post<TEntityDto>("Update", input); });
        } 
    }
    #endregion

    #region 预定义删除

    public class BaseCrudContract<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput> : BaseCrudContract<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, EntityDto<TPrimaryKey>>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
        where TGetInput : IEntityDto<TPrimaryKey>
    {
        public BaseCrudContract(string apiRootPath) : base(apiRootPath) { }
    }
    #endregion

    #region 预定义删除、获取

    public class BaseCrudContract<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput> : BaseCrudContract<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, EntityDto<TPrimaryKey>, EntityDto<TPrimaryKey>>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
    {
        public BaseCrudContract(string apiRootPath) : base(apiRootPath) { }
    }
    #endregion
}
