using FrontEnd.DataAccess;
using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class UserController : Controller
    {
        private UserApiConnection _userApi = new UserApiConnection();
        // GET: User
        public ActionResult Index()
        {
            return View(_userApi.GetAllUsers());
        }

        // GET: User/Details/5
        public ActionResult Details(long id)
        {
            return View(_userApi.GetUserById(id));
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                User newUser = new User()
                {
                    Nome = collection["Nome"],
                    CPF = collection["CPF"],
                    Email = collection["Email"],
                    Telefone = collection["Telefone"],
                    DataNascimento = Convert.ToDateTime(collection["DataNascimento"]),

                    Sexo = (Genero)Enum.Parse(typeof(Genero), collection["Sexo"])
                };


                _userApi.InsertUser(newUser);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(long id)
        {
            return View(_userApi.GetUserById(id));
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                User updateUser = new User()
                {
                    Id = id,
                    Nome = collection["Nome"],
                    CPF = collection["CPF"],
                    Email = collection["Email"],
                    Telefone = collection["Telefone"],
                    DataNascimento = Convert.ToDateTime(collection["DataNascimento"]),

                    Sexo = (Genero)Enum.Parse(typeof(Genero), collection["Sexo"])
                };

                _userApi.UpdateUser(updateUser);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_userApi.GetUserById(id));
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _userApi.DeletUserById(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
