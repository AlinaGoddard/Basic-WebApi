using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class TransactionsRepository
    {
        public List<Entities.Transaction> GetEntities(string connectionString, Guid id)
        {
            using (var context = new AccountsDatabase(connectionString))
            {
                return context.Transactions.AsNoTracking().Where(t => t.AccountId == id).ToList();
            }
        }

        public List<ApiModels.Transaction> GetTransactions(string connectionString, Guid id)
        {
            var transactions = GetEntities(connectionString, id);
            if (transactions != null)
            {
                var transactionsDisplay = new List<ApiModels.Transaction>();
                transactions.ForEach(t => {
                    if (t.Date != null)
                        transactionsDisplay.Add(new ApiModels.Transaction((float)t.Amount, (DateTime)t.Date));
                });
                return transactionsDisplay;
            }
            return null;
        }

        public bool Add(string connectionString, ApiModels.Transaction transaction, Guid id)
        {
            if (transaction != null)
            {
                using (var context = new AccountsDatabase(connectionString))
                {
                    var accountsRepo = new AccountsRepository();
                    var account = accountsRepo.GetAccount(connectionString, id);
                    account.Amount += transaction.Amount;
                    if (!accountsRepo.Update(connectionString, account))
                        return false;

                    context.Transactions.Add(new Entities.Transaction()
                    {
                        Amount = transaction.Amount,
                        Date = transaction.Date,
                        AccountId = account.Id,
                        Id = Guid.NewGuid()
                    });

                    context.SaveChanges();

                    return true;
                }
            }

            return false;
        }
    }
}
