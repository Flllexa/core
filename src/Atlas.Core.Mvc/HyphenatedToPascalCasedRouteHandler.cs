// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HyphenatedToPascalCasedRouteHandler.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System.Web;
   using System.Web.Mvc;
   using System.Web.Routing;

   /// <summary>
   /// Changes hyphenated inbound routes to pascal cased
   /// </summary>
   public class HyphenatedToPascalCasedRouteHandler : MvcRouteHandler
   {
      protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
      {
         var rd = requestContext.RouteData;

         rd.Values["controller"] = rd.Values["controller"].ToString().HyphenatedToPascalCase();
         rd.Values["action"] = rd.Values["action"].ToString().HyphenatedToPascalCase();

         return base.GetHttpHandler(requestContext);
      }
   }
}
