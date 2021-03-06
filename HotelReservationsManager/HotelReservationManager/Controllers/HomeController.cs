﻿using HotelReservationsManager.Models;
using System.Web.Mvc;

namespace HotelReservationsManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PermissionDenied()
        {
            return View();
        }

        public ActionResult ClientIndex()
        {
            return View();
        }
        public ActionResult AdminIndex()
        {
            return View();
        }

        public ActionResult RoomIndex()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View(new LoginVM());
        }

        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            if (this.ModelState.IsValid)
            {
                 AuthenticationManager.Authenticate(model.Username, model.Password);
                
                 if (AuthenticationManager.LoggedUser == null)
                ModelState.AddModelError("authenticationFailed", "Wrong username or password!");
            }

            if (!this.ModelState.IsValid)
            {
                return View(model);
            }
            if (AuthenticationManager.LoggedUser.UserName == "admin")
            {
                return RedirectToAction("AdminIndex", "Users");
            }
            else if(AuthenticationManager.LoggedUser.IsActiveAccount == false)
                return RedirectToAction("PermissionDenied", "Home");
            else return RedirectToAction("RoomIndex", "Rooms");
        }
       
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Logout()
        {
            AuthenticationManager.Logout();

            return RedirectToAction("Index", "Home");
        }

    }
}