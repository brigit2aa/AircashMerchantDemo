﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class GeneratePartnerCodeRequest
    {
        public decimal Amount { get; set; }
        public int CurrencyID { get; set; }
        public string Description { get; set; }
        public string LocationID { get; set; }
        public string CodeLink { get; set; }
    }
}
