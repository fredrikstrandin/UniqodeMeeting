using System;
using System.Threading.Tasks;
using HeroesServices.Interface;
using HeroesServices.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace HeroesUtils.Attributes
{
    public class ETagCheckFilterAttribute : Attribute, IAsyncActionFilter
    {
        private readonly string _collection;
        private readonly string _key;
        //This is supposed to be an interface but has not come about how to use Dependecy injection 
        //without having it as a constructor parameter.
        //private IETagService _eTagService { get; }

        private ETagService _eTagService = new ETagService();

        public ETagCheckFilterAttribute(string collection, string key)
        {
            _collection = collection;
            _key = key;

            //_eTagService = eTagService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.RouteData.Values.ContainsKey("id"))
            {
                string id = (string)context.RouteData.Values["id"];

                long eTag = await _eTagService.GetETagAsync(_collection, _key, id);

                if (eTag == 0)
                {
                    context.Result = new NotFoundResult();
                    return;
                }
                else
                {
                    if (context.HttpContext.Request.Headers.ContainsKey("If-None-Match") && context.HttpContext.Request.Headers["If-None-Match"] == eTag.ToString())
                    {
                        // not modified
                        context.Result = new StatusCodeResult(304);
                        return;
                    }

                    if (!context.HttpContext.Response.HasStarted && !context.HttpContext.Response.Headers.ContainsKey("ETag"))
                        context.HttpContext.Response.Headers.Add("ETag", new[] { eTag.ToString() });
                }
            }

            await next();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {            
        }
    }
}
