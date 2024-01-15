using API_MessageTrigger.Domain.DTO;
using API_MessageTrigger.Domain.Entities;
using API_MessageTrigger.Domain.Interfaces;
using API_MessageTrigger.Service.Validators;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace API_MessageTrigger.Service.Services
{
    public class ServiceMessageTrigger : IServiceMessageTrigger
    {
        private readonly IRequestEvolutionApi _requestEvolutionApi;
        private IBaseService<MessageTrigger> _baseUserService;

        public ServiceMessageTrigger(IRequestEvolutionApi requestEvolutionApi, IBaseService<MessageTrigger> baseService)
        {
            _requestEvolutionApi = requestEvolutionApi;
            _baseUserService = baseService;
        }

        public string CreateInstance(CreateInstanceEvolutionDTO createInstanceEvolution)
        {
            var getBase64 = _requestEvolutionApi.CreateInstance(createInstanceEvolution);

            //TODO: adicionar auto mapper
            //Salvar no banco
            var MessageTrigger = new MessageTrigger()
            {
                NameInstance = createInstanceEvolution.InstanceName,
                PhoneNumber = createInstanceEvolution.Number,
                Token = createInstanceEvolution.Token,
            };

            var addInstance = _baseUserService.Add<MessageTriggerValidator>(MessageTrigger).Id;
            return getBase64.Result;


        }

        public async Task<ResultNumbersDTO> ProcessMessage(AttachmentDTO attachment)
        {
            try
            {
                var extractData = ExtractDataCsv(attachment).Result;
                string MessageBase64 = "";
                List<string> NumerosSucess = new List<string>();
                List<string> NumerosErros = new List<string>();
                var ResultNumbersDTO = new ResultNumbersDTO();

                if (attachment.MediaBase64 is not null)
                {
                    MessageBase64 = ConvertForBase64(attachment.MediaBase64);
                }

                //Chamar Request
                foreach (var numero in extractData)
                {
                    if (attachment.MediaBase64 is not null)
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
                                Caption = attachment.Text,
                                Base64 = MessageBase64
                            }
                        };

                        var sendMensage = _requestEvolutionApi.SendMessageWhatsapp(BodyRequest).Result;

                        if (sendMensage)
                        {
                            NumerosSucess.Add(BodyRequest.Number);
                            ResultNumbersDTO.NumberSucess = NumerosSucess;
                        }
                        else
                        {
                            NumerosErros.Add(BodyRequest.Number);
                            ResultNumbersDTO.NumberError = NumerosErros;
                        }
                    }
                    else
                    {
                        var BodyRequest = new SendMessageEvolutionDTO()
                        {
                            Number = numero,
                            Options = new Options
                            {
                                Delay = 1200,
                                Presence = "composing",
                                LinkPreview = false
                            },
                            TextMessage = new Textmessage()
                            {
                                Text = attachment.Text,
                            }
                        };
                        var sendMensage = _requestEvolutionApi.SendMessageWhatsapp(BodyRequest).Result;

                        if (sendMensage)
                        {
                            NumerosSucess.Add(BodyRequest.Number);
                            ResultNumbersDTO.NumberSucess = NumerosSucess;
                        }
                        else
                        {
                            NumerosErros.Add(BodyRequest.Number);
                            ResultNumbersDTO.NumberError = NumerosErros;
                        }
                    }
                }
                return ResultNumbersDTO;
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
