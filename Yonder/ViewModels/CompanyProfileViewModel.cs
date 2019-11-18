using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Yonder.Models;

namespace Yonder.ViewModels
{
    public class CompanyProfileViewModel
    {
        public CompanyMaster Company { get; set; }


        public List<CompanyProfile> Profile { get; set; }
    }
}