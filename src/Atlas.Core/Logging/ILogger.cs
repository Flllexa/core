// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Logging
{
   using System;

   public interface ILogger
   {
      bool InfoLoggingIsEnabled { get; }

      bool DebugLoggingIsEnabled { get; }

      void LogError(string format, Exception exception, params object[] args);

      void LogError(string format, params object[] args);

      void LogWarning(string format, params object[] args);

      void LogInfo(string format, params object[] args);

      void LogDebug(string format, params object[] args);
   }
}
