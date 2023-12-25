using API_MessageTrigger.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
namespace API_MessageTrigger.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MessageTriggerController : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> UploadExcel([FromForm] attachmentDTO attachment)
        {
            if (attachment == null || attachment.File.Length == 0)
            {
                return BadRequest("Arquivo não fornecido.");
            }

            try
            {

                return Ok("Dados extraídos com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar o arquivo: {ex.Message}");
            }
        }
    }
}
