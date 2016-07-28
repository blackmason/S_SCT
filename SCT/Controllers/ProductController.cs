using SCT.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT.Controllers
{
    public class ProductController : BaseController
    {
        //
        // GET: /Product/

        public ActionResult Default()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult OnSubmit(string productGb, string modelName, string productName, string contents)
        {
            productGb = Request["productGb"];
            modelName = Request["modelName"];
            productName = Request["productName"];
            contents = Request["contents"];

            ProductHelper helper = new ProductHelper();
            int result = helper.AddProduct(productGb, modelName, productName, contents);

            return RedirectToAction("Products", "Almighty");

        }

    }
}
