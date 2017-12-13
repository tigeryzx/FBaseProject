using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using IFoxtec.Authorization.Users;
using IFoxtec.Editions;
using System.Linq;
using System.Threading.Tasks;

namespace IFoxtec.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore
            ) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore
            )
        {
        }

        public async Task<IQueryable<Tenant>> GetActiveTenantAsync()
        {
            return await Task.Run(() =>
            {
                return base.TenantRepository.GetAll().Where(x => x.IsActive);
            });
        }
    }
}