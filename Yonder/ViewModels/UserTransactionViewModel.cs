using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yonder.Models;

namespace Yonder.ViewModels
{
    public class UserTransactionViewModel
    {
        public CardRegistration User { get; set; }

        public List<UserTransaction> UserTransactions { get; set; }

    }
}