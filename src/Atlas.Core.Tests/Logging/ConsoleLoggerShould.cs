﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleLoggerShould.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Tests.Logging
{
   using System;

   using Atlas.Core.Logging;

   using NUnit.Framework;

   public class ConsoleLoggerShould
   {
      private ConsoleLogger componentUnderTest;

      [SetUp]
      public void SetupBeforeEachTest()
      {
         this.componentUnderTest = new ConsoleLogger();
      }

      [Test]
      public void NotThrowExceptionWhenLogErrorCalledWithException()
      {
         Assert.That(() => this.componentUnderTest.LogError("LogError", new Exception()), Throws.Nothing);
      }

      [Test]
      public void NotThrowExceptionWhenLogErrorCalledWithoutException()
      {
         Assert.That(() => this.componentUnderTest.LogError("LogError"), Throws.Nothing);
      }

      [Test]
      public void NotThrowExceptionWhenLogWarningCalled()
      {
         Assert.That(() => this.componentUnderTest.LogWarning("LogWarning"), Throws.Nothing);
      }

      [Test]
      public void NotThrowExceptionWhenLogInfoCalled()
      {
         Assert.That(() => this.componentUnderTest.LogInfo("LogInfo"), Throws.Nothing);
      }

      [Test]
      public void NotThrowExceptionWhenLogDebugCalled()
      {
         Assert.That(() => this.componentUnderTest.LogDebug("LogDebug"), Throws.Nothing);
      }

      [Test]
      public void NotThrowExceptionWhenLogInfoCalledWhenLogInfoNotEnabled()
      {
         this.componentUnderTest.InfoLoggingIsEnabled = false;

         Assert.That(() => this.componentUnderTest.LogInfo("LogInfo"), Throws.Nothing);
      }

      [Test]
      public void NotThrowExceptionWhenLogDebugCalledWhenLogInfoNotEnabled()
      {
         this.componentUnderTest.DebugLoggingIsEnabled = false;

         Assert.That(() => this.componentUnderTest.LogDebug("LogDebug"), Throws.Nothing);
      }
   }
}
