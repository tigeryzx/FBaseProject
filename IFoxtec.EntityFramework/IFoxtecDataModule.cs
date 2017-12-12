using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using IFoxtec.EntityFramework;

namespace IFoxtec
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(IFoxtecCoreModule))]
    public class IFoxtecDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<IFoxtecDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
