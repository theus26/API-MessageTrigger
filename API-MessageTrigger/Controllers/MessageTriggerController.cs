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
                List<string> valoresEncontrados = new List<string>();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var stream = new MemoryStream())
                {
                    await attachment.File.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Assumindo a primeira planilha

                        int rowCount = worksheet.Dimension.Rows;
                        int colCount = worksheet.Dimension.Columns;

                        for (int row = 1; row <= rowCount; row++)
                        {
                            for (int col = 1; col <= colCount; col++)
                            {
                                var cellValue = worksheet.Cells[row, col].Value;
                                // Aqui você pode processar ou armazenar os dados conforme necessário
                                Console.Write(cellValue + "\t");
                                if (cellValue != null && cellValue.ToString().Contains("79"))
                                {
                                    // Adicionar o valor ao array
                                    valoresEncontrados.Add(cellValue.ToString());
                                    Console.WriteLine($"Encontrado '5579' na célula ({row}, {col}): {cellValue}");
                                }
                            }
                            Console.WriteLine();
                        }
                    }
                }

                return Ok("Dados extraídos com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar o arquivo: {ex.Message}");
            }
        }
    }
}
