// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleLogger.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Logging
{
   using System;

   public class ConsoleLogger : ILogger
   {
      public ConsoleLogger()
      {
         this.InfoLoggingIsEnabled = true;
         this.DebugLoggingIsEnabled = true;
      }

      public bool InfoLoggingIsEnabled { get; set; }

      public bool DebugLoggingIsEnabled { get; set; }

      public void LogError(string format, Exception exception, params object[] args)
      {
         this.LogError(format, args);
         Console.WriteLine(exception.ToString());
      }

      public void LogError(string format, params object[] args)
      {
         var message = string.Format(format, args);
         Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss.fff} ERROR {1}", DateTime.Now, message);
      }

      public void LogWarning(string format, params object[] args)
      {
         var message = string.Format(format, args);
         Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss.fff} WARN  {1}", DateTime.Now, message);
      }

      public void LogInfo(string format, params object[] args)
      {
         if (!this.InfoLoggingIsEnabled)
         {
            return;
         }

         var message = string.Format(format, args);
         Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss.fff} INFO  {1}", DateTime.Now, message);
      }

      public void LogDebug(string format, params object[] args)
      {
         if (!this.DebugLoggingIsEnabled)
         {
            return;
         }

         var message = string.Format(format, args);
         Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss.fff} DEBUG {1}", DateTime.Now, message);
      }
   }
}
