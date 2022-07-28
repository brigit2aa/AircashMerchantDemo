using Microsoft.AspNetCore.Mvc;
using Services.Signature;
using Services.Transactions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ISignatureService _signatureService;

        public TransactionController(ITransactionService transactionService, ISignatureService signatureService)
        {
            _transactionService = transactionService;
            _signatureService = signatureService;
        }


        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            return Ok(await _transactionService.GetTransactions());
  
        }

        [HttpPost]
        public async Task<IActionResult> AddTransactions([FromBody] TransactionDto transactionDto)
        {
            var id = await _transactionService.AddTransactions(transactionDto);
            return CreatedAtAction(nameof(GetTransactions), new { id = id, controller = "Transaction" }, id);
        }

        [HttpGet]
        public async Task<IActionResult> GenerateSignature()
        {
            return Ok(_signatureService.GenerateSignature("To potpiši"));
        }
    }
}
 