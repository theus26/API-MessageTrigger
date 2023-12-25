using API_MessageTrigger.Domain.DTO;
using API_MessageTrigger.Domain.Interfaces;
using OfficeOpenXml;
using System.Net.Mail;

namespace API_MessageTrigger.Service.Services
{
    public class ServiceMessageTrigger : IServiceMessageTrigger
    {
        public async Task<bool> ProcessMessage(attachmentDTO attachment)
        {
            try
            {
                var extractData = ExtractDataCsv(attachment).Result;
                //Chamar Request
                foreach (var numero in extractData)
                {
                    
                }
                return true;
            }
            catch
            {
                throw;
            }

        }

        private async Task<List<string>> ExtractDataCsv(attachmentDTO attachment)
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
