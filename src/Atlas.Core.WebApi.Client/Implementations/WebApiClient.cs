// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiClient.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.WebApi.Client.Implementations
{
   public class WebApiClient : IWebApiClient
   {
      public TResponse Post<TRequest, TResponse>(string baseUrl, string api, TRequest request)
      {
         throw new System.NotImplementedException();
      }
   }
}
