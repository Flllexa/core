// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestExtensionsTests.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc.Tests
{
   using System.Collections.Specialized;
   using System.Web;

   using FakeItEasy;

   using NUnit.Framework;

   // Based on http://stackoverflow.com/questions/2577496/how-can-i-get-the-clients-ip-address-in-asp-net-mvc
   public class RequestExtensionsTests
   {
      private const string XForwardedFor = "X_FORWARDED_FOR";
      private const string MalformedIpAddress = "MALFORMED";
      private const string DefaultIpAddress = "0.0.0.0";
      private const string GoogleIpAddress = "74.125.224.224";
      private const string MicrosoftIpAddress = "65.55.58.201";
      private const string Private24Bit = "10.0.0.0";
      private const string Private20Bit = "172.16.0.0";
      private const string Private16Bit = "192.168.0.0";
      private const string PrivateLinkLocal = "169.254.0.0";

      private HttpRequestBase httpRequest;
      private NameValueCollection serverVariables;

      [SetUp]
      public void SetupBeforeEachTest()
      {
         this.httpRequest = A.Fake<HttpRequestBase>();
         this.serverVariables = new NameValueCollection();

         var httpHelper = A.Fake<IHttpHelper>();
         A.CallTo(() => httpHelper.CurrentRequest).Returns(this.httpRequest);

         A.CallTo(() => this.httpRequest.ServerVariables).Returns(this.serverVariables);
      }

      [Test]
      public void PublicIpAndNullXForwardedForReturnsCorrectIp()
      {
         // Arrange
         A.CallTo(() => this.httpRequest.UserHostAddress).Returns(GoogleIpAddress);

         // Act
         var ip = this.httpRequest.GetCallerIdentity();

         // Assert
         Assert.AreEqual(GoogleIpAddress, ip);
      }

      [Test]
      public void PublicIpAndEmptyXForwardedForReturnsCorrectIp()
      {
         // Arrange
         A.CallTo(() => this.httpRequest.UserHostAddress).Returns(GoogleIpAddress);
         this.serverVariables.Add(XForwardedFor, string.Empty);

         // Act
         var ip = this.httpRequest.GetCallerIdentity();

         // Assert
         Assert.AreEqual(GoogleIpAddress, ip);
      }

      [Test]
      public void MalformedUserHostAddressReturnsDefaultIpAddress()
      {
         // Arrange
         A.CallTo(() => this.httpRequest.UserHostAddress).Returns(MalformedIpAddress);

         // Act
         var ip = this.httpRequest.GetCallerIdentity();

         // Assert
         Assert.AreEqual(DefaultIpAddress, ip);
      }

      [Test]
      public void MalformedXForwardedForReturnsDefaultIpAddress()
      {
         // Arrange
         A.CallTo(() => this.httpRequest.UserHostAddress).Returns(GoogleIpAddress);
         this.serverVariables.Add(XForwardedFor, MalformedIpAddress);

         // Act
         var ip = this.httpRequest.GetCallerIdentity();

         // Assert
         Assert.AreEqual(DefaultIpAddress, ip);
      }

      [Test]
      public void SingleValidPublicXForwardedForReturnsXForwardedFor()
      {
         // Arrange
         A.CallTo(() => this.httpRequest.UserHostAddress).Returns(GoogleIpAddress);
         this.serverVariables.Add(XForwardedFor, MicrosoftIpAddress);

         // Act
         var ip = this.httpRequest.GetCallerIdentity();

         // Assert
         Assert.AreEqual(MicrosoftIpAddress, ip);
      }

      [Test]
      public void MultipleValidPublicXForwardedForReturnsLastXForwardedFor()
      {
         // Arrange
         A.CallTo(() => this.httpRequest.UserHostAddress).Returns(GoogleIpAddress);
         this.serverVariables.Add(XForwardedFor, GoogleIpAddress + "," + MicrosoftIpAddress);

         // Act
         var ip = this.httpRequest.GetCallerIdentity();

         // Assert
         Assert.AreEqual(MicrosoftIpAddress, ip);
      }

      [Test]
      public void SinglePrivateXForwardedForReturnsUserHostAddress()
      {
         // Arrange
         A.CallTo(() => this.httpRequest.UserHostAddress).Returns(GoogleIpAddress);
         this.serverVariables.Add(XForwardedFor, Private24Bit);

         // Act
         var ip = this.httpRequest.GetCallerIdentity();

         // Assert
         Assert.AreEqual(GoogleIpAddress, ip);
      }

      [Test]
      public void MultiplePrivateXForwardedForReturnsUserHostAddress()
      {
         const string PrivateIpList = Private24Bit + "," + Private20Bit + "," + Private16Bit + "," + PrivateLinkLocal;

         // Arrange
         A.CallTo(() => this.httpRequest.UserHostAddress).Returns(GoogleIpAddress);
         this.serverVariables.Add(XForwardedFor, PrivateIpList);

         // Act
         var ip = this.httpRequest.GetCallerIdentity();

         // Assert
         Assert.AreEqual(GoogleIpAddress, ip);
      }

      [Test]
      public void MultiplePublicXForwardedForWithPrivateLastReturnsLastPublic()
      {
         const string PrivateIpList = Private24Bit + "," + Private20Bit + "," + MicrosoftIpAddress + "," + PrivateLinkLocal;

         // Arrange
         A.CallTo(() => this.httpRequest.UserHostAddress).Returns(GoogleIpAddress);
         this.serverVariables.Add(XForwardedFor, PrivateIpList);

         // Act
         var ip = this.httpRequest.GetCallerIdentity();

         // Assert
         Assert.AreEqual(MicrosoftIpAddress, ip);
      }
   }
}
