﻿using SCT.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT.Controllers
{
    public class AuthorizeController : BaseController
    {
        //
        // GET: /Authorize/

        public ActionResult Login()
        {
            string id = Request["userId"];
            string password = Request["userPass"];

            AuthorizeHelper auth = new AuthorizeHelper();
            var result = auth.LoginCheck(id, password);
            
            if (0 == result.Count)
            {
                TempData["msg"] = "사용자 정보가 없습니다.";
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.User = result;
                Session["UserName"] = result[0].Name;
                Session["Role"] = result[0].Role;
                return RedirectToAction("Main", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Main", "Home");
        }

    }
}
