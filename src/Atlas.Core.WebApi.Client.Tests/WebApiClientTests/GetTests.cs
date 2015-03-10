// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetTests.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.WebApi.Client.Tests.WebApiClientTests
{
   using System;
   using System.Net;
   using System.Web.Http;

   using Atlas.Core.WebApi.Client.Exceptions;
   using Atlas.Core.WebApi.Client.Implementations;
   using Atlas.Core.WebApi.Client.Tests.Controllers;
   using Atlas.Core.WebApi.Filters;

   using Microsoft.Owin.Hosting;

   using NUnit.Framework;
   using NUnit.Framework.Constraints;

   using Owin;

   public class GetTests
   {
      private const string TestServerUrl = "http://localhost:62731";

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
                  config.Filters.Add(new WebApiExceptionFilterAttribute());
                  app.UseWebApi(config);
               });
      }

      [TestFixtureTearDown]
      public void TeardownAfterAllTests()
      {
         this.testServer.Dispose();
      }

      [Test]
      public void CallGetApi()
      {
         var response = this.componentUnderTest.Get<GetResponse>(TestServerUrl, "api/test-controller/get?arg=myArg");
         
         Assert.That(response.Argument, Is.EqualTo("myArg"));
      }

      [Test]
      public void ShouldThrowWebApiExceptionForUnexpectedException()
      {
         Assert.That(
            () => this.componentUnderTest.Get<GetResponse>(TestServerUrl, "api/test-controller/get-unexpected-exception"),
            ThrowsWebApiClientException(HttpStatusCode.InternalServerError, "Unexpected Exception", "An unexpected error has occured. Please contact your administrator quoting token '"));
      }

      [Test]
      public void ShouldThrowWebApiExceptionForExpectedException()
      {
         Assert.That(
            () => this.componentUnderTest.Get<GetResponse>(TestServerUrl, "api/test-controller/get-expected-exception"),
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
