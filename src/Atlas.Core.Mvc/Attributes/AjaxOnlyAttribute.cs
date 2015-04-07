// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AjaxOnlyAttribute.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System;
   using System.Web.Mvc;

   /// <summary>
   /// ActionFilter to throw a 404 Not Found exception if the attributed method was not called
   /// by an ajax request.
   /// </summary>
   /// <remarks>
   /// AjaxOnlyAttribute requires X-Requested-With: XMLHttpRequest to be passed in the HTTP header.
   /// Using the jquery.unobtrusive-ajax.js script adds this.
   /// </remarks>
   [AttributeUsage(AttributeTargets.Method)]
   public class AjaxOnlyAttribute : ActionFilterAttribute
   {
      public override void OnActionExecuting(ActionExecutingContext filterContext)
      {
         if (!filterContext.HttpContext.Request.IsAjaxRequest())
         {
            throw new HttpNotFoundException("AjaxOnlyAttribute - request must be made by an AJAX call");
         }

         base.OnActionExecuting(filterContext);
      }
   }
}
