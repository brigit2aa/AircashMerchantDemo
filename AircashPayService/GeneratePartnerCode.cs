using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AircashPay
{
    public class GeneratePartnerCode
    {
        public string PartnerID { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyID { get; set; }
        public string PartnerTransactionID { get; set; }
        public string Description { get; set; }
        public int? ValidForPeriod { get; set; }
        public string LocationID { get; set; }
        public string Signature { get; set; }
        public string CodeLink { get; set; }
    }
}
