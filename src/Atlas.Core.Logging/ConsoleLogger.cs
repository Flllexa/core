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
      public void LogError(string format, Exception exception, params object[] args)
      {
         var message = string.Format(format, args);
         Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss.fff} ERROR {1}", DateTime.Now, message);
         Console.WriteLine(exception.ToString());
      }

      public void LogWarning(string format, params object[] args)
      {
         var message = string.Format(format, args);
         Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss.fff} WARN  {1}", DateTime.Now, message);
      }

      public void LogInfo(string format, params object[] args)
      {
         var message = string.Format(format, args);
         Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss.fff} INFO  {1}", DateTime.Now, message);
      }

      public void LogDebug(string format, params object[] args)
      {
         var message = string.Format(format, args);
         Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss.fff} DEBUG {1}", DateTime.Now, message);
      }
   }
}
