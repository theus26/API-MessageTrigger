using API_MessageTrigger.Domain.DTO;
using API_MessageTrigger.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace API_MessageTrigger.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MessageTriggerController : ControllerBase
    {
        private readonly IServiceMessageTrigger _serviceMessageTrigger;
        public MessageTriggerController(IServiceMessageTrigger serviceMessage)
        {
            _serviceMessageTrigger = serviceMessage;
        }

        [HttpGet]
        public IActionResult HeathCheck()
        {
            return Ok("I´am alive");
        }

        [HttpPost]
        public IActionResult SendMessageTrigger([FromForm] AttachmentDTO attachment)
        {
            try
            {
                var processMensage = _serviceMessageTrigger.ProcessMessage(attachment).Result;
                return Ok(processMensage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Error = ex.Message,
                });
            }
        }

        [HttpPost]
        public IActionResult CreateIntance(CreateInstanceEvolutionDTO CreateInstanceEvolution)
        {
            try
            {
                var createInstance = _serviceMessageTrigger.CreateInstance(CreateInstanceEvolution);
                return Ok(createInstance);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Error = ex.Message,
                });
            }


        }
    }
}
