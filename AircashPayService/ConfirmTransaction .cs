using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AircashPay
{
    public class ConfirmTransaction
    {
        public string PartnerID { get; set; }
        public string PartnerTransactionID { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyID { get; set; }
        public string AircashTransactionID { get; set; }
        public string Signature { get; set; }
    }
}
