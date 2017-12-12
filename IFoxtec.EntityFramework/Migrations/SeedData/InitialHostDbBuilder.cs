using IFoxtec.EntityFramework;
using EntityFramework.DynamicFilters;

namespace IFoxtec.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly IFoxtecDbContext _context;

        public InitialHostDbBuilder(IFoxtecDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
