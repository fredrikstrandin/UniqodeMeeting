using System;
using System.Threading.Tasks;
using HeroesServices.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HeroesUtils.Attributes
{
    public class ETagAttribute : Attribute, IAsyncActionFilter
    {
        public string _collection;
        public string _key;
        
        public ETagAttribute(string collection, string key)
        {
            _collection = collection;
            _key = key;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IETagService _eTagService = (IETagService)context.HttpContext.RequestServices.GetService(typeof(IETagService));
            
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
