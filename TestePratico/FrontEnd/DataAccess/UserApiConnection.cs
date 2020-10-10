using FrontEnd.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Web;

namespace FrontEnd.DataAccess
{
    public class UserApiConnection
    {
        private HttpClient _api;


        //a url da api é salva em um aquivo de configuração que fica na raiz do projeto para facil manutenção
        public UserApiConnection()
        {
            string path = HttpContext.Current.Server.MapPath("~/");
            var UrlApiUser = File.ReadAllText(path + @"\UrlApiUser.txt");

            _api = new HttpClient();
            _api.BaseAddress = new Uri(UrlApiUser);
        }

        public List<User> GetAllUsers()
        {
            List<User> _users;

            var response = _api.GetAsync("users");
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var read = result.Content.ReadAsStringAsync();
                read.Wait();

                try
                {
                    //recebe o retorno em json, entao o converte para a lista de usuários
                    _users = JsonConvert.DeserializeObject<List<User>>(read.Result);
                }
                catch (Exception e)
                {
                    _users = new List<User>();
                }
            }
            else
            {
                _users = new List<User>();
            }

            return _users;
        }

        public User GetUserById(long id)
        {
            User _user;

            var response = _api.GetAsync("users/" + id);

            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                // converte o retorno em um objeto de Categoria
                var read = result.Content.ReadAsStringAsync();
                read.Wait();

                try
                {
                    _user = JsonConvert.DeserializeObject<User>(read.Result);
                }
                catch (Exception e)
                {
                    _user = new User();
                }
            }
            else
            {
                _user = new User();
            }

            return _user;
        }

        public bool UpdateUser(User updateUser)
        {
            // converte o objeto do usuário a ser atualizado para poder mandar para a api
            var json = JsonConvert.SerializeObject(updateUser);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = _api.PutAsync("users", stringContent);
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public bool InsertUser(User newUser)
        {
            // converte para json o objeto do usuario a ser inserido na base de dados
            var json = JsonConvert.SerializeObject(newUser);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = _api.PostAsync("users", stringContent);
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public bool DeletUserById(int id)
        {
            // envia o id do usuário que a ser excluído
            var response = _api.DeleteAsync("users/" + id);

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}