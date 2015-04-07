// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AtlasControllerFactory.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System;
   using System.Web.Mvc;
   using System.Web.Mvc.Async;
   using System.Web.Routing;

   public class AtlasControllerFactory : DefaultControllerFactory
   {
      private readonly Func<Type, IController> controllerResolver;
      private readonly Action<IController> controllerReleaser;

      public AtlasControllerFactory(
         Func<Type, IController> controllerResolver,
         Action<IController> controllerReleaser)
      {
         if (controllerResolver == null)
         {
            throw new ArgumentNullException("controllerResolver");
         }

         if (controllerReleaser == null)
         {
            throw new ArgumentNullException("controllerReleaser");
         }

         this.controllerResolver = controllerResolver;
         this.controllerReleaser = controllerReleaser;
      }

      public override void ReleaseController(IController controller)
      {
         if (controller == null)
         {
            throw new ArgumentNullException("controller");
         }

         this.controllerReleaser(controller);
      }

      protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
      {
         if (requestContext == null)
         {
            throw new ArgumentNullException("requestContext");
         }

         if (controllerType == null)
         {
            return this.controllerResolver(typeof(NotFoundController));
         }

         var controller = this.controllerResolver(controllerType);
         var typedController = controller as Controller;

         if (typedController != null)
         {
            typedController.ActionInvoker = new AtlasAsyncActionInvoker(new AsyncControllerActionInvoker());
         }

         return controller;
      }
   }
}
