using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;
using Yonder.ViewModels;

namespace Yonder.Controllers
{
    public class WalkinCustomersController : Controller
    {
        // GET: WalkinCustomers
        [Route("Walkin/New")]
        public ActionResult Index()
        {
            CardType ct = new CardType();
            CardRegistration c = new CardRegistration();
            c.CardTypes = ct.getCardTypes();
            return View(c);
        }

        [HttpPost]
        public ActionResult Register(CardRegistration Card)
        {
            bool Success=false;
            CardType ct = new CardType();
            ct = ct.getCardTypeByName(Card.CardType);
            Card.Value = ct.value.ToString() ;
            Card.PIN = new Random().Next(1000, 9999).ToString();
            Card.userType = "Walkin";
            Card.regDate = DateTime.Now.ToShortDateString();
            Card.Validity = DateTime.Now.AddYears(1).ToShortDateString();
            if (Card.RegisterWalkin())
            {
                Success = true;
            }
            else
            {
                Success = false;
            }
            return Json(new { Success=Success,data=Card},JsonRequestBehavior.AllowGet);
        }

       
        public ActionResult getOTP(string id)
        {
            string OTP = new Random().Next(1000, 9999).ToString();
            //string Message = "http://203.212.70.200/smpp/sendsms?username=spctrahttp&password=spctrahttp1&to=" + id + "&from=SPCTRA&udh=&text=Your Yonder OTP is " + OTP + "&dlr-mask=19";
            //WebRequest sendSms = WebRequest.Create(Message);
            //WebResponse response = sendSms.GetResponse();
            string responseFromServer = "Sent";
            //using (Stream dataStream = response.GetResponseStream())
            //{
            //    StreamReader reader = new StreamReader(dataStream);
            //    responseFromServer = reader.ReadToEnd();
            //}
            HttpCookie otpCookie = new HttpCookie("OTP");
            otpCookie.Value = OTP;
            otpCookie.Name = "OTP";
            otpCookie.Expires = DateTime.Now.AddMinutes(2);
            Response.SetCookie(otpCookie);
            if(responseFromServer.ToLower().IndexOf("sent")!=-1)
                return Json(new { Sent = true }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { Sent = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getCardInfo(string id)
        {
            CardType ct = new CardType();
            ct=ct.getCardTypeByName(id);
            return Json(new List<CardType>() { ct }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActivateAccount(string cardNumber)
        {
            CardRegistration card = new CardRegistration();
            return Json(new { success = card.acivateAccount(cardNumber) });
        }
    }
}