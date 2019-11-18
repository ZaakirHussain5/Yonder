using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yonder.Models;

namespace Yonder.ViewModels
{
    public class WalkinRegistrationViewModel
    {
        public CardRegistration Card { get; set; }
        public CardType CardTypes { get; set; }
    }
}