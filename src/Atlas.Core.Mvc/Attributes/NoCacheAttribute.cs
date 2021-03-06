﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoCacheAttribute.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System;
   using System.Web;
   using System.Web.Mvc;

   public class NoCacheAttribute : ActionFilterAttribute
   {
      public override void OnResultExecuting(ResultExecutingContext filterContext)
      {
         filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
         filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
         filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
         filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
         filterContext.HttpContext.Response.Cache.SetNoStore();

         base.OnResultExecuting(filterContext);
      }
   }
}