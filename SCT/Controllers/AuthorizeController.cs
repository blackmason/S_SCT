using SCT.Models.Helpers;
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

        public ActionResult Login(string id, string password)
        {
            //string id = Request["id"];
            //string password = Request["password"];

            AuthorizeHelper auth = new AuthorizeHelper();
            var result = auth.LoginCheck(id, password);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
