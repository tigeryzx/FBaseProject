using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.Facade.Contract
{
    #region 全定义

    /// <summary>
    /// 基础Crud契约
    /// </summary>
    public interface ICrudContract<TEntityDto, TPrimaryKey, in TGetAllInput, in TCreateInput, in TUpdateInput, in TGetInput, in TDeleteInput> : IContract
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
        where TGetInput : IEntityDto<TPrimaryKey>
        where TDeleteInput : IEntityDto<TPrimaryKey>
    {
        Task<TEntityDto> Create(TCreateInput input);
        Task Delete(TDeleteInput input);
        Task<TEntityDto> Get(TGetInput input);
        Task<PagedResultDto<TEntityDto>> GetAll(TGetAllInput input);
        Task<TEntityDto> Update(TUpdateInput input);
    }
    #endregion

    #region 预定义删除

    /// <summary>
    /// 基础Crud契约
    /// </summary>
    public interface ICrudContract<TEntityDto, TPrimaryKey, in TGetAllInput, in TCreateInput, in TUpdateInput, in TGetInput> :
        ICrudContract<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, EntityDto<TPrimaryKey>>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
        where TGetInput : IEntityDto<TPrimaryKey>
    {

    }
    #endregion

    #region 预定义删除、获取

    /// <summary>
    /// 基础Crud契约
    /// </summary>
    public interface ICrudContract<TEntityDto, TPrimaryKey, in TGetAllInput, in TCreateInput, in TUpdateInput> :
        ICrudContract<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, EntityDto<TPrimaryKey>, EntityDto<TPrimaryKey>>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
    {

    }
    #endregion

}
