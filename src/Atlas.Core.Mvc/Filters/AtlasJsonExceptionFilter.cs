// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AtlasJsonExceptionFilter.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System;
   using System.Web.Mvc;

   using Atlas.Core.Logging;

   public class AtlasJsonExceptionFilter : IActionFilter
   {
      public void OnActionExecuting(ActionExecutingContext filterContext)
      {
      }

      public void OnActionExecuted(ActionExecutedContext filterContext)
      {
         if (filterContext == null)
         {
            throw new ArgumentNullException("filterContext");
         }

         if (filterContext.Exception == null || filterContext.ExceptionHandled || !filterContext.HttpContext.Request.IsAjaxRequest())
         {
            return;
         }

         var actionDescriptor = filterContext.ActionDescriptor as ReflectedActionDescriptor;

         if (actionDescriptor == null || actionDescriptor.MethodInfo.ReturnType != typeof(JsonResult))
         {
            return;
         }

         var logger = (ILogger)DependencyResolver.Current.GetService(typeof(ILogger));
         logger.LogError("An unexpected error has occurred.", filterContext.Exception);

         filterContext.Result = new JsonErrorResult();
         filterContext.ExceptionHandled = true;
      }
   }
}
