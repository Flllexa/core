// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWebApiClient.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.WebApi.Client
{
   public interface IWebApiClient
   {
      TResponse Post<TRequest, TResponse>(string baseUrl, string api, TRequest request);
   }
}
