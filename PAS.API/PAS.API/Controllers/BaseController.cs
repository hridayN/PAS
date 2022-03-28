using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAS.API.DTO.Base;

namespace PAS.API.Controllers
{
    /// <summary>
    /// Base contoller for common functionality
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Method to return response object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceResponse"></param>
        /// <returns></returns>
        protected IActionResult CreateResponse<T>(T serviceResponse) where T : BaseResponse =>
             serviceResponse?.MessageInformation?.MessageStatus?.StatusCode switch
             {
                 401 => Unauthorized(serviceResponse),
                 201 => new ObjectResult(serviceResponse) { StatusCode = StatusCodes.Status201Created },
                 404 => NotFound(serviceResponse),
                 400 => BadRequest(serviceResponse),
                 200 => Ok(serviceResponse),
                 409 => Conflict(serviceResponse),
                 _ => Ok(serviceResponse),
             };

        /// <summary>
        /// Method to return response object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="serviceResponse"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        protected IActionResult CreateResponse<T, R>(T serviceResponse, R request)
            where T : BaseResponse
            where R : BaseRequest
        {
            return CreateResponse(serviceResponse);
        }
    }
}
