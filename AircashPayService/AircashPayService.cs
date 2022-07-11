using DataAccess;
using Domain.Entities;
using Newtonsoft.Json;
using Service.AircashPay;
using Services.HttpRequest;
using Services.Setting;
using Services.Signature;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace AircashPayService
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

        public async Task<object> GeneratePartnerCode(decimal amount, int currencyID,  string description)
        {
            var partnerTransactionID = Guid.NewGuid();
            var partnerID = _settingService.PartnerID;
            var responseContent = new object();
            var generatePartnerCode = new GeneratePartnerCode()
            {
                PartnerID = partnerID,
                Amount = amount,
                CurrencyID = currencyID,
                PartnerTransactionID = partnerTransactionID.ToString(),
                Description = description,
                LocationID = null,
                
            };
            
            var dataToString = _signatureService.ConvertObjectToString(generatePartnerCode);
            var signature = _signatureService.GenerateSignature(dataToString, _settingService.PrivateKeyPath, _settingService.PrivateKeyPass);
            generatePartnerCode.Signature = signature;
            var response = await _httpRequestService.SendHttpRequest(generatePartnerCode, HttpMethod.Post, "https://staging-m3.aircash.eu/api/AircashPay/GeneratePartnerCode");
           
            if(response.ResponseCode == System.Net.HttpStatusCode.OK)
            {
                responseContent = response.ResponseContent;
            }
            else
            {
                responseContent = response.ResponseContent;
            }
            return responseContent;

            /*var responseContent = response.ResponseContent;
            return responseContent;*/
        }

        public async Task ConfirmTransaction(decimal amount, int currencyID, string aircashTransactionID)
        {
            var partnerTransactionID = Guid.NewGuid();
            var partnerID = _settingService.PartnerID;

            var confirmTransaction = new ConfirmTransaction()
            {
                PartnerID = partnerID,
                PartnerTransactionID = partnerTransactionID.ToString(),
                Amount = amount,
                CurrencyID = currencyID,
                AircashTransactionID = aircashTransactionID,
            };
            var dataToString = SignatureService.ConvertObjectToString(confirmTransaction);
            var signature = SignatureService.GenerateSignature(dataToString, _settingService.PrivateKeyPath, _settingService.PrivateKeyPass);
            confirmTransaction.Signature = signature;
        }

        public async Task CancelTransaction()
        {
            var partnerTransactionID = Guid.NewGuid();
            var partnerID = _settingService.PartnerID;

            var cancelTransaction = new CancelTransaction()
            {
                PartnerTransactionID = partnerTransactionID.ToString(),
                PartnerID = partnerID,
            };
            var dataToString = SignatureService.ConvertObjectToString(cancelTransaction);
            var signature = SignatureService.GenerateSignature(dataToString, _settingService.PrivateKeyPath, _settingService.PrivateKeyPass);
            cancelTransaction.Signature = signature;
 
        }
    }
}
