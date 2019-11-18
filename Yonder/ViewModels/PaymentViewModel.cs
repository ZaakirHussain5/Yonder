using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yonder.ViewModels
{
    public class PaymentViewModel
    {
        public decimal payment_id { get; set; }

        public DateTime Payment_of_Date { get; set; }

        public decimal Amount { get; set; }

        public decimal GST { get; set; }

        public decimal CGST { get; set; }

        public decimal SGST { get; set; }

    }
}