// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggerFactoryAdapter.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Logging.Log4Net.CommonLogging
{
   using Common.Logging;
   using Common.Logging.Factory;

   public class LoggerFactoryAdapter : AbstractCachingLoggerFactoryAdapter
   {
      protected override ILog CreateLogger(string name)
      {
         return new Logger(log4net.LogManager.GetLogger(name));
      }
   }
}
