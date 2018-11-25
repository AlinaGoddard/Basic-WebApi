using System.Data.Entity;

namespace DataAccess
{
    public partial class AccountsDatabase : DbContext
    {
        public AccountsDatabase(string connectionString)
            : base(connectionString)
        {
        }

    }
}
