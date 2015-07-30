using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IdNumbers.Api.Extensions;
using IdNumbers.Api.Models;

namespace IdNumbers.Api.Controllers
{
    [RoutePrefix("api")]
    public class IdNumbersController : ApiController
    {
        // GET api/idnumbers
        [HttpGet]
        [Route("idnumbers")]
        public HttpResponseMessage Get()
        {
            string id = "";
            try
            {
                id = id.NewId();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        // POST api/idnumbers
        [HttpPost]
        [Route("idnumbers")]
        public HttpResponseMessage Post([FromBody] IdNumberModel idNumberModel)
        {
            if (idNumberModel == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "ID Number is required");
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "ID Number should have a length of 13");

            try
            {
                bool isValid =  idNumberModel.idNumber.IsValid(idNumberModel.idNumber);
                if (!isValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "ID Number is invalid");

                return Request.CreateResponse(HttpStatusCode.OK, "ID Number is valid");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
    }
}