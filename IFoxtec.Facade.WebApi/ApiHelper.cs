using Abp.Dependency;
using Abp.Web.Models;
using IFoxtec.Common.WPF.Config;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.Facade.WebApi
{
    /// <summary>
    /// API访问辅助
    /// </summary>
    public class ApiHelper
    {
        public ApiHelper()
        {
            this.ApiRootPath = "";
        }

        public ApiHelper(string apiRootPath)
        {
            this.ApiRootPath = apiRootPath;
        }

        public string ApiRootPath;

        public TResult Get<TResult>()
        {
            return Get<TResult>(string.Empty, null);
        }

        public TResult Get<TResult>(string path)
        {
            return Get<TResult>(path, null);
        }

        public TResult Get<TResult>(string path,object param)
        {
            var client = GetClient(path);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");

            if (param != null)
            {
                var props = param.GetType().GetProperties();
                foreach (var p in props)
                    request.AddParameter(p.Name, p.GetValue(param, null));
            }
            SetAuthInfo(client, request);
            IRestResponse response = client.Execute(request);
            if (response.ContentLength <= 0)
                return default(TResult);

            var responseObj = JsonConvert.DeserializeObject<AjaxResponse<TResult>>(response.Content);
            if (responseObj != null)
                return responseObj.Result;
            return default(TResult);

        }

        public void Post(string path, object param)
        {
            Post<object>(path, param);
        }

        public TResult Post<TResult>(string path)
        {
            return Post<TResult>(path, null);
        }

        public TResult Post<TResult>(string path, object param)
        {
            var responseObj = RealPost<AjaxResponse<TResult>>(path, param);
            if (responseObj != null)
                return responseObj.Result;
            return default(TResult);
        }

        protected TResult RealPost<TResult>(string path, object param)
        {
            var client = GetClient(path);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(param), ParameterType.RequestBody);
            SetAuthInfo(client, request);
            IRestResponse response = client.Execute(request);
            if (response.ContentLength <= 0)
                return default(TResult);

            return JsonConvert.DeserializeObject<TResult>(response.Content);
        }

        public AjaxResponse<TResult> SrcPost<TResult>(string path,object param)
        {
            return Post<AjaxResponse<TResult>>(path, param);
        }

        protected virtual void SetAuthInfo(RestClient client, RestRequest request)
        {
            var configManager = IocManager.Instance.Resolve<IConfigManager>();
            var token = configManager.Get<string>(ConfigIndex.Token);
            if (string.IsNullOrEmpty(token))
                request.AddHeader("Authorization", "Bearer " + token);
        }

        private RestClient GetClient(string path)
        {
            var fullUrl = string.Empty;
            if (!path.StartsWith("http://"))
                fullUrl += this.ApiRootPath;

            if (this.ApiRootPath.EndsWith("/") && path.StartsWith("/"))
                fullUrl += path.TrimStart('/');
            else
                fullUrl += "/" + path;

            var client = new RestClient(fullUrl);
            return client;
        }
    }
}
