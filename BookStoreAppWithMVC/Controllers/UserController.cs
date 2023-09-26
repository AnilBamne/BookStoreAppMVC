using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreAppWithMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserModel model)
        {
            try
            {
                var result=this.userBL.AddUser(model);
                return View(result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                var result = this.userBL.Login(model);
                return RedirectToAction("Register");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
