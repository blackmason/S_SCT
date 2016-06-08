using SCT.Models.Domains;
using SCT.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT.Controllers
{
    public class HomeController : BaseController
    {
        /* 메인화면 */
        public ActionResult Main()
        {
            BoardHelper helper = new BoardHelper();
            SummaryData summary = new SummaryData();
            
            summary.BbsList = helper.SummaryData("Notice");
            SetMenus();

            return View(summary);
        }

        public ActionResult Login()
        {
            return View();
        }

    }
}
