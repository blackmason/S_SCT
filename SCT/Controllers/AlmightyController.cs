﻿using SCT.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCT.Models.Domains;

namespace SCT.Controllers
{
    public class AlmightyController : BaseController
    {
        /// 데이터 액션
        /// 전체메뉴 가져오기
        /// MenuHelper.GetAllMenus()
        /// List<Menus>
        public List<Menu> GetAllMenus()
        {
            MenuHelper helper = new MenuHelper();
            var result = helper.GetAllMenus();

            return result;
        }

        /// 데이터 액션
        /// 서브메뉴 가져오기
        /// MenuHelper.GetSubMenus()
        /// List<Menus>
        public JsonResult GetSubMenus()
        {
            MenuHelper helper = new MenuHelper();
            var result = helper.GetSubMenus();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// 데이터 액션
        /// 한개의 메뉴 가져오기
        /// MenuHelper.GetMenu()
        /// Menus
        public JsonResult GetOneMenu(string code)
        {
            MenuHelper helper = new MenuHelper();
            var result = helper.GetMenu(code);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// 데이터 액션
        /// 메뉴관리 화면 - 메뉴 수정
        /// void
        public void UpdateMenu(string code, string name, string parentCode, string url, string role, string enabled)
        {
            MenuHelper helper = new MenuHelper();
            helper.UpdateMenu(code, name, parentCode, url, role, enabled);
            return;
        }

        /// 데이터 액션
        /// 메뉴관리 화면 - 메뉴 추가
        /// void
        public void AddMenu(string code, string name, string parentCode, string url, string role, string enabled)
        {
            MenuHelper helper = new MenuHelper();
            helper.AddMenu(code, name, parentCode, url, role, enabled);
            return;
        }

        /// 데이터 액션
        /// 메뉴 관리 화면 - 메뉴 하나 선택 시
        /// JsonResult
        public JsonResult GetMenu(string code)
        {
            //var result = GetOneMenu(code);
            return GetOneMenu(code);
        }

        /// 페이지 액션
        /// 요약 화면
        /// ActionResult
        public ActionResult CommandCenter()
        {
            return View();
        }

        /// 페이지 액션
        /// 메뉴 관리 화면
        /// ActionResult
        public ActionResult ManageMenu()
        {
            return View(GetAllMenus());
        }

        /// 페이지 액션
        /// 제품 관리 목록 화면
        /// ActionResult
        public ActionResult Products()
        {
            ProductHelper helper = new ProductHelper();
            var result = helper.GetAllProducts();
            return View("Products/Products", result);
        }

        /// 
        /// 
        /// 
        /// 
        public ActionResult Write()
        {
            return View("Products/Write");
        }
    }
}
