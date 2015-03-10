// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiExceptionFilterAttribute.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.WebApi.Filters
{
   using System;
   using System.Collections.Generic;
   using System.Net;
   using System.Net.Http;
   using System.Web.Http;
   using System.Web.Http.Filters;

   using Atlas.Core.WebApi.Exceptions;

   public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
   {
      // TODO: Need a webapilogger to be passed into here
      public WebApiExceptionFilterAttribute()
      {
      }

      public override void OnException(HttpActionExecutedContext actionExecutedContext)
      {
         var responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);

         var webApiResponseException = actionExecutedContext.Exception as WebApiExpectedException;

         if (webApiResponseException != null)
         {
            responseMessage.Content = new StringContent(webApiResponseException.Message);
            responseMessage.ReasonPhrase = webApiResponseException.Reason;
         }
         else
         {
            var exceptionToken = CreateExceptionToken();

            // TODO: Write out exceptionToken to logger

            responseMessage.Content = new StringContent(string.Format("An unexpected error has occured. Please contact your administrator quoting token '{0}'", exceptionToken));
            responseMessage.ReasonPhrase = "Unexpected Exception";
         }

         throw new HttpResponseException(responseMessage);
      }

      private static string CreateExceptionToken()
      {
         var value = Guid.NewGuid().ToString().ToUpper().Replace("-", null);

         return string.Join("-", SplitString(value));
      }

      private static IEnumerable<string> SplitString(string value)
      {
         var i = 0;

         while (i < value.Length)
         {
            var split = value.Substring(i, 4);

            yield return split;

            i += split.Length;
         }
      }
   }
}
