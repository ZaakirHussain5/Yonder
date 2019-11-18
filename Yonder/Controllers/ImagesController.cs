using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;

namespace Yonder.Controllers
{
    public class ImagesController : Controller
    {
        // GET: Images
        public ActionResult Categories()
        {
            Category c = new Category();
            c.categoriesList = c.getDistinctCategories();
            return View(c);
        }

        [HttpPost]
        public ActionResult Categories(Category c)
        {
            bool success = false;
            string Message = "";
            try
            {
                HttpPostedFileBase file = c.ImageFile;
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
                c.Image = FileUploader.UploadFile(Path.GetFileName(file.FileName), data);
            }
            catch
            {
                success = false;
                Message = "Image Uploading Failed";
                goto end;
            }
            if (c.UpdateImage())
            {
                success = true;
                Message = "Image Updated Successfully";
            }
            else
            {
                success = false;
                Message = "Image Updating Failed";
            }
        end:
            return Json(new { success = success, message = Message });
        }

        public ActionResult Offers()
        {
            Offer o = new Offer();
            o.offerTypes = new ShopCategory().getCategories();
            return View(o);
        }

        public ActionResult SliderImages()
        {
            SliderImage img = new SliderImage();
            return View(img);
        }

        [HttpPost]
        public ActionResult SliderImages(SliderImage Img)
        {
            bool success = false;
            string Message = "";
            try
            {
                HttpPostedFileBase file = Img.ImageFile;
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
                Img.Image = FileUploader.UploadFile(Path.GetFileName(file.FileName), data);
            }
            catch
            {
                success = false;
                Message = "Image Uploading Failed";
                goto end;
            }
            if (Img.AddImage())
            {
                success = true;
                Message = "Image Added Successfully";
            }
            else
            {
                success = false;
                Message = "Image Updating Failed";
            }
        end:
            return Json(new { success = success, message = Message });
        }

        public ActionResult getSliderImages()
        {
            SliderImage si = new SliderImage();
            return Json(si.getImages(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveSliderImage(int id)
        {
            bool Success = false;
            SliderImage c = new SliderImage();
            if (c.RemoveImage(id))
            {
                Success = true;
            }
            else
            {
                Success = false;
            }
            return Json(new { Success = Success });
        }

        [HttpPost]
        public ActionResult Offers(Offer o)
        {
            bool success = false;
            string Message = "";
            try
            {
                HttpPostedFileBase file = o.ImageFile;
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
                o.Image = FileUploader.UploadFile(Path.GetFileName(file.FileName), data);
            }
            catch
            {
                success = false;
                Message = "Image Uploading Failed";
                goto end;
            }
            if (o.AddOffer())
            {
                success = true;
                Message = "Offer added Successfully";
            }
            else
            {
                success = false;
                Message = "Offer Adding Failed";
            }
        end:
            return Json(new { success = success, message = Message });
        }
    }
}