using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Yonder.Models
{
    public class CompanyProfile
    {
        public decimal id { get; set; }

        [Display(Name="Select Company")]
        [Required(ErrorMessage="*")]
        public string company { get; set; }

        public CompanyMaster companyMaster{get;set;}

        [Display(Name="Branch Name")]
        [Required(ErrorMessage="*")]
        public string BranchName { get; set; }

        [Required(ErrorMessage="*")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage="*")]
        public string State { get; set; }

        [Required(ErrorMessage="*")]
        public string City { get; set; }

        [Display(Name="Pin Code")]
        [Required(ErrorMessage="*")]
        [Range(0,999999999,ErrorMessage="Invalid Pincode")]
        public string pincode { get; set; }

        [Display(Name="Geo Location")]
        [Required(ErrorMessage="*")]
        public string GeoLocation { get; set; }

        [Display(Name="Contact Person")]
        [Required(ErrorMessage="*")]
        public string ContactPerson { get; set; }

        [Required(ErrorMessage="*")]
        public string Designation { get; set; }

        [Required(ErrorMessage="*")]
        [EmailAddress(ErrorMessage="Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name="Mobile Number")]
        [Required(ErrorMessage="*")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Invalid Mobile Number")]
        public string MobileNumber { get; set; }

        [Display(Name="Optional Contact Number")]
        public string OptionalNumber { get; set; }

        QueryExecutor _q;

        public List<CompanyProfile> GetCompanyProfiles(string cmpny)
        {
            _q = new QueryExecutor();
            DataTable tabProfile = _q.NonTransaction(string.Format(
                "Select * from companyprofile where company='{0}'", cmpny));
            return DataConvertor.ConvertDataTable<CompanyProfile>(tabProfile);
        }

        public bool Save()
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format(
                "insert into companyprofile (company,branchname,address,state,city,"+
                "geolocation,contactperson,email,mobilenumber,optionalnumber,pincode,designation)"+
                "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')",
                this.company,this.BranchName,this.Address,this.State,this.City,this.GeoLocation,
                this.ContactPerson,this.Email,this.MobileNumber,this.OptionalNumber,this.pincode,this.Designation)) > 0;
        }


        public bool Update(decimal id)
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format
                ("Update companyprofile set branchname="))>0;
        }
    }
}