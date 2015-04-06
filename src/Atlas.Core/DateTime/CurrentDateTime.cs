// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrentDateTime.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.DateTime
{
   using System;

   public class CurrentDateTime : IDateTime
   {
      public DateTime Date
      {
         get { return DateTime.Today; }
      }

      public DateTime DateTime
      {
         get { return DateTime.Now; }
      }
   }
}
