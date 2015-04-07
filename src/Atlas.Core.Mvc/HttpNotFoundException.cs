// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpNotFoundException.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System.Net;
   using System.Web;

   public class HttpNotFoundException : HttpException
   {
      public HttpNotFoundException(string message)
         : base((int)HttpStatusCode.NotFound, message)
      {
      }

      public HttpNotFoundException(string format, params object[] args)
         : base((int)HttpStatusCode.NotFound, string.Format(format, args))
      {
      }
   }
}
