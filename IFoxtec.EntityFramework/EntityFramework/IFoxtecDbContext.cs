using System.Data.Common;
using Abp.Zero.EntityFramework;
using IFoxtec.Authorization.Roles;
using IFoxtec.Authorization.Users;
using IFoxtec.MultiTenancy;

namespace IFoxtec.EntityFramework
{
    public class IFoxtecDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public IFoxtecDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in IFoxtecDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of IFoxtecDbContext since ABP automatically handles it.
         */
        public IFoxtecDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public IFoxtecDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public IFoxtecDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
