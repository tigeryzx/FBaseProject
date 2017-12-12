using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using IFoxtec.EntityFramework;

namespace IFoxtec.Migrator
{
    [DependsOn(typeof(IFoxtecDataModule))]
    public class IFoxtecMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<IFoxtecDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}