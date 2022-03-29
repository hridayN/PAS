using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PAS.API.Constants;
using PAS.API.DTO;
using PAS.API.Models;
using PAS.API.Services.Contract;
using PAS.API.Utilites;

namespace PAS.API.Controllers
{
    /// <summary>
    /// Controller for Reference Data Operations
    /// </summary>
    [ApiController]
    // [Route("[controller]")]
    public class CodeListController : BaseController
    {
        /// <summary>
        /// ICodeListService variable
        /// </summary>
        private readonly ICodeListService _codeListService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="codeListService">CodeListService object Injected By Dependency Injection</param>        
        public CodeListController(ICodeListService codeListService)
        {
            _codeListService = codeListService;
        }

        /// <summary>
        /// Imports the code list from Excel file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="importCodeListRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("importCodeList/v1/codeList")]
        public async Task<IActionResult> ImportCodeList(IFormFile file, [FromForm] string importCodeListRequest)
        {
            ImportCodeListResponse importCodeListResponse = new ImportCodeListResponse()
            {
                MessageInformation = new MessageInformation()
            };

            ImportCodeListRequest importRequest = null;
            try
            {
                importRequest = JsonConvert.DeserializeObject<ImportCodeListRequest>(importCodeListRequest);
            }
            catch
            {
                Utility.AddErrorMessage(importCodeListResponse.MessageInformation, Errors.RequestNotValid);
                Utility.SetStatus(400, Errors.BadRequest.ErrorDescription, importCodeListResponse.MessageInformation);
            }

            if (importCodeListResponse.MessageInformation.MessageStatus.StatusCode == 200)
            {
                importCodeListResponse = await _codeListService.ImportCodeList(file, importRequest);
            }

            return CreateResponse(importCodeListResponse, importRequest);
        }
    }
}
