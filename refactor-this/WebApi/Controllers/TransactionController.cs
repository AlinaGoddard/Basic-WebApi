using System.Configuration;
using System.Web.Http;
using DataAccess;
using System;

namespace WebApi.Controllers
{
    public class TransactionController : ApiController
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["AccountsDatabase"].ConnectionString;

        [HttpGet, Route("api/Accounts/{id}/Transactions")]
        public IHttpActionResult GetTransactions(Guid id)
        {
            var repo = new TransactionsRepository();
            var transactionList = repo.GetTransactions(_connectionString, id);
            return Ok(transactionList);
        }

        [HttpPost, Route("api/Accounts/{id}/Transactions")]
        public IHttpActionResult AddTransaction(Guid id, ApiModels.Transaction transaction)
        {
            var repo = new TransactionsRepository();
            if (repo.Add(_connectionString, transaction, id))
                return Ok();

            return BadRequest("Could not update account amount"); ;
        }
    }
}