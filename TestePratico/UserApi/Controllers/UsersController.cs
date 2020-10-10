using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserApi.DataContext;
using UserApi.Model;

namespace UserApi.Controllers
{
    [Route("/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserDataConnection _userDb = new UserDataConnection();

        //realiza o gerenciamento das requisições http, direcionando para o metodo equivalente do banco
        //realiza também as converções para json dos retornos que necessitam

        // GET api/users
        [HttpGet]
        public ActionResult<string> Get()
        {
            var ret = JsonConvert.SerializeObject(_userDb.GetAllUser());
            return ret;
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(long id)
        {
            var ret = JsonConvert.SerializeObject(_userDb.GetUserById(id));
            return ret;
        }
        
        // POST api/users
        [HttpPost]
        public bool Post([FromBody] User newUser)
        {
            _userDb.InsertUser(newUser);
            return true;
        }

        // PUT api/users
        [HttpPut]
        public bool Put([FromBody] User updateUser)
        {
            _userDb.UpdateUser(updateUser);
            return true;
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            _userDb.DeletUserById(id);
            return true;
        }
    }
}
