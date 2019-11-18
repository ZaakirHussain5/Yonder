using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yonder.Models
{
    public class VendorPayments
    {
        public decimal slno { get; set; }

        public decimal payment_id { get; set; }

        [Required(ErrorMessage="*")]
        [Display(Name="Paid By")]
        public string paid_from { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Paid To")]
        public string paid_to { get; set; }

        public DateTime paid_date { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Bank Transaction ID")]
        public string transaction_Id { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Paid Amount")]
        public decimal Amount { get; set; }

        public List<Shop> shopsList { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Shop")]
        public decimal shopNumber { get; set; }

        QueryExecutor _q;

        public bool UpdatePayment()
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format(
                "Update vendor_payments set paid_from='{0}'," +
                "paid_to='{1}',paid_date='{2}',transaction_id='{3}',"+
                "status='Paid' where payment_of_date='{4}'",this.paid_from,this.paid_to,this.paid_date,
                this.transaction_Id,this.payment_of_Date.ToString("yyyy-MM-dd"))) > 0;
        }

        public List<VendorPayments> getVendorPayments_unpaid(int ShopNumber)
        {
            _q = new QueryExecutor();

            return DataConvertor.ConvertDataTable<VendorPayments>(
                _q.NonTransaction("select * from vendor_payments where status='Unpaid' and shopnumber="+ShopNumber));
        }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime payment_of_Date { get; set; }

        public decimal partner_need_percent { get; set; }

        public decimal gst { get; set; }

        public decimal total_Collection { get; set; }
    }
}