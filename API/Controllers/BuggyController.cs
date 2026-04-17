using API.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("unauthorized")]
        public ActionResult<string> GetUnauthorized()
        {
            return Unauthorized();
        }
        [HttpGet("badrequest")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This is a bad request");
        }
        [HttpGet("notfound")]
        public ActionResult<string> GetNotFound()
        {
            return NotFound("This is not found");
        }
        [HttpGet("servererror")]
        public ActionResult<string> GetServerError()
        {
            throw new Exception("This is a server error");
        }
        [HttpPost("validationerror")]
        public ActionResult<string> GetValidationError(CreateProductDto product)
        {
            return ValidationProblem("This is a validation error");
        }
        [HttpGet("ok")]
        public ActionResult<string> GetOk()
        {
            return Ok("This is ok");
        }
    }
}
