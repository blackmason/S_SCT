using SCT.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace SCT.Controllers
{
    public class ProductController : BaseController
    {
        //
        // GET: /Product/

        public ActionResult Write()
        {
            return View("/Almighty/Products/Write");
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

        public void UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                if (file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    file.SaveAs(path);
                }
            }
        }

    }
}
