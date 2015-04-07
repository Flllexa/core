// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PascalCasedToHyphenatedRoute.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System.Web;
   using System.Web.Routing;

   /// <summary>
   /// Changes pascal cased outbound routes to hyphenated
   /// </summary>
   public class PascalCasedToHyphenatedRoute : Route
   {
      public PascalCasedToHyphenatedRoute(string url, IRouteHandler routeHandler)
         : base(url, routeHandler)
      {
      }

      public PascalCasedToHyphenatedRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
         : base(url, defaults, routeHandler)
      {
      }

      public PascalCasedToHyphenatedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
         : base(url, defaults, constraints, routeHandler)
      {
      }

      public PascalCasedToHyphenatedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
         : base(url, defaults, constraints, dataTokens, routeHandler)
      {
      }

      public override RouteData GetRouteData(HttpContextBase httpContext)
      {
         var routeData = base.GetRouteData(httpContext);

         return routeData;
      }

      public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
      {
         values["controller"] = values["controller"].ToString().PascalCaseToHyphenated();
         values["action"] = values["action"].ToString().PascalCaseToHyphenated();

         return base.GetVirtualPath(requestContext, values);
      }
   }
}
