using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yonder.Models
{
    public class Login
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        QueryExecutor _qe;
        public bool AdminAuth()
        {
            return this.UserName == "admin@yonder.com" && this.Password == "1234";
          
        }

        public bool ShopAuth()
        {
            _qe = new QueryExecutor();

            return Convert.ToBoolean(_qe
                .Aggregate(string
                .Format("select count(*) from shopdetails where (email='{0}' or mobilenumber='{0}') and password='{1}'",
                this.UserName,this.Password)));
        }

        internal bool UserAuth()
        {
            _qe = new QueryExecutor();

            return Convert.ToBoolean(_qe
                .Aggregate(string
                .Format("select count(*) from cardregistration where cardnumber='{0}' and pin='{1}' and "+
                " isactive='true' and isblocked='false'",
                this.UserName, this.Password)));
        }

        public string userType { get; set; }
    }
}