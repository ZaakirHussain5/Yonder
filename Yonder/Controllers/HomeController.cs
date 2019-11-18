using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;
using Yonder.ViewModels;

namespace Yonder.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomePageViewModel Img = new HomePageViewModel()
            {
                SliderImages=new SliderImage().getImages(),
                WhatsNewContent=new WebContent().getWhatsNewContent(),
                Testimonials=new Testimonials().getTesimonials()
            };
            return View(Img);
        }

        public ActionResult Dine()
        {
            Category c = new Category();
           
            return View(c.getDistinctCategories());
        }

        public ActionResult FoodMenu(string category)
        {
            Menu m = new Menu();
            Category c = new Category();
            m.categories = c.getDistinctCategories();
            m.menuList = m.GetCategoryMenu(category);
            if (m.menuList.Count != 0){
                return View(m);
            }
            else
            {
                TempData["Message"] = "No Menu Found";
                return View("Dine",m.categories);
            } 
                
        }

        public ActionResult EntertainmentSingle()
        {
            return View();
        }

        public ActionResult About()
        {
            return View(new WebContent().getAboutContent());
        }

        public ActionResult EnterTainment()
        {
            return View();
        }

        public ActionResult Wellness()
        {
            return View();
        }

        public ActionResult MeetingRooms()
        {
            return View();
        }


        public ActionResult Offers()
        {
            Offer o = new Offer();
            return View(o.getOffers());
        }

        [Route("yon-admin")]
        public ActionResult Admin()
        {
            ViewBag.user = "Admin";
            Login l = new Login() 
            {
                userType = "Admin"
            }; 
            return View(l);
        }

        [Route("yon-vendor")]
        public ActionResult Vendor()
        {
            ViewBag.user = "Vendor";
            Login l = new Login()
            {
                userType = "Vendor"
            }; 
            return View("Admin",l);
        }

        [Route("yon-store")]
        public ActionResult Store()
        {
            ViewBag.user = "Convinient Store";
            Login l = new Login()
            {
                userType = "Store"
            };
            return View("Admin", l);
        }

        [HttpPost]
        public ActionResult Login(Login User)
        {

            if (User.userType == "Admin")
            {
                if (User.AdminAuth())
                {
                    Session["Username"] = User.UserName;
                    return RedirectToAction("Index", "AdminHome");
                }
                else
                {
                    TempData["Message"] = "Login Failed";
                    return RedirectToAction("Admin", "Home");
                }
            }
            else
            {
                if (User.ShopAuth())
                {
                    Shop s = new Shop();
                    s = s.getShopByEmail(User.UserName);

                    if (User.userType == "Vendor")
                    {
                        Session["Shop"] = s;
                        return RedirectToAction("Index", "ShopHome");
                    }
                    else
                    {
                        Session["Store"] = s;
                        return RedirectToAction("Index", "StoreHome");
                    }

                }
                else
                {
                    TempData["Message"] = "Login Failed";
                    return Redirect(User.userType == "Vendor" ? "/yon-vendor" : "/yon-store");
                }
            }
        }

        [HttpPost]
        public ActionResult CustomerLogin(Login User)
        {
            bool res = User.UserAuth();
            if (res) Session["User"] = User.UserName;
            return Json(new { isValid = res }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult LoginPartial()
        {
            return PartialView("_LoginPartial");
        }

        public ActionResult GreetUser()
        {
            CardRegistration card = new CardRegistration();
            card = card.getCardInfo(Session["User"].ToString());
            return Content("Welcome "+card.Name.Split(' ')[0]);
        }

    }
}