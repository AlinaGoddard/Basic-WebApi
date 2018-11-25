using System.Collections.Generic;
using System.Linq;

using System;

namespace DataAccess
{
    public class AccountsRepository
    {
        public List<ApiModels.Account> GetAccounts(string connectionString)
        {
            using (var context = new AccountsDatabase(connectionString))
            {
                var accounts = new List<Entities.Account>();
                accounts = context.Accounts.AsNoTracking().ToList();

                if (accounts != null)
                {
                    var accountsDisplay = new List<ApiModels.Account>();
                    accounts.ForEach(a => {
                        accountsDisplay.Add(new ApiModels.Account()
                        {
                            Amount = (float)a.Amount,
                            Name = a.Name,
                            Number = a.Number,
                            Id = a.Id
                        });
                    });
                    return accountsDisplay;
                }
                return null;
            }
        }

        public Entities.Account GetEntity(string connectionString, Guid id)
        {
            using (var context = new AccountsDatabase(connectionString))
            {
                return context.Accounts.AsNoTracking().Single(a => a.Id == id);
            }
        }

        public ApiModels.Account GetAccount(string connectionString, Guid id)
        {
            var account = GetEntity(connectionString, id);

            if (account != null)
            {
                return new ApiModels.Account() {
                    Amount = (float)account.Amount,
                    Name = account.Name,
                    Number = account.Number,
                    Id = account.Id
                };
            }

            return null;
        }

        public bool Add(string connectionString, ApiModels.Account account)
        {       
            if (account != null)
            {
                var transactionsRepo = new TransactionsRepository();
                var transactions = transactionsRepo.GetEntities(connectionString, account.Id);

                using (var context = new AccountsDatabase(connectionString))
                {
                    if (account.Id == null)
                        context.Accounts.Add(new Entities.Account() {
                            Amount = account.Amount,
                            Name = account.Name,
                            Number = account.Number,
                            Id = Guid.NewGuid(),
                            Transactions = transactions
                        });
                    else
                        context.Accounts.Single(a => a.Id == account.Id).Name = account.Name;
                    
                    context.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public bool Update(string connectionString, ApiModels.Account account)
        {
            if (account != null)
            {
                using (var context = new AccountsDatabase(connectionString))
                {
                    var entity = context.Accounts.Single(a => a.Id == account.Id);
                    entity.Name = account.Name;

                    context.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public bool Delete(string connectionString, Guid id)
        {
            using (var context = new AccountsDatabase(connectionString))
            {
                var entity = context.Accounts.Single(a => a.Id == id);
                context.Accounts.Remove(entity);
                context.SaveChanges();
                return true;
            }
        }
    }
}
