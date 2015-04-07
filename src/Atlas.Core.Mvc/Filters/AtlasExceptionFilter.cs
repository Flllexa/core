// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AtlasExceptionFilter.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System;
   using System.Net;
   using System.Web;
   using System.Web.Mvc;

   public class AtlasExceptionFilter : IExceptionFilter
   {
      public AtlasExceptionFilter()
      {
         this.PageNotFoundView = "PageNotFound";
         this.AjaxPageNotFoundView = "AjaxPageNotFound";
         this.UnexpectedErrorView = "UnexpectedError";
         this.AjaxUnexpectedErrorView = "AjaxUnexpectedError";
      }

      public string PageNotFoundView { get; set; }

      public string AjaxPageNotFoundView { get; set; }

      public string UnexpectedErrorView { get; set; }

      public string AjaxUnexpectedErrorView { get; set; }

      public void OnException(ExceptionContext filterContext)
      {
         if (filterContext == null)
         {
            throw new ArgumentNullException("filterContext");
         }

         if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
         {
            return;
         }

         HttpStatusCode statusCode;
         string viewName;

         var isAjax = filterContext.HttpContext.Request.IsAjaxRequest();
         var httpException = filterContext.Exception as HttpException;

         if (httpException != null && httpException.GetHttpCode() == (int)HttpStatusCode.NotFound)
         {
            statusCode = HttpStatusCode.NotFound;
            viewName = isAjax  ? this.AjaxPageNotFoundView : this.PageNotFoundView;
         }
         else
         {
            statusCode = HttpStatusCode.InternalServerError;
            viewName = isAjax ? this.AjaxUnexpectedErrorView : this.UnexpectedErrorView;
         }

         filterContext.Result = AtlasExceptionAction.ViewResultResponse(filterContext.HttpContext, statusCode, viewName, filterContext.Exception);
         filterContext.ExceptionHandled = true;
      }
   }
}
