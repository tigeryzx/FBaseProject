using System.Linq;
using IFoxtec.EntityFramework;
using IFoxtec.MultiTenancy;

namespace IFoxtec.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly IFoxtecDbContext _context;

        public DefaultTenantCreator(IFoxtecDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
