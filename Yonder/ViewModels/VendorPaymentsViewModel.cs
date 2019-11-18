using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yonder.Models;

namespace Yonder.ViewModels
{
    public class VendorPaymentsViewModel
    {
        public decimal shopnumber { get; set; }

        public string name { get; set; }

        public string Owner { get; set; }

        public decimal Collection { get; set; }

        public decimal gst { get; set; }

        public decimal partner_need_percent { get; set; }

        public decimal Amount {
            get
            {
                decimal amount = Collection - gst;
                return amount - ((amount * partner_need_percent) / 100);
            }
        }

        public List<VendorPaymentsViewModel> getVendorPayments()
        {
            QueryExecutor _q = new QueryExecutor();

            return DataConvertor.ConvertDataTable<VendorPaymentsViewModel>
                (_q.NonTransaction("select * from view_vendor_payments"));
        }

    }
}