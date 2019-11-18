using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yonder.Models
{
    public class Refund
    {
        public int slNo { get; set; }

        public int refundId { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name="Card Number")]
        public string cardNumber { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Card Type")]
        public string cardType { get; set; }

        public DateTime refundDate { get; set; }
        public string refundTime { get; set; }

        [Required(ErrorMessage = "*")]
        public string Reason { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Account Number")]
        public string acc_number { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Accounant Name")]
        public string acc_name { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "IFSC Code")]
        public string ifsc { get; set; }
        
        [Required(ErrorMessage = "*")]
        public string Branch { get; set; }

        [Required(ErrorMessage = "*")]
        public string Bank { get; set; }

        public string status { get; set; }

        QueryExecutor _q;

        public bool AddRefundRequest()
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format(
                "insert into refunds (refunddate,bank,acc_number,acc_name,ifsc,branch,reason,cardtype,cardnumber)"+
                "values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                this.refundDate,this.Bank,this.acc_number,this.acc_name,this.ifsc,this.Branch,this.Reason,cardType,this.cardNumber)) > 0;
        }

    }
}