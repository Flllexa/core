// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiClientTests.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.WebApi.Client.Tests
{
   using Atlas.Core.WebApi.Client.Implementations;

   using NUnit.Framework;

   public class WebApiClientTests
   {
      [Test]
      public void PostReturnsPostedData()
      {
         var componentUnderTest = new WebApiClient();

         var request = new PostRequest { Title = "myTitle", Body = "myBody", UserId = 123 };

         var response = componentUnderTest.Post<PostRequest, PostResponse>("http://jsonplaceholder.typicode.com", "posts", request);

         Assert.That(response.Title, Is.EqualTo("myTitle"));
         Assert.That(response.Body, Is.EqualTo("myBody"));
         Assert.That(response.UserId, Is.EqualTo(123));
         Assert.That(response.Id, Is.Not.EqualTo(0));
      }

      private class PostRequest
      {
         public string Title { get; set; }

         public string Body { get; set; }

         public int UserId { get; set; }
      }

      // ReSharper disable once ClassNeverInstantiated.Local
      private class PostResponse
      {
         public int Id { get; set; }

         public string Title { get; set; }

         public string Body { get; set; }

         public int UserId { get; set; }
      }
   }
}
