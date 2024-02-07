using API_MessageTrigger.Domain.DTO;
using API_MessageTrigger.Domain.Entities;
using API_MessageTrigger.Domain.Interfaces;
using API_MessageTrigger.Service.Validators;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace API_MessageTrigger.Service.Services
{
    public class ServiceMessageTrigger(IBaseService<MessageTrigger> baseService, IRequestEvolutionApi requestEvolutionApi) : IServiceMessageTrigger
    {
        #region Criar instancia da evolution
        public string CreateInstance(CreateInstanceEvolutionDTO createInstanceEvolution)
        {
            var getBase64 = requestEvolutionApi.CreateInstance(createInstanceEvolution).Result ?? throw new ArgumentNullException("Error");
            var createMessageTrigger = new MessageTrigger()
            {
                InstanceName = createInstanceEvolution.InstanceName,
                PhoneNumber = createInstanceEvolution.PhoneNumber,
                Token = createInstanceEvolution.Token,
            };

            _ = baseService.Add<MessageTriggerValidator>(createMessageTrigger).Id;
            return getBase64;
        }
        #endregion

        public async Task<ResultNumbersDTO> ProcessMessage(AttachmentDTO attachmentDto)
        {
            string intanceName = baseService.GetInstanceNameByPhoneNumber(attachmentDto.PhoneNumber).InstanceName;

            if (string.IsNullOrEmpty(intanceName)) throw new Exception("Intancia não existe para esse numero");

            ResultNumbersDTO resultNumbersDTO = new ResultNumbersDTO();
            var numbersPhones = await ExtractNumberPhoneFromCsv(attachmentDto);

            if (attachmentDto.MediaBase64 is not null)
            {
                var base64 = ConvertMediaForBase64(attachmentDto.MediaBase64);
                await ProcessMediaMessages(numbersPhones, attachmentDto.Text, base64, resultNumbersDTO, intanceName);
            }
            else
            {
                await ProcessTextMessages(numbersPhones, attachmentDto.Text, resultNumbersDTO, intanceName);
            }

            return resultNumbersDTO;
        }

        private async Task ProcessMediaMessages(IEnumerable<string> numbers, string text, string base64, ResultNumbersDTO resultNumbersDTO, string instanceName)
        {
           
           
            var tasks = numbers.Select(async numero =>
            {
                var bodyRequest = new SendMessageEvolutionDTO
                {
                    Number = numero,
                    Options = new Options { Delay = 1200, Presence = "composing" },
                    MediaMessage = new MediaMessage { MediaType = "image", Caption = text, Base64 = base64 }
                };

                var sendMensage = await requestEvolutionApi.SendMessageWhatsapp(bodyRequest, instanceName).ConfigureAwait(false);

                if (sendMensage)
                {
                    resultNumbersDTO.NumberSucess.Add(bodyRequest.Number);
                }
                else
                {
                    resultNumbersDTO.NumberError.Add(bodyRequest.Number);
                }
            });

            await Task.WhenAll(tasks);
        }

        private async Task ProcessTextMessages(IEnumerable<string> numbers, string text, ResultNumbersDTO? resultNumbersDTO, string instanceName)
        {
            var tasks = numbers.Select(async numero =>
            {
                var bodyRequest = new SendMessageEvolutionDTO
                {
                    Number = numero,
                    Options = new Options { Delay = 1200, Presence = "composing", LinkPreview = false },
                    TextMessage = new Textmessage { Text = text }
                };

                var sendMensage = await requestEvolutionApi.SendMessageWhatsapp(bodyRequest, instanceName).ConfigureAwait(false);

                if (sendMensage)
                {
                    resultNumbersDTO.NumberSucess.Add(bodyRequest.Number);
                }
                else
                {
                    resultNumbersDTO.NumberError.Add(bodyRequest.Number);
                }
            });

            await Task.WhenAll(tasks);
        }

        #region Converter para base64
        private static string ConvertMediaForBase64(IFormFile file)
        {
            using var ms = new MemoryStream();
            file.CopyTo(ms);
            var fileByte = ms.ToArray();
            string base64 = Convert.ToBase64String(fileByte);
            return base64;
        }
        #endregion

        #region Extraindo dado do Excel
        private static async Task<ICollection<string>> ExtractNumberPhoneFromCsv(AttachmentDTO attachment)
        {
            List<string> valoresEncontrados = new List<string>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var stream = new MemoryStream();
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
                        if (cellValue != null && cellValue.ToString().Contains("5579"))
                        {
                            // Adicionar o valor ao array
                            valoresEncontrados.Add(cellValue.ToString());
                        }
                    }
                }
                return valoresEncontrados;
            }
        }
        #endregion
    }
}
