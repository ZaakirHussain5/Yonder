using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yonder.Models
{
    public class UserTransaction
    {
        public decimal slno { get; set; }

        public DateTime Trans_Date { get; set; }

        public string trans_Time { get; set; }

        public string Particular { get; set; }

        public decimal credit { get; set; }

        public decimal debit { get; set; }

        public decimal balance { get; set; }

        public string cardNumber { get; set; }

        QueryExecutor _q;

        public List<UserTransaction> getUserTransactions(string CardNumber)
        {
            _q = new QueryExecutor();

            return DataConvertor.ConvertDataTable<UserTransaction>
                (_q.NonTransaction("SELECT * FROM mis_trans_customer_cardtransaction_details "+
                "where cardnumber='" + CardNumber + "' " +
                "ORDER BY trans_date DESC LIMIT 90 "));
        }



        public List<UserTransaction> getUserTransactionsBetweenDates(string CardNumber, string FromDate, string ToDate)
        {
            _q = new QueryExecutor();

            return DataConvertor.ConvertDataTable<UserTransaction>
                (_q.NonTransaction("SELECT * FROM mis_trans_customer_cardtransaction_details " +
                "where cardnumber='" + CardNumber + "' and trans_date between '"+
                DateTime.Parse(FromDate).ToString("yyyy-MM-dd") + "' and  '" + DateTime.Parse(ToDate).ToString("yyyy-MM-dd") + "'" +
                "ORDER BY trans_date"));
        }
    }
}
