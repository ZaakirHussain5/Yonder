using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yonder.Models
{
    public class Recharge
    {
        public int slno { get; set; }

        public int rechargeId { get; set; }

        [Required(ErrorMessage="*")]
        [Display(Name="Card Type")]
        public string CardType { get; set; }

        [Required(ErrorMessage = "*")]
        public string Description { get; set; }

        [Required(ErrorMessage = "*")]
        public int Amount { get; set; }

        public DateTime RechargeDate { get; set; }

        public string RechargeTime { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Recharged By")]
        public string RechargedBy { get; set; }

        public List<Package> Packages { get; set; }

        [Required(ErrorMessage="*")]
        [Display(Name="Card Number")]
        public string CardNumber { get; set; }

        public decimal paid_amount { get; set; }

        public string mop { get; set; }

        QueryExecutor _q;

        public bool makeRecharge()
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format("INSERT INTO public.recharges(" +
               "cardnumber, amount, rechargedate, cardtype, description, rechargetime, rechargedby)" +
               "VALUES ('{0}', '{1}', '{2}', '{3}','{4}', '{5}', '{6}')",this.CardNumber,this.Amount,this.RechargeDate,
               this.CardType,this.Description,this.RechargeTime,this.RechargedBy)) > 0;
        }
    }
}