using API_MessageTrigger.Domain.DTO;
using API_MessageTrigger.Domain.Entities;
using API_MessageTrigger.Domain.Interfaces;

namespace API_MessageTrigger.Service.Services
{
    public class ServiceMessageTrigger(IBaseService<MessageTrigger> baseService, IRequestEvolutionApi requestEvolutionApi) : IServiceMessageTrigger
    {
        #region Criar instancia da evolution
        public bool CreateInstance(CreateInstanceEvolutionDTO createInstanceEvolution)
        {
            return requestEvolutionApi.CreateInstance(createInstanceEvolution).Result;
        }
        #endregion

        #region Obter QrCode para conectar 
        public ConnectInstanceDTO? ConnectInstance(string instanceName)
        {
            if (string.IsNullOrEmpty(instanceName)) throw new ArgumentNullException("instanceName, nula ou vazia");
            return requestEvolutionApi.ConnectInstance(instanceName).Result;
        }

        #endregion

        public List<InstancesDTO>? FetchInstance(string accountId)
        {
            var instances = requestEvolutionApi.FetchInstances().Result;
            var query = "account_" + accountId;
            return instances?.FindAll(x => x.Instance.InstanceName.Contains(query));
        }

        public void LogoutInstances(string instanceName)
        {
            if (string.IsNullOrEmpty(instanceName)) throw new ArgumentNullException("instanceName, nula ou vazia");
            requestEvolutionApi.LogoutInstances(instanceName);
        }

        public void DeleteInstances(string instanceName)
        {
            if (string.IsNullOrEmpty(instanceName)) throw new ArgumentNullException("instanceName, nula ou vazia");
            requestEvolutionApi.DeleteInstances(instanceName);
        }

        public async Task<ResultNumbersDTO> ProcessMessage(TriggerDTO triggerDto)
        {
            ResultNumbersDTO resultNumbersDTO = new ResultNumbersDTO();
            var tasks = triggerDto.Messages.Select(async message =>
            {
                var numbersPhones = triggerDto.Numbers;
                if (message.Type != "text")
                {
                    await ProcessMediaMessages(numbersPhones, message.Message, message.Media, resultNumbersDTO, triggerDto.InstanceName, message?.Type);
                }
                else
                {
                    await ProcessTextMessages(numbersPhones, message.Message, resultNumbersDTO, triggerDto.InstanceName);
                }

            });

            await Task.WhenAll(tasks);
            return resultNumbersDTO;
        }

        private async Task ProcessMediaMessages(IEnumerable<string> numbers, string? text, string? base64, ResultNumbersDTO resultNumbersDTO, string? instanceName, string? type)
        {
            var tasks = numbers.Select(async numero =>
            {
                var bodyRequest = new SendMessageEvolutionDTO
                {
                    Number = numero,
                    Options = new Options { Delay = 1200, Presence = "composing" },
                    MediaMessage = new MediaMessage { MediaType = type, Caption = text ?? "", Base64 = base64 }
                };

                var sendMensage = await requestEvolutionApi.SendMessageWhatsapp(bodyRequest, instanceName).ConfigureAwait(false);

                if (sendMensage)
                {
                    resultNumbersDTO?.NumberSucess?.Add(bodyRequest.Number);
                }
                else
                {
                    resultNumbersDTO?.NumberError?.Add(bodyRequest.Number);
                }
            });

            await Task.WhenAll(tasks);
        }

        private async Task ProcessTextMessages(IEnumerable<string> numbers, string? text, ResultNumbersDTO? resultNumbersDTO, string? instanceName)
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
                    resultNumbersDTO?.NumberSucess?.Add(bodyRequest.Number);
                }
                else
                {
                    resultNumbersDTO?.NumberError?.Add(bodyRequest.Number);
                }
            });

            await Task.WhenAll(tasks);
        }
    }
}
