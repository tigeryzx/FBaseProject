using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Swashbuckle.Application;

namespace IFoxtec.Api
{
    [DependsOn(typeof(AbpWebApiModule), typeof(IFoxtecApplicationModule))]
    public class IFoxtecWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(IFoxtecApplicationModule).Assembly, "app")
                .Build();

            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));

            ConfigureSwaggerUi();
        }

        public override void PreInitialize()
        {
            //关闭跨站脚本攻击
            // Configuration.Modules.AbpWeb().AntiForgery.IsEnabled = false;
        }

        /// <summary>
        /// 配置SwaggerUi
        /// </summary>
        private void ConfigureSwaggerUi()
        {
            Configuration.Modules.AbpWebApi().HttpConfiguration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "IFoxtec.WebAPI文档");
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                    ////将application层中的注释添加到SwaggerUI中
                    //var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                    //var commentsFileName = "Bin//IFoxtec.Application.xml";
                    //var commentsFile = Path.Combine(baseDirectory, commentsFileName);
                    ////将注释的XML文档添加到SwaggerUI中
                    //c.IncludeXmlComments(commentsFile);
                })
                .EnableSwaggerUi("apis/{*assetPath}", b =>
                {
                    b.InjectJavaScript(typeof(IFoxtecWebApiModule).Assembly, "IFoxtec.SwaggerUI.scripts.swagger.js");
                });
        }

    }
}
