// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiExceptionFilterAttributeTests.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.WebApi.Tests
{
   using System;
   using System.Web.Http;
   using System.Web.Http.Filters;

   using Atlas.Core.Logging;
   using Atlas.Core.WebApi.Exceptions;
   using Atlas.Core.WebApi.Filters;

   using FakeItEasy;

   using NUnit.Framework;

   public class WebApiExceptionFilterAttributeTests
   {
      private ILogger logger;

      private WebApiExceptionFilterAttribute componentUnderTest;

      [SetUp]
      public void SetupBeforeEachTest()
      {
         this.logger = A.Fake<ILogger>();

         this.componentUnderTest = new WebApiExceptionFilterAttribute(this.logger);
      }

      [Test]
      public void ShouldLogWarningForExpectedException()
      {
         var context = new HttpActionExecutedContext { Exception = new WebApiExpectedException("myReason", "myMessage") };

         Assert.That(() => this.componentUnderTest.OnException(context), Throws.InstanceOf<HttpResponseException>());

         A.CallTo(() => this.logger.LogWarning("An expected error occured; Reason='{0}',Message='{1}'", "myReason", "myMessage")).MustHaveHappened(Repeated.Exactly.Once);
      }

      [Test]
      public void ShouldLogErrorForExpectedException()
      {
         var exception = new Exception();
         var context = new HttpActionExecutedContext { Exception = exception };

         Assert.That(() => this.componentUnderTest.OnException(context), Throws.InstanceOf<HttpResponseException>());

         A.CallTo(() => this.logger.LogError("An unexpected error occured. The caller was given the error token '{0}'", exception, A<string>._)).MustHaveHappened(Repeated.Exactly.Once);
      }
   }
}
