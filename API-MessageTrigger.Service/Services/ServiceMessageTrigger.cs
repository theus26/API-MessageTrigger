using API_MessageTrigger.Domain.DTO;
using API_MessageTrigger.Domain.Entities;
using API_MessageTrigger.Domain.Interfaces;
using API_MessageTrigger.Service.Validators;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;

namespace API_MessageTrigger.Service.Services
{
    public class ServiceMessageTrigger(IBaseService<MessageTrigger> baseService, IRequestEvolutionApi requestEvolutionApi) : IServiceMessageTrigger
    {
        private readonly IBaseService<MessageTrigger> _baseUserService = baseService;
        private readonly IRequestEvolutionApi _requestEvolutionApi = requestEvolutionApi;


        #region Criar instancia da evolution
        public string CreateInstance(CreateInstanceEvolutionDTO createInstanceEvolution)
        {
            var getBase64 = _requestEvolutionApi.CreateInstance(createInstanceEvolution).Result ?? throw new ArgumentNullException("Error");
            var createMessageTrigger = new MessageTrigger()
            {
                InstanceName = createInstanceEvolution.InstanceName,
                PhoneNumber = createInstanceEvolution.PhoneNumber,
                Token = createInstanceEvolution.Token,
            };

            _ = _baseUserService.Add<MessageTriggerValidator>(createMessageTrigger).Id;
            return getBase64;
        }
        #endregion

        public async Task<ResultNumbersDTO> ProcessMessage(AttachmentDTO attachment)
        {
            try
            {
                ResultNumbersDTO resultNumbersDTO = new ResultNumbersDTO();
                var extractData = await ExtractDataCsv(attachment);

                if (attachment.MediaBase64 is not null)
                {
                    var messageBase64 = ConvertForBase64(attachment.MediaBase64);
                    await ProcessMediaMessages(extractData, attachment.Text, messageBase64, resultNumbersDTO);
                }
                else
                {
                    await ProcessTextMessages(extractData, attachment.Text, resultNumbersDTO);
                }

                return resultNumbersDTO;
            }
            catch
            {
                throw;
            }

        }

        private async Task ProcessMediaMessages(IEnumerable<string> numbers, string text, string messageBase64, ResultNumbersDTO resultNumbersDTO)
        {
           
            var tasks = numbers.Select(async numero =>
            {
                var bodyRequest = new SendMessageEvolutionDTO
                {
                    Number = numero,
                    Options = new Options { Delay = 1200, Presence = "composing" },
                    MediaMessage = new MediaMessage { MediaType = "image", Caption = text, Base64 = messageBase64 }
                };

                var sendMensage = await _requestEvolutionApi.SendMessageWhatsapp(bodyRequest).ConfigureAwait(false);

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

        private async Task ProcessTextMessages(IEnumerable<string> numbers, string text, ResultNumbersDTO? resultNumbersDTO)
        {
            var tasks = numbers.Select(async numero =>
            {
                var bodyRequest = new SendMessageEvolutionDTO
                {
                    Number = numero,
                    Options = new Options { Delay = 1200, Presence = "composing", LinkPreview = false },
                    TextMessage = new Textmessage { Text = text }
                };

                var sendMensage = await _requestEvolutionApi.SendMessageWhatsapp(bodyRequest).ConfigureAwait(false);

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
        private static string ConvertForBase64(IFormFile file)
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
        #endregion

        #region Extraindo dado do Excel
        private static async Task<List<string>> ExtractDataCsv(AttachmentDTO attachment)
        {
            try
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
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
