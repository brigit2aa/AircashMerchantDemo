using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        private AircashSimulatorContext AircashSimulatorContext;
        public TransactionService(AircashSimulatorContext aircashSimulatorContext)
        {
            AircashSimulatorContext = aircashSimulatorContext;

        }

        public async Task<List<TransactionDto>> GetTransactions()
        {
            var transaction = await AircashSimulatorContext.Transactions.Select(x => new TransactionDto()
            {
                Id = x.Id,
                Amount = x.Amount,
                IsoCurrencyCode = x.IsoCurrencyCode,
                TransactionId = x.TransactionId,
                AircashTransactionId = x.AircashTransactionId,
                DateTimeUTC = x.DateTimeUTC
            }).ToListAsync();

            return transaction;
        }

        public async Task<int> AddTransactions(TransactionDto transactionDto)
        {
            var transaction2 = new TransactionEntity()
            {
                Amount = transactionDto.Amount,
                IsoCurrencyCode = transactionDto.IsoCurrencyCode,
                TransactionId = transactionDto.TransactionId,
                AircashTransactionId = transactionDto.AircashTransactionId,
                DateTimeUTC = transactionDto.DateTimeUTC
            };
            AircashSimulatorContext.Transactions.Add(transaction2);
            await AircashSimulatorContext.SaveChangesAsync();

            return transaction2.Id;

        }
    }
}
