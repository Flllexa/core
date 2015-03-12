// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiClient.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.WebApi.Client.Implementations
{
   using System;
   using System.Net;
   using System.Net.Http;
   using System.Text;

   using Atlas.Core.WebApi.Client.Exceptions;

   using Newtonsoft.Json;

   // TODO: Create async Get and Post methods
   public class WebApiClient : IWebApiClient
   {
      public TResponse Get<TResponse>(string baseUrl, string api)
      {
         var httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };

         var httpResponse = httpClient.GetAsync(api).Result;
         var responseContent = httpResponse.Content.ReadAsStringAsync().Result;

         if (httpResponse.StatusCode == HttpStatusCode.OK)
         {
            // TODO: Create serialisation abstraction if the serialisation needs customisation
            var response = JsonConvert.DeserializeObject<TResponse>(responseContent);
            return response;
         }

         throw new WebApiClientException(httpResponse.StatusCode, httpResponse.ReasonPhrase, responseContent);
      }

      public TResponse Post<TRequest, TResponse>(string baseUrl, string api, TRequest request)
      {
         // TODO: Create serialisation abstraction if the serialisation needs customisation
         var jsonRequest = JsonConvert.SerializeObject(request);

         var httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
         var httpRequest = new HttpRequestMessage(HttpMethod.Post, api) { Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json") };

         var httpResponse = httpClient.SendAsync(httpRequest).Result;
         var responseContent = httpResponse.Content.ReadAsStringAsync().Result;

         if (httpResponse.StatusCode == HttpStatusCode.OK)
         {
            // TODO: Create serialisation abstraction if the serialisation needs customisation
            var response = JsonConvert.DeserializeObject<TResponse>(responseContent);
            return response;
         }

         throw new WebApiClientException(httpResponse.StatusCode, httpResponse.ReasonPhrase, responseContent);
      }
   }
}
