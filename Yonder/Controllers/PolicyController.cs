using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;
using Yonder.ViewModels;

namespace Yonder.Controllers
{
    public class PolicyController : Controller
    {
        // GET: Policy
        public ActionResult Index()
        {
            Policy p = new Policy();
            return View(p.getPolicyCollection());
        }

        public ActionResult New()
        {
            Policy p = new Policy();
            CompanyMaster c = new CompanyMaster();
            CardType ct = new CardType();

            CompanyPolicyViewModel compPolicyViewModel = new CompanyPolicyViewModel() 
            {
                policy=p,
                CompanyList=c.CompanyCollection,
                CardTypeList=ct.getCardTypes()
            };

            return View(compPolicyViewModel);
        }


        public ActionResult getBranch(string id)
        {
            CompanyProfile cp = new CompanyProfile();
            List<CompanyProfile> branchList = cp.GetCompanyProfiles(id);
            return Json(branchList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CompanyPolicyViewModel cpvm)
        {
            bool Success = false;
            string Message = "";
            try
            {
                int policyid = cpvm.policy.GetLastId(cpvm.policy.company);
                cpvm.policy.IssuedDate = DateTime.Now.Date;
                cpvm.policy.IssuedTime = DateTime.Now.ToShortTimeString();
                cpvm.policy.ValidTill = DateTime.Now.AddMonths(12).Date;
                cpvm.policy.PolicyNumber =
                    "YON-"+ cpvm.policy.company.Substring(0, 3)+"/"+(policyid+1);
                if (cpvm.policy.Save())
                {
                    Success = true;
                    Message = "Policy Added Successfully";
                }
                else
                {
                    Success = false;
                    Message = "Policy Adding Failed";
                }
            }
            catch
            {
                Success = false;
                Message = "Somthing Unexpected Happened";
            }
            return Json(new { Success = Success, Message = Message ,data=cpvm.policy}, JsonRequestBehavior.AllowGet);
        }
    }
}