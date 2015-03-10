// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiExpectedException.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.WebApi.Exceptions
{
   using System;

   public class WebApiExpectedException : Exception
   {
      public WebApiExpectedException(string reason, string message)
         : base(message)
      {
         this.Reason = reason;
      }

      public string Reason { get; private set; }
   }
}
