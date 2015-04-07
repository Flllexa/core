// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AllowHttpAttribute.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System;

   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
   public class AllowHttpAttribute : Attribute
   {
   }
}
