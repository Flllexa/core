// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AtlasExceptionHttpModule.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System;
   using System.Net;
   using System.Web;

   /// <summary>
   /// Use this to catch not found errors for URLs containing files which may exist (eg. *.cshtml)
   /// </summary>
   /// <remarks>
   /// Not found errors will ultimately be forwarded to the NotFoundController
   /// </remarks>
   public class AtlasExceptionHttpModule : IHttpModule
   {
      public void Init(HttpApplication httpApplication)
      {
         httpApplication.Error += this.HttpApplicationError;
      }

      public void Dispose()
      {
      }

      private void HttpApplicationError(object sender, EventArgs e)
      {
         var httpApplication = (HttpApplication)sender;
         var context = httpApplication.Context;
         var exception = context.Server.GetLastError();

         var httpException = exception as HttpException;

         if (httpException != null && httpException.GetHttpCode() == (int)HttpStatusCode.NotFound)
         {
            context.Server.ClearError();

            AtlasExceptionAction.ExecutePageNotFound(context.Request.RequestContext, httpException);
         }
      }
   }
}
