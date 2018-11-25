using DataAccess;
using System;
using System.Configuration;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class AccountController : ApiController
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["AccountsDatabase"].ConnectionString;

        [HttpGet, Route("api/Accounts/{id}")]
        public IHttpActionResult GetById(Guid id)
        {
            var repo = new AccountsRepository();
            var account = repo.GetAccount(_connectionString, id);
            return Ok(account);
        }

        [HttpGet, Route("api/Accounts")]
        public IHttpActionResult Get()
        {
            var repo = new AccountsRepository();
            var accountList = repo.GetAccounts(_connectionString);
            return Ok(accountList);
        }

        [HttpPost, Route("api/Accounts")]
        public IHttpActionResult Add(ApiModels.Account account)
        {
            var repo = new AccountsRepository();

            if(repo.Add(_connectionString, account))
                return Ok();

            return BadRequest("Account Data Supplied is invalid.");
        }

        [HttpPut, Route("api/Accounts/{id}")]
        public IHttpActionResult Update(Guid id, ApiModels.Account account)
        {
            var repo = new AccountsRepository();

            if (repo.Update(_connectionString, account))
                return Ok();

            return BadRequest("Update Account Data Supplied is invalid.");
        }

        [HttpDelete, Route("api/Accounts/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            var repo = new AccountsRepository();

            if (repo.Delete(_connectionString, id))
                return Ok();

            return BadRequest("Delete Account Data Supplied is invalid.");
        }
    }
}