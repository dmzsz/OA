using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OA.WebApp.Helpers;

namespace OA.WebApp.Filter
{
    public class SidebarActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // do something before the action executes
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // do something after the action executes
            base.OnActionExecuted(context);

            var result = context.Result as ViewResult;

            
            if (result != null)
            {
                result.ViewData["nav"] = new YamlHandler("sidebar.yml").ToJson();
            }
        }
    }
}
