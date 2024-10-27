using API_MessageTrigger.Domain.DTO;
using API_MessageTrigger.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace API_MessageTrigger.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MessageTriggerController(IServiceMessageTrigger _serviceMessage) : ControllerBase
    {
        [HttpGet]
        public IActionResult HeathCheck()
        {
            return Ok("I´am alive");
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResultNumbersDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
        public IActionResult SendMessageTrigger(TriggerDTO trigger)
        {
            try
            {
                var processMensage = _serviceMessage.ProcessMessage(trigger).Result;
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
        public IActionResult CreateInstance(CreateInstanceEvolutionDTO createInstanceEvolution)
        {
            try
            {
                var createInstance = _serviceMessage.CreateInstance(createInstanceEvolution);
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

        [HttpGet]
        public IActionResult ConnectInstance(string instanceName)
        {
            try
            {
                var base64 = _serviceMessage.ConnectInstance(instanceName);
                return Ok(base64);
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

        [HttpGet]
        public IActionResult FetchInstance()
        {
            try
            {
                var instances = _serviceMessage.FetchInstance();
                return Ok(instances);
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

        [HttpDelete]
        public IActionResult LogoutInstance(string instanceName)
        {
            try
            {
                 _serviceMessage.LogoutInstances(instanceName);
                return Ok();
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

        [HttpDelete]
        public IActionResult DeleteInstance(string instanceName)
        {
            try
            {
                _serviceMessage.DeleteInstances(instanceName);
                return Ok();
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
