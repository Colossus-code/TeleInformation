using Contracts;
using Serilog;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class PhoneInformationController : ApiController
    {
        private readonly IPhoneInformationService _phoneInformationService;
        private readonly ILogger _logger;
        public PhoneInformationController(IPhoneInformationService phoneInformationService, ILogger logger)
        {
            _logger = logger;
            _phoneInformationService = phoneInformationService;
        }
        /// <summary>
        /// Endpoint to retrieve information about a model or get a list of them.
        /// </summary>
        /// <param name="entryModel">Parameters to identify the phone</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> GetPhones(InputModel entryModel)
        {
            try
            {

                var response = await _phoneInformationService.GetPhone(entryModel.MobileFabricator, entryModel.MobileID, entryModel.MobileName);
                
                if(string.IsNullOrEmpty(response) || response.Equals("null"))
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception has been thrown: {ex.Message}, \nStackTrace: {ex.StackTrace}");
                return BadRequest("An error occurred during the petition.");
            }

        }
    }

}