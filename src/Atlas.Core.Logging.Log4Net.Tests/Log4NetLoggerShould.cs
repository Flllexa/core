﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Log4NetLoggerShould.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Logging.Log4Net.Tests
{
   using System;

   using FakeItEasy;

   using log4net;

   using NUnit.Framework;

   public class Log4NetLoggerShould
   {
      private ILog log;

      private Log4NetLogger componentUnderTest;

      [SetUp]
      public void SetupBeforeEachTest()
      {
         this.log = A.Fake<ILog>();

         this.componentUnderTest = new Log4NetLogger(this.log);
      }

      [Test]
      public void ImplementILogger()
      {
         Assert.That(this.componentUnderTest, Is.InstanceOf<ILogger>());
      }

      [Test]
      public void ReturnLog4NetServiceBusLoggerFromFromConfigWithName()
      {
         var result = Log4NetLogger.FromConfig("myLogName");

         Assert.That(result, Is.InstanceOf<Log4NetLogger>());
      }

      [Test]
      public void ReturnLog4NetServiceBusLoggerFromFromConfigWithLog()
      {
         var result = Log4NetLogger.FromConfig(A.Fake<ILog>());

         Assert.That(result, Is.InstanceOf<Log4NetLogger>());
      }

      [Test]
      public void CallErrorFormatFromLogError()
      {
         const string ErrorMessage = "myErrorMessage '{0}'";
         var args = new object[] { "arg" };
         var exception = new Exception();

         this.componentUnderTest.LogError(ErrorMessage, exception, args);

         A.CallTo(() => this.log.Error("myErrorMessage 'arg'", exception)).MustHaveHappened(Repeated.Exactly.Once);
      }

      [Test]
      public void CallWarningFormatFromLogWarning()
      {
         const string WarningMessage = "myWarningMessage";
         var args = new object[1];

         this.componentUnderTest.LogWarning(WarningMessage, args);

         A.CallTo(() => this.log.WarnFormat(WarningMessage, args)).MustHaveHappened(Repeated.Exactly.Once);
      }

      [Test]
      public void CallInfoFormatFromLogInfo()
      {
         const string InfoMessage = "myInfoMessage";
         var args = new object[1];

         this.componentUnderTest.LogInfo(InfoMessage, args);

         A.CallTo(() => this.log.InfoFormat(InfoMessage, args)).MustHaveHappened(Repeated.Exactly.Once);
      }

      [Test]
      public void CallDebugFormatFromLogDebug()
      {
         const string DebugMessage = "myDebugMessage";
         var args = new object[1];

         this.componentUnderTest.LogDebug(DebugMessage, args);

         A.CallTo(() => this.log.DebugFormat(DebugMessage, args)).MustHaveHappened(Repeated.Exactly.Once);
      }
   }
}
