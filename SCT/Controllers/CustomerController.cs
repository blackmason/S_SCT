using SCT.Models.Domains;
using SCT.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT.Controllers
{
    public class CustomerController : BaseController
    {
        private BoardHelper helper;
        private string url;
        //세션처리 해야함
        
        /*
         * 고객지원/공지사항
         */
        public ActionResult Notice(string mode, string id)
        {
            ViewBag.Title = "고객지원";

            if ("Write" == mode)
            {
                return View("Notice/Write");
            }
            else if ("View" == mode)
            {
                helper = new BoardHelper();
                var result = helper.GetContents(id);

                if (null == result.No)
                {
                    url = Request.UrlReferrer.ToString();
                    TempData["msg"] = "없는 게시물 입니다.";
                    return Redirect(url);
                }
                else
                {
                    url = string.Format("Notice/{0}", mode);
                    return View(url, result);
                }
                
            }
            else if ("Edit" == mode)
            {
                helper = new BoardHelper();
                var result = helper.GetContents(id);
                url = string.Format("Notice/{0}", mode);
                return View(url, result);
            }
            else if ("Delete" == mode)
            {
                return RedirectToAction("OnSubmit", "Customer", new { mode = mode, id = id, bbsId = "Notice" });
            }
            else
            {
                helper = new BoardHelper();
                var result = helper.GetAllContents();
                return View("Notice/Notice", result);
            }
        }

        /*
         * 게시물에 전달되는 액션
         * mode: 글등록, 글수정, 글삭제
         * id: 글번호
         */
        [ValidateInput(false)]
        public ActionResult OnSubmit(string mode, string id, string subject, string contents, string author)       //공지사항에 구속되지 않게 구현 보드아이디?
        {
            //세션처리 해야함
            string bbsId = Request["bbsId"];
            BoardHelper helper;

            if ("Write" == mode)
            {
                helper = new BoardHelper();
                int result = helper.AddContents(subject, contents, author);

                if (0 != result)
                {
                    return RedirectToAction(bbsId, "Customer");
                }
                else
                {
                    return RedirectToAction(bbsId,"Customer");       //글 등록 실패 시 액션 구현해야 함. 지금은 공지사항 메인으로 그냥 감.
                }
            }
            else if ("Edit" == mode)      // 수정모드 업데이트 해야됨.
            {
                helper = new BoardHelper();
                
                int result = helper.EditContents(bbsId, id, subject, contents);

                if (0 != result)
                {
                    url = string.Format("{0}/View/{1}", bbsId, id);
                    return RedirectToAction(url, "Customer");
                }
                else
                {
                    url = string.Format("{0}/View/{1}", bbsId, id);
                    return RedirectToAction(url,"Customer");
                }
            }
            else if ("Delete" == mode)
            {
                helper = new BoardHelper();
                int result = helper.DeleteContents(bbsId, id);

                if (0 != result)
                {
                    return RedirectToAction(bbsId, "Customer");
                }
                else
                {
                    url = string.Format("{0}/View/{1}", bbsId, id);
                    return RedirectToAction(url,"Customer");
                }
            }
            else
            {
                return RedirectToAction(bbsId,"Customer");
            }
        }
    }
}
