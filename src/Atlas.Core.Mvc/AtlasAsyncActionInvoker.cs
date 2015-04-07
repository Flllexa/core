// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AtlasAsyncActionInvoker.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System;
   using System.Web.Mvc;
   using System.Web.Mvc.Async;

   public class AtlasAsyncActionInvoker : IAsyncActionInvoker
   {
      private readonly IAsyncActionInvoker actionInvoker;

      public AtlasAsyncActionInvoker(IAsyncActionInvoker actionInvoker)
      {
         this.actionInvoker = actionInvoker;
      }

      public bool InvokeAction(ControllerContext controllerContext, string actionName)
      {
         var result = this.actionInvoker.InvokeAction(controllerContext, actionName);

         if (!result)
         {
            // This is handling the case when the routing matches a controller, but not an action
            AtlasExceptionAction.ExecutePageNotFound(controllerContext, null);
         }

         return true;
      }

      public IAsyncResult BeginInvokeAction(ControllerContext controllerContext, string actionName, AsyncCallback callback, object state)
      {
         return this.actionInvoker.BeginInvokeAction(controllerContext, actionName, callback, controllerContext);
      }

      public bool EndInvokeAction(IAsyncResult asyncResult)
      {
         bool result;

         try
         {
            result = this.actionInvoker.EndInvokeAction(asyncResult);
         }
         catch (InvalidOperationException e)
         {
            if (e.Message != "No route in the route table matches the supplied values.")
            {
               throw;
            }

            result = false;
         }

         if (!result)
         {
            // This is handling the case when the routing matches a controller, but not an action
            AtlasExceptionAction.ExecutePageNotFound((ControllerContext)asyncResult.AsyncState, null);
         }

         return true;
      }
   }
}
