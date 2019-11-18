using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yonder.Models;

namespace Yonder.ViewModels
{
    public class CompanyPolicyViewModel
    {
        public Policy policy { get; set; }

        public List<CompanyMaster> CompanyList { get; set; }

        public CompanyMaster SelectedCompany { get; set; }

        public List<CompanyProfile> BranchList { get; set; }

        public CompanyProfile SelecttedBranch { get; set; }

        public List<CardType> CardTypeList { get; set; }
    }
}