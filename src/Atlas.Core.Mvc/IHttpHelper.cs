// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHttpHelper.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System.Web;

   public interface IHttpHelper
   {
      HttpRequestBase CurrentRequest { get; }

      HttpResponseBase CurrentResponse { get; }
   }
}
