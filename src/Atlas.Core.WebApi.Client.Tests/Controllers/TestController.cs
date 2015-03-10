// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestController.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.WebApi.Client.Tests.Controllers
{
   using System;
   using System.Web.Http;

   using Atlas.Core.WebApi.Exceptions;

   public class TestController : ApiController
   {
      [HttpGet]
      [Route("api/test-controller/get")]
      public GetResponse Get(string arg)
      {
         return new GetResponse { Argument = arg };
      }

      [HttpGet]
      [Route("api/test-controller/get-unexpected-exception")]
      public GetResponse GetUnexpectedException()
      {
         throw new Exception("This is unexpected");
      }

      [HttpGet]
      [Route("api/test-controller/get-expected-exception")]
      public GetResponse GetExpectedException()
      {
         throw new WebApiExpectedException("myReason", "myMessage");
      }

      [HttpPost]
      [Route("api/test-controller/post")]
      public PostResponse Post(PostRequest request)
      {
         return new PostResponse { Subject = request.Subject, Body = request.Body };
      }

      [HttpPost]
      [Route("api/test-controller/post-unexpected-exception")]
      public PostResponse PostUnexpectedException(PostRequest request)
      {
         throw new Exception("This is unexpected");
      }

      [HttpPost]
      [Route("api/test-controller/post-expected-exception")]
      public PostResponse PostExpectedException(PostRequest request)
      {
         throw new WebApiExpectedException("myReason", "myMessage");
      }
   }
}
