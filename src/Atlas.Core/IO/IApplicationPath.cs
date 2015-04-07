// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApplicationPath.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.IO
{
   public interface IApplicationPath
   {
      string Path { get; }

      string ConfigPath { get; }
   }
}
