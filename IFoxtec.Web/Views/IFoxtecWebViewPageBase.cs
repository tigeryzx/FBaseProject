using Abp.Web.Mvc.Views;

namespace IFoxtec.Web.Views
{
    public abstract class IFoxtecWebViewPageBase : IFoxtecWebViewPageBase<dynamic>
    {

    }

    public abstract class IFoxtecWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected IFoxtecWebViewPageBase()
        {
            LocalizationSourceName = IFoxtecConsts.LocalizationSourceName;
        }
    }
}