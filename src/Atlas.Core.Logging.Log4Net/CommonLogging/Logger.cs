// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Logger.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Logging.Log4Net.CommonLogging
{
   using System;

   using Common.Logging;
   using Common.Logging.Factory;

   using log4net.Core;

   public class Logger : AbstractLogger
   {
      private readonly log4net.ILog log;
      private readonly Type declaringType;

      public Logger(log4net.ILog log)
      {
         this.log = log;
         this.declaringType = typeof(Logger);
      }

      public override bool IsTraceEnabled
      {
         get { return false; }
      }

      public override bool IsDebugEnabled
      {
         get { return this.log.IsDebugEnabled; }
      }

      public override bool IsInfoEnabled
      {
         get { return this.log.IsInfoEnabled; }
      }

      public override bool IsWarnEnabled
      {
         get { return this.log.IsWarnEnabled; }
      }

      public override bool IsErrorEnabled
      {
         get { return this.log.IsErrorEnabled; }
      }

      public override bool IsFatalEnabled
      {
         get { return this.log.IsFatalEnabled; }
      }

      protected override void WriteInternal(LogLevel logLevel, object message, Exception exception)
      {
         var level = GetLevel(logLevel);

         this.log.Logger.Log(this.declaringType, level, message, exception);
      }

      private static Level GetLevel(LogLevel logLevel)
      {
         switch (logLevel)
         {
            case LogLevel.Off:
               return Level.Off;
            case LogLevel.All:
               return Level.All;
            case LogLevel.Trace:
               return Level.Trace;
            case LogLevel.Debug:
               return Level.Debug;
            case LogLevel.Info:
               return Level.Info;
            case LogLevel.Warn:
               return Level.Warn;
            case LogLevel.Error:
               return Level.Error;
            case LogLevel.Fatal:
               return Level.Fatal;
            default:
               throw new ArgumentOutOfRangeException("logLevel", logLevel, "Unexpected LogLevel");
         }
      }
   }
}
