using System;
using System.Collections.Generic;

namespace DataAccess
{
    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class
    {
        public virtual IList<T> GetAll(string connectionString, Guid id)
        {

        }

        public virtual IList<T> GetEntities(string connectionString, Guid id)
        {
            using (var context = new AccountsDatabase(connectionString))
            {
                //TODO: how to do this
                return context.Transactions.AsNoTracking().Where(t => t.AccountId == id).ToList();
            }
        }

        public virtual bool Add(string connectionString, T itemToAdd, Guid id)
        {

        }
    }
}

