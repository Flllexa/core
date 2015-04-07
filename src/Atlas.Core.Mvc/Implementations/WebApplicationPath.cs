// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApplicationPath.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System;

   // TODO: IApplicationPath should be taken from the Atlas.Core assembly
   public class WebApplicationPath : IApplicationPath
   {
      public string Path
      {
         get { return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin"); }
      }

      public string ConfigPath
      {
         get { return AppDomain.CurrentDomain.BaseDirectory; }
      }
   }
}
