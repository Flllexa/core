// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonErrorResult.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Net;
   using System.Web.Mvc;
   using System.Web.Script.Serialization;

   public class JsonErrorResult : JsonResult
   {
      private readonly ModelStateDictionary modelState;
      private readonly HttpStatusCode statusCode;

      public JsonErrorResult()
      {
         this.modelState = null;
         this.statusCode = HttpStatusCode.InternalServerError;
      }

      public JsonErrorResult(ModelStateDictionary modelState)
      {
         if (modelState == null)
         {
            throw new ArgumentNullException("modelState");
         }

         this.modelState = modelState;
         this.statusCode = HttpStatusCode.BadRequest;
      }

      public override void ExecuteResult(ControllerContext context)
      {
         if (context == null)
         {
            throw new ArgumentNullException("context");
         }

         if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
         {
            throw new InvalidOperationException("To allow GET requests, set JsonRequestBehavior to AllowGet.");
         }

         var response = context.HttpContext.Response;

         response.StatusCode = (int)this.statusCode;
         response.ContentType = !string.IsNullOrEmpty(this.ContentType) ? this.ContentType : "application/json";

         if (this.ContentEncoding != null)
         {
            response.ContentEncoding = this.ContentEncoding;
         }

         var errors = new Dictionary<string, IEnumerable<string>>();

         if (this.modelState != null)
         {
            foreach (var keyValue in this.modelState)
            {
               errors[keyValue.Key] = keyValue.Value.Errors.Select(e => e.ErrorMessage);
            }
         }
         else
         {
            errors.Add(string.Empty, new[] { "An unexpected error has occurred" });
         }

         response.Write(new JavaScriptSerializer().Serialize(new { Errors = errors }));
      }
   }
}
