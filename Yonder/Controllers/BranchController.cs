using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;

namespace Yonder.Controllers
{
    public class BranchController : Controller
    {
        // GET: Branch
        public ActionResult New(int CompanyId)
        {
            CompanyMaster comp = new CompanyMaster();
            comp = comp.GetComapnyById(CompanyId)[0];
            CompanyProfile compProfile = new CompanyProfile() 
            {
                companyMaster=comp,company=comp.company
            };

            return View("BranchForm",compProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CompanyProfile compProfile)
        {
            try
            {
                bool success = false;
                string message = "";
                if(compProfile.id==0)
                {
                    if (compProfile.Save())
                    {
                        success = true;
                        message = "Branch Added Successfully";
                    }
                    else
                    {
                        success = false;
                        message = "Branch Adding Failed";
                    }
                }
                else
                {
                    if (compProfile.Update(compProfile.id))
                    {
                        success = true;
                        message = "Branch Updated Successfully";
                    }
                    else
                    {
                        success = false;
                        message = "Branch Updation Failed";
                    }
                }
                return Json(new { Success = success, Message = message });
            }
            catch
            {
                return Json(new {Success=false,Message="Something Unexpected Happened"});
            }
        }

    }
}