// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpHelper.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System.Web;

   public class HttpHelper : IHttpHelper
   {
      public HttpRequestBase CurrentRequest
      {
         get { return new HttpRequestWrapper(HttpContext.Current.Request); }
      }

      public HttpResponseBase CurrentResponse
      {
         get { return new HttpResponseWrapper(HttpContext.Current.Response); }
      }
   }
}
