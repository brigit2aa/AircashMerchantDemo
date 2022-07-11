using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AircashPay
{
    public interface IAircashPayService
    {
        Task<object> GeneratePartnerCode(decimal amount, int currencyID, string description);
        Task ConfirmTransaction(decimal amount, int currencyID, string aircashTransactionID);
        Task CancelTransaction();
    }
}
