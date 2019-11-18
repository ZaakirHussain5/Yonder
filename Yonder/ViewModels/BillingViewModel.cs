using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Yonder.Models;

namespace Yonder.ViewModels
{
    public class billItem 
    {
        public string itemName { get; set; }
        public int qty { get; set; }
        public int Price { get; set; }
        public decimal total { get; set; }
        public decimal totalWithoutTax { get; set; }
        public decimal gst { get; set; }
    }

    public class BillingViewModel
    {
        [Display(Name="Card Number")]
        [Required(ErrorMessage="*")]
        public string cardNumber { get; set; }

        public List<billItem> billItems { get; set; }

        public bool GenerateBill(ref string invNumber,Shop shop)
        {
            CardRegistration card=new CardRegistration();
            card=card.getCardInfo(this.cardNumber);
            Dictionary<string, decimal> calResults = caluclations(card);
            BillingChild billChild = new BillingChild();
            BillingMain billMain = new BillingMain();
            int billcount=billMain.getBillCount(shop.ShopNumber);
            int todaysBillCount = billMain.getTodaysBillCount(shop.ShopNumber);
            invNumber = "YON" + DateTime.Now.Year + "-" + (billcount + 1);
            billMain.invoiceNumber = invNumber;
            billMain.billedDate = DateTime.Now;
            billMain.time = DateTime.Now.ToShortTimeString();
            billMain.shopNumber = shop.ShopNumber;
            billMain.GST_amt = calResults["taxVal"];
            billMain.totalAmount = calResults["totalWithoutTax"];
            billMain.discount = card.Discount;
            billMain.CGST_amt = calResults["taxVal"] / 2;
            billMain.SGST_amt = calResults["taxVal"] / 2;
            billMain.cardNumber = card.CardNumber;
            billMain.name = card.Name;
            billMain.phoneNumber = card.PhoneNumber;
            billMain.netAmount = calResults["totalAfterDiscount"] + calResults["taxVal"];
            billMain.order_number = todaysBillCount + 1;
            billMain.billedBy = shop.Owner;
            List<bool> results = new List<bool>();
            results.Add(billMain.newBill());
            string financialYear = billMain.getLastFinancialYear(shop.ShopNumber);
            foreach (var bi in this.billItems) 
            {
                billChild.financialYear = financialYear;
                billChild.invoiceNumber = invNumber;
                billChild.BilledDate = DateTime.Now;
                billChild.shopNumber = shop.ShopNumber;
                billChild.itemName = bi.itemName;
                billChild.quantity = bi.qty;
                billChild.price = bi.Price;
                billChild.total = bi.totalWithoutTax;
                billChild.totalWithTax = bi.total;
                billChild.gst = bi.gst;
                billChild.cgst = bi.gst / 2;
                billChild.sgst = bi.gst / 2;
                billChild.gst_amt = bi.totalWithoutTax -bi.total;
                billChild.cgst_amt = (bi.total - bi.totalWithoutTax)/2;
                billChild.sgst_amt = (bi.total - bi.totalWithoutTax) / 2;
                billChild.slno = billItems.IndexOf(bi) + 1;
                results.Add(billChild.addBillItem());
            }
            results.Add(card.UpdateBalanceDebit(calResults["totalAfterDiscount"] + calResults["taxVal"]));
            return !results.Contains(false);
        }

        public Dictionary<string,decimal> caluclations(CardRegistration card) 
        {
            decimal minBal = (decimal.Parse(card.Value) * 10) / 100;
            decimal total = 0;
            decimal totalWithTax = 0;
            foreach (var billItem in this.billItems)
            {
                total += billItem.totalWithoutTax;
                totalWithTax += billItem.total;
            }
            decimal taxVal = totalWithTax - total;
            decimal totalAfterDiscount = total - ((total * card.Discount) / 100);

            Dictionary<string, decimal> dictResult = new Dictionary<string, decimal>();
            dictResult.Add("totalAfterDiscount", totalAfterDiscount);
            dictResult.Add("totalWithoutTax", total);
            dictResult.Add("taxVal", taxVal);
            dictResult.Add("cardBalance", card.Balance);
            dictResult.Add("minBal", minBal);

            return dictResult;
        }
    }
}