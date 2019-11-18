using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;

namespace Yonder.Controllers
{
    public class CorpCardRegistrationController : Controller
    {
        // GET: CorpCardRegistration
        public ActionResult Index(string pn)
        {
            if (string.IsNullOrEmpty(pn))
                return RedirectToAction("Index","Policy");
            CardRegistration card = new CardRegistration();
            Policy p = new Policy();
            p = p.getPolicyByNumber(pn)[0];
            card.PolicyNumber = pn;
            card.NumberOfCardsIssued = card.getCount(card.PolicyNumber);
            card.TotalNumberofcards = p.NumberOfCards;
            return View(card);
        }

        [HttpPost]
        public ActionResult AddCard(CardRegistration Card)
        {
            bool Success=false;
            string Message = "";
            try
            {
                Policy p = new Policy();
                p=p.getPolicyByNumber(Card.PolicyNumber)[0];
                CardType ct = new CardType();
                ct=ct.getCardTypeByName(p.CardType);
                int cardCount = Card.getCount(Card.PolicyNumber);
                if (cardCount != p.NumberOfCards)
                {
                    Card.CardType = p.CardType;
                    Card.Value = ct.value.ToString();
                    Card.isActive = true;
                    Card.PIN = new Random().Next(1000,9999).ToString();
                    if (Card.Register())
                    {
                        Success = true;
                        Message = cardCount + " Card(s) Issued out of " + p.NumberOfCards+" cards";
                    }
                    else
                    {
                        Success = false;
                        Message = "Card Registration failed ";
                    }
                }
                else
                {
                    Success = false;
                    Message = "Card Registration Limit Reached";
                }
            }
            catch
            {
                Success = false;
                Message = "Something unexpected Happened";
            }
            return Json(new {Success=Success,Message=Message });
        }

        public ActionResult GetCards(string policyNumber)
        {
            CardRegistration card=new CardRegistration();
            return Json(
                card.getPolicyCards(policyNumber),JsonRequestBehavior.AllowGet);
        }
    }

   
}