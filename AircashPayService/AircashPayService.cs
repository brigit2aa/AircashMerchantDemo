using Newtonsoft.Json;
using Service.HttpRequestService;
using Services.Setting;
using Services.Signature;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace Service.AircashPay
{
    public class AircashPayService : IAircashPayService
    {
        private readonly ISettingService _settingService;
        private readonly ISignatureService _signatureService;
        private IHttpRequestService _httpRequestService;

        public AircashPayService(ISettingService settingService, ISignatureService signatureService, IHttpRequestService httpRequestService)
        {
            _settingService = settingService;
            _signatureService = signatureService;
            _httpRequestService = httpRequestService;
        }

        public async Task<string> GeneratePartnerCode(decimal amount, int currencyID, string description, string locationID)
        {
            var partnerTransactionID = Guid.NewGuid();
            var partnerID = _settingService.PartnerID;
            //var responseContent = new object();
            var generatePartnerCode = new GeneratePartnerCode()
            {
                PartnerID = partnerID,
                Amount = amount,
                CurrencyID = currencyID,
                PartnerTransactionID = partnerTransactionID.ToString(),
                Description = description,
                LocationID = locationID,
            };
            //var dataToString = _signatureService.ConvertObjectToString(generatePartnerCode);
            var dataToString = $"Amount={generatePartnerCode.Amount}&CurrencyID={generatePartnerCode.CurrencyID}&Description={generatePartnerCode.Description}&LocationID={generatePartnerCode.LocationID}&PartnerID={generatePartnerCode.PartnerID}&PartnerTransactionID={generatePartnerCode.PartnerTransactionID}"; 
            var signature = _signatureService.GenerateSignature(dataToString);
            generatePartnerCode.Signature = signature;
            var response = await _httpRequestService.SendHttpRequest(generatePartnerCode, HttpMethod.Post, "https://dev-m3.aircash.eu/api/AircashPay/GeneratePartnerCode");
          
            /*if (response.ResponseCode == System.Net.HttpStatusCode.OK)
            {
                responseContent = JsonConvert.DeserializeObject<GeneratePartnerCodeResponse>(response.ResponseContent);
            }
            else
            {
                responseContent = response.ResponseContent;
            }*/
            // return (string)responseContent;
            var responseContent = JsonConvert.DeserializeObject<GeneratePartnerCodeResponse>(response.ResponseContent);
            return responseContent.CodeLink;
        }
    } 
}
