using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;

namespace Yonder.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content
        public ActionResult About()
        {
            WebContent wc = new WebContent();
            return View(wc.getAboutContent());
        }

        [HttpPost]
        public ActionResult About(WebContent wc)
        {
            try
            {
                bool success = false;
                string Message = "";
                if (wc.ImageFile != null)
                {
                    HttpPostedFileBase file = wc.ImageFile;
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
                    wc.Image = FileUploader.UploadFile(Path.GetFileName(file.FileName), data);
                }
                else
                {
                    WebContent cTemp = new  WebContent().getAboutContent();
                    wc.Image = cTemp.Image;
                }
                if (wc.SaveAbout())
                {
                    success = true;
                    Message = "About Contents Updated Successfully";
                }
                else
                {
                    success = false;
                    Message = "About Contents Updation Failed";
                }

                return Json(new { Success = success, Message = Message });

            }
            catch
            {
                return Json(new { Success = false, Message = "Something Unexpected Happened" });
            }
        }

        public ActionResult WhatsNew()
        {
            WebContent wc = new WebContent();
            return View(wc.getWhatsNewContent());
        }

        [HttpPost]
        public ActionResult WhatsNew(WebContent wc)
        {
            try
            {
                bool success = false;
                string Message = "";
                if (wc.ImageFile != null)
                {
                    HttpPostedFileBase file = wc.ImageFile;
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
                    wc.Image = FileUploader.UploadFile(Path.GetFileName(file.FileName), data);
                }
                else
                {
                    WebContent cTemp = new WebContent().getWhatsNewContent();
                    wc.Image = cTemp.Image;
                }
                if (wc.SaveWhatsNew())
                {
                    success = true;
                    Message = "Whats New Contents Updated Successfully";
                }
                else
                {
                    success = false;
                    Message = "Whats New Contents Updation Failed";
                }

                return Json(new { Success = success, Message = Message });

            }
            catch
            {
                return Json(new { Success = false, Message = "Something Unexpected Happened" });
            }
        }
    }
}