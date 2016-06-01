using SCT.Models.Domains;
using SCT.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult Notice(string mode, int? id)
        {
            ViewBag.Title = "고객센터";

            if (mode == "Write")
            {
                return View("Notice/Write");
            }
            else if (mode == "Update" && id != null)
            {
                string path = string.Format("Notice/{0}/{1}", mode, id);
                return View(path);
            }
            else if (mode == "View" && id != null)
            {
                BoardHelper helper = new BoardHelper();
                var result = helper.GetContents(id);
                string path = string.Format("Notice/{0}", mode);
                return View(path, result);
            }
            else if (mode == "Delete" && id != null)
            {
                string bbsId = "COMM_NOTICE";
                string path = string.Format("DeleteNotice/{0}/{1}", bbsId, id);
                return RedirectToAction("DeleteNotice","Customer"); // 요기 수정해야됨
            }
            else
            {
                BoardHelper helper = new BoardHelper();
                var result = helper.GetAllContents();
                return View("Notice/Notice", result);
            }
        }

        [ValidateInput(false)]
        public ActionResult SubmitNotice(string mode, string id, string subject, string contents)
        {
            if (mode == "Write")
            {
                BoardHelper helper = new BoardHelper();
                var result = helper.AddContents(subject, contents);

                if (0 != result)
                {
                    return RedirectToAction("Notice", "Customer");
                }
                else
                {
                    return View("Notice/Notice");
                }
            }
            else
            {
                return View("Notice/Notice");
            }
        }

        public ActionResult DeleteNotice(string bbsId, int id)
        {
            BoardHelper helper = new BoardHelper();
            var result = helper.DeleteContent(bbsId, id);

            if (0 != result)
            {
                return RedirectToAction("/Customer/Notice","Customer");
            }
            else
            {
                return View(Request.UrlReferrer);
            }
        }

    }
}
