// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AtlasExceptionAction.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System;
   using System.Net;
   using System.Web;
   using System.Web.Mvc;
   using System.Web.Routing;

   using Atlas.Core.Logging;

   public static class AtlasExceptionAction
   {
      public static void ExecutePageNotFound(RequestContext requestContext, Exception exception)
      {
         ExecutePageNotFound(new ControllerContext(requestContext, new FakeController()), exception);
      }

      public static void ExecutePageNotFound(ControllerContext context, Exception exception)
      {
         var logger = GetLogger();
         logger.LogError(
            "Page not found\r\nUrl: {0}\r\nUsername: {1}\r\nIP: {2}\r\nUser Agent: {3}",
            new Exception(), // TODO: Awaiting build of Atlas.Core with LogError overload without exception
            context.HttpContext.Request.Url,
            context.HttpContext.User.Identity.Name,
            context.HttpContext.Request.GetCallerIdentity(),
            context.HttpContext.Request.UserAgent);

         if (!context.RouteData.Values.ContainsKey("controller"))
         {
            // This doesn't need to be a real controller - the controller key just needs populating
            context.RouteData.Values["controller"] = "Errors";
         }

         var exceptionViewModel = exception != null ? new ExceptionViewModel(exception) : new ExceptionViewModel();

         var viewName = context.HttpContext.Request.IsAjaxRequest() ? "AjaxPageNotFound" : "PageNotFound";
         var viewResult = CreateViewResult(context.HttpContext, HttpStatusCode.NotFound, viewName, exceptionViewModel);

         viewResult.ExecuteResult(context);
      }

      public static ViewResult ViewResultResponse(HttpContextBase httpContext, HttpStatusCode statusCode, string viewName, Exception exception)
      {
         var exceptionViewModel = new ExceptionViewModel(exception);

         var logger = GetLogger();
         logger.LogError(
            "An unexpected error has occured\r\nUrl: {0}\r\nExceptionGuid: {1}\r\nUsername: {2}\r\nIP: {3}",
            exceptionViewModel.Exception,
            httpContext.Request.Url,
            exceptionViewModel.ExceptionGuid.ToString(),
            // TODO: Await next build of atlas.core for .ToToken() extension,
            httpContext.User.Identity.Name,
            httpContext.Request.GetCallerIdentity());

         return CreateViewResult(httpContext, statusCode, viewName, exceptionViewModel);
      }

      private static ILogger GetLogger()
      {
         var logger = (ILogger)DependencyResolver.Current.GetService(typeof(ILogger));
         return logger;
      }

      private static ViewResult CreateViewResult(HttpContextBase httpContext, HttpStatusCode statusCode, string viewName, ExceptionViewModel exceptionViewModel)
      {
         var response = httpContext.Response;

         response.StatusCode = (int)statusCode;
         response.TrySkipIisCustomErrors = true;
         response.Clear();

         return new ViewResult
            {
               ViewName = viewName,
               ViewData = new ViewDataDictionary(exceptionViewModel)
            };
      }

      private class FakeController : ControllerBase
      {
         protected override void ExecuteCore()
         {
         }
      }
   }
}
