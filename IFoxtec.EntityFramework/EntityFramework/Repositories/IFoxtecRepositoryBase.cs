using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace IFoxtec.EntityFramework.Repositories
{
    public abstract class IFoxtecRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<IFoxtecDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected IFoxtecRepositoryBase(IDbContextProvider<IFoxtecDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class IFoxtecRepositoryBase<TEntity> : IFoxtecRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected IFoxtecRepositoryBase(IDbContextProvider<IFoxtecDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
