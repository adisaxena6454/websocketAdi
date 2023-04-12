using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSocketWebApp.Models;
using WebSocketWebApp.Helper;

namespace WebSocketWebApp.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Login()
        {

            return View();
        }
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginReqModel objUser)
        {
            AuthJwtHelper authJwtHelper = new AuthJwtHelper();
            string userName = Convert.ToString(ConfigurationManager.AppSettings["UserName"]);
            string passWord = Convert.ToString(ConfigurationManager.AppSettings["Password"]);
            string token = string.Empty;
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(passWord))
                {
                    if (userName.Equals(objUser.Username) && passWord.Equals(objUser.Password))
                    {
                        Session["UserName"] = userName.ToString();
                        token = authJwtHelper.GetToken(userName);
                        Session["token"] = token.ToString();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "The user name or password is incorrect");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The user name or password is incorrect");

                }


            }
            return View(objUser);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(string Token)
        {
            if (Session.Contents["UserName"] == null)
                return View("Login");
            WebSocketHelper webSocketHelper = new WebSocketHelper();
            string rsp = webSocketHelper.getSocketDataAsync().ToString();
            return View((object)rsp);
        }
        public ActionResult About()
        {
            AuthJwtHelper authJwtHelper = new AuthJwtHelper();
            string validationStatus = string.Empty;
            string userName = string.Empty;
            var token = Session["token"].ToString();
            string loginUserName = Session["UserName"].ToString();
            if (!string.IsNullOrWhiteSpace(token.ToString()))
                validationStatus = authJwtHelper.ValidateToken(token.ToString() , loginUserName);
            if (!string.IsNullOrWhiteSpace(validationStatus))
            {
                if (validationStatus == "true")
                {
                    return View();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The user name or password is incorrect");

                    return View("Index");
                   
                }
            }
            else
            {
                return View("Index");
                ModelState.AddModelError(string.Empty, "The user name or password is incorrect");


            }

        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}