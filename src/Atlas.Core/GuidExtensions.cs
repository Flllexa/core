// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GuidExtensions.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core
{
   using System;
   using System.Collections.Generic;

   public static class GuidExtensions
   {
      public static string ToToken(this Guid guid)
      {
         var value = guid.ToString().ToUpper().Replace("-", null);

         return string.Join("-", SplitString(value));
      }

      private static IEnumerable<string> SplitString(string value)
      {
         var i = 0;

         while (i < value.Length)
         {
            var split = value.Substring(i, 4);

            yield return split;

            i += split.Length;
         }
      }
   }
}
