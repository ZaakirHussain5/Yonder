using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;

namespace Yonder.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            if (Session["Shop"] == null)
                return RedirectToAction("Logout", "AdminHome");
            Menu m = new Menu();
            return View(m.GetShopMenu(((Shop)Session["Shop"]).ShopNumber));
        }

        public ActionResult New()
        {
            if (Session["Shop"] == null)
                return RedirectToAction("Logout", "AdminHome");
            Menu m = new Menu();
            Category c = new Category();
            m.categories = c.getCategories(((Shop)Session["Shop"]).ShopNumber);
            return View(m);
        }

        [HttpPost]
        public ActionResult Save(Menu m)
        {
            try
            {
                bool success = false;
                string Message = "";
                if (m.ImageFile != null)
                {
                    HttpPostedFileBase file = m.ImageFile;
                    byte[] data;
                    using (Stream inputStream = file.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        data = memoryStream.ToArray();
                    }
                    m.Image = FileUploader.UploadFile(Path.GetFileName(file.FileName),data);
                }
                else
                {
                    m.Image =  new Menu().GetMenuById(m.MenuId).Image;
                }
                if (m.MenuId == 0)
                {
                    m.ShopNumber = ((Shop)Session["Shop"]).ShopNumber;
                    if (m.AddMenu())
                    {
                        success = true;
                        Message = "Menu Created Successfully";
                    }
                    else
                    {
                        success = false;
                        Message = "Menu Creation Failed";
                    }
                }
                else
                {
                    m.ShopNumber = ((Shop)Session["Shop"]).ShopNumber;
                    if (m.Update(m.MenuId))
                    {
                        success = true;
                        Message = "Company Details Updated Successfully";
                    }
                    else
                    {
                        success = false;
                        Message = "Company Details Updation Failed";
                    }
                }

                return Json(new { Success = success, Message = Message });

            }
            catch
            {
                return Json(new { Success = false, Message = "Something Unexpected Happened" });
            }
        }

        public ActionResult Edit(int id)
        {
            Menu m = new Menu();
            m = m.GetMenuById(id);
            Category c = new Category();
            m.categories = c.getCategories(((Shop)Session["Shop"]).ShopNumber);
            return View("New", m);
        }

        public ActionResult Delete(int id)
        {
            Menu m=new Menu();
            bool success = false;
            try {
                success = m.Delete(id);
            }
            catch
            {
                success = false;
            }
            return Json(new { Success=success});
        }
    }
}