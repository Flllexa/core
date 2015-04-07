// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultApplicationPath.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.IO
{
   using System;

   public class DefaultApplicationPath : IApplicationPath
   {
      public string Path
      {
         get { return AppDomain.CurrentDomain.BaseDirectory; }
      }

      public string ConfigPath
      {
         get { return AppDomain.CurrentDomain.BaseDirectory; }
      }
   }
}
