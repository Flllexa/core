// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostTests.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.WebApi.Client.Tests.WebApiClientTests
{
   using System;
   using System.Net;
   using System.Web.Http;

   using Atlas.Core.Logging;
   using Atlas.Core.WebApi.Client.Exceptions;
   using Atlas.Core.WebApi.Client.Implementations;
   using Atlas.Core.WebApi.Client.Tests.Controllers;
   using Atlas.Core.WebApi.Filters;

   using FakeItEasy;

   using Microsoft.Owin.Hosting;

   using NUnit.Framework;
   using NUnit.Framework.Constraints;

   using Owin;

   public class PostTests
   {
      private const string TestServerUrl = "http://localhost:23753";

      private IDisposable testServer;

      private WebApiClient componentUnderTest;

      [SetUp]
      public void SetupBeforeEachTest()
      {
         this.componentUnderTest = new WebApiClient();
      }

      [TestFixtureSetUp]
      public void SetupBeforeAllTests()
      {
         this.testServer = WebApp.Start(
            TestServerUrl,
            app =>
            {
               var config = new HttpConfiguration();
               config.MapHttpAttributeRoutes();
               config.Filters.Add(new WebApiExceptionFilterAttribute(A.Fake<ILogger>()));
               app.UseWebApi(config);
            });
      }

      [TestFixtureTearDown]
      public void TeardownAfterAllTests()
      {
         this.testServer.Dispose();
      }

      [Test]
      public void CallPostApi()
      {
         var request = new PostRequest { Subject = "myTitle", Body = "myBody" };

         var response = this.componentUnderTest.Post<PostRequest, PostResponse>(TestServerUrl, "api/test-controller/post", request);

         Assert.That(response.Subject, Is.EqualTo("myTitle"));
         Assert.That(response.Body, Is.EqualTo("myBody"));
      }

      [Test]
      public void ShouldThrowWebApiExceptionForUnexpectedException()
      {
         var request = new PostRequest();

         Assert.That(
            () => this.componentUnderTest.Post<PostRequest, PostResponse>(TestServerUrl, "api/test-controller/post-unexpected-exception", request),
            ThrowsWebApiClientException(HttpStatusCode.InternalServerError, "Unexpected Exception", "An unexpected error has occured. Please contact your administrator quoting token '"));
      }

      [Test]
      public void ShouldThrowWebApiExceptionForExpectedException()
      {
         var request = new PostRequest();

         Assert.That(
            () => this.componentUnderTest.Post<PostRequest, PostResponse>(TestServerUrl, "api/test-controller/post-expected-exception", request),
            ThrowsWebApiClientException(HttpStatusCode.InternalServerError, "myReason", "myMessage"));
      }

      private static IResolveConstraint ThrowsWebApiClientException(HttpStatusCode statusCode, string reason, string messageStartsWith)
      {
         return Throws.InstanceOf<WebApiClientException>()
            .With.Property("Reason").EqualTo(reason)
            .And.Property("Message").StartsWith(messageStartsWith)
            .And.Property("StatusCode").EqualTo(statusCode);
      }
   }
}
