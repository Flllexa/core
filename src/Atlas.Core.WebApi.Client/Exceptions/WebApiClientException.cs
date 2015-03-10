// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiClientException.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.WebApi.Client.Exceptions
{
   using System;
   using System.Net;

   public class WebApiClientException : Exception
   {
      public WebApiClientException(HttpStatusCode statusCode, string reason, string message)
         : base(message)
      {
         this.StatusCode = statusCode;
         this.Reason = reason;
      }

      public HttpStatusCode StatusCode { get; private set; }

      public string Reason { get; private set; }
   }
}
