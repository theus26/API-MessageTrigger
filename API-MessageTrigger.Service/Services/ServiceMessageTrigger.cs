using API_MessageTrigger.Domain.DTO;
using API_MessageTrigger.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Net.Mail;

namespace API_MessageTrigger.Service.Services
{
    public class ServiceMessageTrigger : IServiceMessageTrigger
    {
        public async Task<bool> ProcessMessage(AttachmentDTO attachment)
        {
            try
            {
                var extractData = ExtractDataCsv(attachment).Result;
                string MessageBase64 = "";
                if (attachment.ImageBase64 is not null)
                {
                    MessageBase64 = ConvertForBase64(attachment.ImageBase64);
                }

                //Chamar Request
                foreach (var numero in extractData)
                {
                    if (attachment.ImageBase64 is not null)
                    {
                        var BodyRequest = new SendMessageEvolutionDTO()
                        {
                            Number = numero,
                            Options = new Options
                            {
                                Delay = 1200,
                                Presence = "composing",
                            },
                            MediaMessage = new MediaMessage()
                            {
                                MediaType = "image",
                                Base64 = MessageBase64
                            }
                        };


                    }
                }
             return true;
        }
            catch
            {
                throw;
            }

        }

        private string ConvertForBase64(IFormFile file)
        {
            try
            {
                using var ms = new MemoryStream();
                file.CopyTo(ms);
                var fileByte = ms.ToArray();
                string base64 = Convert.ToBase64String(fileByte);
                return base64;
            }
            catch
            {
                throw;
            }
        }

        private async Task<List<string>> ExtractDataCsv(AttachmentDTO attachment)
        {
            try
            {
                List<string> valoresEncontrados = new List<string>();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var stream = new MemoryStream())
                {
                    await attachment.File.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        // Assumindo a primeira planilha
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                        int rowCount = worksheet.Dimension.Rows;
                        int colCount = worksheet.Dimension.Columns;

                        for (int row = 1; row <= rowCount; row++)
                        {
                            for (int col = 1; col <= colCount; col++)
                            {
                                var cellValue = worksheet.Cells[row, col].Value;
                                // Aqui você pode processar ou armazenar os dados conforme necessário
                                Console.Write(cellValue + "\t");
                                if (cellValue != null && cellValue.ToString().Contains("5579"))
                                {
                                    // Adicionar o valor ao array
                                    valoresEncontrados.Add(cellValue.ToString());
                                    Console.WriteLine($"Encontrado '5579' na célula ({row}, {col}): {cellValue}");
                                }
                            }
                            Console.WriteLine();
                        }
                        return valoresEncontrados;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
