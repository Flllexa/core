// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICookieHelper.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System;

   public interface ICookieHelper
   {
      bool? ConsentGiven { get; set; }

      bool HasConsent { get; }

      T Get<T>(string name, Func<string, T> converterFunc, T defaultValue);

      string Get(string name);

      T? GetNullable<T>(string name, Func<string, T> converterFunc) where T : struct;

      void Set<T>(string name, T value);

      void Remove(string name);
   }
}
