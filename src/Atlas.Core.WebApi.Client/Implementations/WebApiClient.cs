// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiClient.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.WebApi.Client.Implementations
{
   using Newtonsoft.Json;

   using RestSharp;
   using RestSharp.Serializers;

   public class WebApiClient : IWebApiClient
   {
      public TResponse Post<TRequest, TResponse>(string baseUrl, string api, TRequest request)
      {
         var restClient = new RestClient(baseUrl);
         var restRequest = new RestRequest(api, Method.POST)
            {
               RequestFormat = DataFormat.Json,
               JsonSerializer = new JsonSerialiser()
            };

         restRequest.AddBody(request);

         var restResponse = restClient.Execute(restRequest);

         // TODO: Create serialisation abstraction if the serialisation needs customisation
         var response = JsonConvert.DeserializeObject<TResponse>(restResponse.Content);
         return response;
      }

      private class JsonSerialiser : ISerializer
      {
         public JsonSerialiser()
         {
            this.ContentType = "application/json";
         }

         public string RootElement { get; set; }

         public string Namespace { get; set; }

         public string DateFormat { get; set; }

         public string ContentType { get; set; }

         public string Serialize(object obj)
         {
            // TODO: Create serialisation abstraction if the serialisation needs customisation
            return JsonConvert.SerializeObject(obj);
         }
      }
   }
}
