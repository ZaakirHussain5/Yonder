using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;

namespace Yonder.Controllers
{
    public class CardTypeController : Controller
    {
        // GET: CardType
        public ActionResult Index()
        {
            List<CardType> CardTypeCollection = new CardType().getCardTypes();
            return View(CardTypeCollection);
        }

        // GET: CardType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CardType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CardType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CardType cardType)
        {
            try
            {
                if (cardType.CardTypeId == 0)
                {
                    if (cardType.Save())
                    {
                        return Json(new { Success = true, Message = "Card Type Created Successfully" });
                    }
                    else
                    {
                        return Json(new { Success = false, Message = "Card Type Already Exists" });
                    }
                }
                else
                {
                    if (cardType.updateCardType(cardType.CardTypeId))
                    {
                        return Json(new { Success = true, Message = "Card Type Updated Successfully" });
                    }
                    else
                    {
                        return Json(new { Success = false, Message = "Card Type Already Exists" });
                    }
                }
            }
            catch
            {
                return Json(new { Success = false, Message = "Card Type Operation Failed" });
            }
        }

        // GET: CardType/Edit/5
        public ActionResult Edit(int id)
        {
            CardType card = new CardType();
            card = card.getCardTypeById(id);
            return View("Create",card);
        }

        // POST: CardType/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                CardType card = new CardType();
                if (card.deleteCardType(id))
                {
                    return Json(new {Success = true,Message="Card Type Deleted Succesfully"});
                }
                else
                {
                    return Json(new { Success = false, Message = "Card Type Deletion failed" });
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
