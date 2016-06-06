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
        /*
         * 고객지원/공지사항
         */
        public ActionResult Notice(string mode, string id)
        {
            ViewBag.Title = "공지사항";
            string url;
            BoardHelper helper;

            if (("View" == mode && null != id) || ("Edit" == mode && null != id))
            {
                helper = new BoardHelper();
                var result = helper.GetContents(id);
                url = string.Format("Notice/{0}", mode);
                return View(url, result);
            }
            else if ("Write" == mode)
            {
                url = string.Format("Notice/{0}", mode);
                return View(url);
            }
            else
            {
                helper = new BoardHelper();
                //TempData["msg"] = "올바른 요청이 아닙니다.";
                var result = helper.GetAllContents();
                return View("Notice/Notice", result);
            }
        }

        /*
         * 글 등록
         */
        [ValidateInput(false)]
        public ActionResult OnSubmit(string mode, string id, string subject, string contents)       //공지사항에 구속되지 않게 구현 보드아이디?
        {
            string bbsId = Request["bbsId"];
            BoardHelper helper;

            if ("Write" == mode)
            {
                helper = new BoardHelper();
                var result = helper.AddContents(subject, contents);

                if (0 != result)
                {
                    return RedirectToAction(bbsId, "Customer");
                }
                else
                {
                    return View("Notice/Notice");       //글 등록 실패 시 액션 구현해야 함. 지금은 공지사항 메인으로 그냥 감.
                }
            }
            else if ("Edit" == mode)      // 수정모드 업데이트 해야됨.
            {
                helper = new BoardHelper();
                int result = helper.EditContents(bbsId, id, subject, contents);

                if (0 <= result)
                {
                    string url = string.Format("{0}/View/{1}", bbsId, id);
                    return RedirectToAction(url, "Customer");
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

        /*
         *글 삭제
         */
        public ActionResult DeleteContents(string mode, string id)
        {
            BoardHelper helper = new BoardHelper();
            var result = helper.DeleteContents(mode, id);
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
