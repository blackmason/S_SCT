using SCT.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT.Controllers
{
    public class AlmightyController : Controller
    {
        //
        // GET: /Almighty/

        public ActionResult CommandCenter()
        {
            return View();
        }

        public ActionResult ManageMenu()
        {
            MenuHelper helper = new MenuHelper();
            var result = helper.GetAllMenus();
            return View(result);
        }

        public JsonResult GetMenus(string code)
        {
            MenuHelper helper = new MenuHelper();
            var result = helper.GetMenus(code);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
