using System;
using System.Collections.Generic;

namespace DataAccess
{
    public interface IGenericDataRepository<T> where T : class
    {
        IList<T> GetAll(string connectionString, Guid id);
        IList<T> GetEntities(string connectionString, Guid id);
        bool Add(string connectionString, T itemToAdd, Guid id);
    }
}

