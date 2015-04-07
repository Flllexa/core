// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System.Security;
   using System.Text;

   public static class StringExtensions
   {
      public static SecureString ToSecureString(this string value)
      {
         if (value == null)
         {
            return null;
         }

         var secureString = new SecureString();

         foreach (var c in value)
         {
            secureString.AppendChar(c);
         }

         secureString.MakeReadOnly();

         return secureString;
      }

      public static string HyphenatedToPascalCase(this string hypenated)
      {
         if (string.IsNullOrEmpty(hypenated))
         {
            return hypenated;
         }

         hypenated = hypenated.ToLower();
         var pascalCased = new StringBuilder();

         pascalCased.Append(char.ToUpperInvariant(hypenated[0]));

         for (var i = 1; i < hypenated.Length; i++)
         {
            if (hypenated[i] != '-')
            {
               pascalCased.Append(hypenated[i]);
            }
            else if (i + 1 < hypenated.Length)
            {
               i++;
               pascalCased.Append(char.ToUpperInvariant(hypenated[i]));
            }
         }

         return pascalCased.ToString();
      }

      public static string PascalCaseToHyphenated(this string pascalCased)
      {
         var hypenatedPath = new StringBuilder();

         for (var i = 0; i < pascalCased.Length; i++)
         {
            if (i > 0 && char.IsUpper(pascalCased[i]))
            {
               hypenatedPath.Append('-');
            }

            hypenatedPath.Append(pascalCased[i]);
         }

         return hypenatedPath.ToString().ToLower();
      }
   }
}
