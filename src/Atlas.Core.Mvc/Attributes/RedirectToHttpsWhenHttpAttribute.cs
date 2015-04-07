// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RedirectToHttpsWhenHttpAttribute.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System;
   using System.Web.Mvc;

   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
   public class RedirectToHttpsWhenHttpAttribute : RequireHttpsAttribute
   {
      public override void OnAuthorization(AuthorizationContext filterContext)
      {
         if (filterContext == null)
         {
            throw new ArgumentNullException("filterContext");
         }

         if (!string.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
         {
            return;
         }

         if (filterContext.ActionDescriptor.IsDefined(typeof(AllowHttpAttribute), true))
         {
            return;
         }

         if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowHttpAttribute), true))
         {
            return;
         }

         if (filterContext.HttpContext.Request.IsLocal)
         {
            return;
         }

         base.OnAuthorization(filterContext);
      }
   }
}
