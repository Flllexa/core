// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotFoundController.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System.Web.Mvc;
   using System.Web.Routing;

   public class NotFoundController : ControllerBase
   {
      protected override void Execute(RequestContext requestContext)
      {
         // This is handling the case when the routing cannot match a controller, or when
         // the routing may identify an actual file (eg. *.cshtml). The later only seems
         // to end up here though if AtlasExceptionHttpModule is used.
         AtlasExceptionAction.ExecutePageNotFound(requestContext, null);
      }

      protected override void ExecuteCore()
      {
      }
   }
}
