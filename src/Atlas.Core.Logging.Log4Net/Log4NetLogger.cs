﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Log4NetLogger.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Logging.Log4Net
{
   using System;

   using log4net;

   public class Log4NetLogger : ILogger
   {
      private readonly ILog log;

      public Log4NetLogger(ILog log)
      {
         this.log = log;
      }

      public Log4NetLogger(string name)
         : this(LogManager.GetLogger(name))
      {
      }

      public bool InfoLoggingIsEnabled
      {
         get { return this.log.IsInfoEnabled; }
      }

      public bool DebugLoggingIsEnabled
      {
         get { return this.log.IsDebugEnabled; }
      }

      public static ILogger FromConfig(ILog log)
      {
         log4net.Config.XmlConfigurator.Configure();

         return new Log4NetLogger(log);
      }

      public static ILogger FromConfig(string name)
      {
         log4net.Config.XmlConfigurator.Configure();

         return new Log4NetLogger(name);
      }

      public void LogError(string format, Exception exception, params object[] args)
      {
         var message = string.Format(format, args);
         this.log.Error(message, exception);
      }

      public void LogError(string format, params object[] args)
      {
         this.log.ErrorFormat(format, args);
      }

      public void LogWarning(string format, params object[] args)
      {
         this.log.WarnFormat(format, args);
      }

      public void LogInfo(string format, params object[] args)
      {
         if (!this.log.IsInfoEnabled)
         {
            return;
         }

         this.log.InfoFormat(format, args);
      }

      public void LogDebug(string format, params object[] args)
      {
         if (!this.log.IsDebugEnabled)
         {
            return;
         }

         this.log.DebugFormat(format, args);
      }
   }
}
