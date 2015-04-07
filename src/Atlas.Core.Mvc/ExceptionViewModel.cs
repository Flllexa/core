// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionViewModel.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System;

   public class ExceptionViewModel
   {
      private readonly Guid exceptionGuid;
      private readonly Exception exception;

      public ExceptionViewModel()
      {
         this.exceptionGuid = Guid.NewGuid();
      }

      public ExceptionViewModel(Exception exception)
         : this()
      {
         if (exception == null)
         {
            throw new ArgumentNullException("exception");
         }

         this.exception = exception;
      }

      public Guid ExceptionGuid
      {
         get { return this.exceptionGuid; }
      }

      public Exception Exception
      {
         get { return this.exception; }
      }
   }
}