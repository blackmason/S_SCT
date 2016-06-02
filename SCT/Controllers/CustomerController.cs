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
        public ActionResult Notice(string mode, int? id)  //게시판 형태의 구속되지 않고, 메서드를 사용하려고 함. 수정요함 CRUD
        {
            ViewBag.Title = "고객센터";

            if (mode == "Write")
            {
                return View("Notice/Write");
            }
            else if (mode == "Edit" && id != null)
            {
                string bbsId = "COMM_NOTICE";
                BoardHelper helper = new BoardHelper();
                var result = helper.EditContent(bbsId, id);

                string path = string.Format("Notice/{0}", mode);
                return View(path, result);
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
                return RedirectToAction(path,"Customer"); // 요기 수정해야됨
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
            BoardHelper helper;
            if (mode == "Write")
            {
                helper = new BoardHelper();
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
            //else if (mode == "Edit")      // 수정모드 업데이트 해야됨.
            //{
            //    helper.
            //}
            else
            {
                return View("Notice/Notice");
            }
        }

        public ActionResult DeleteNotice(string mode, int? id)
        {
            BoardHelper helper = new BoardHelper();
            var result = helper.DeleteContent(mode, id);
            string referrerAddr = Request.UrlReferrer.ToString(); //url을 숨겨야 할거 같음. 오류메시지 노출

            if (0 != result)
            {
                return RedirectToAction("Notice","Customer");
            }
            else
            {
                return Redirect(referrerAddr);
            }
        }

    }
}
