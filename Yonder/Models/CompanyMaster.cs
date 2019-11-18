using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Yonder.Models
{
    public class CompanyMaster
    {
        public decimal id { get; set; }
        
        [Display(Name="Company")]
        [Required(ErrorMessage="*")]
        public string company { get; set; }

        [Display(Name = "Head Office Address")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.MultilineText)]
        public string headOffice { get; set; }

        [Display(Name = "Website")]
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$",ErrorMessage="Invalid Website Address")]
        public string Website { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "*")]
        public string State { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "*")]
        public string City { get; set; }

        [Display(Name = "Pin Code")]
        [Required(ErrorMessage = "*")]
        public string PinCode { get; set; }

        [Display(Name = "Upload File for Logo")]
        [Required(ErrorMessage = "*")]
        public string Logo{ get; set; }

        public bool isProfileUpdating { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        public List<CompanyMaster> CompanyCollection 
        {
            get 
            {
                _q=new QueryExecutor();
                DataTable tab = _q.NonTransaction("Select * from companymaster");
                return DataConvertor.ConvertDataTable<CompanyMaster>(tab);
            }
        }

        QueryExecutor _q = null;

        public List<CompanyMaster> GetComapnyById(decimal id)
        {
            _q = new QueryExecutor();
            DataTable tab = _q.NonTransaction("Select * from companymaster where id="+id);
            return DataConvertor.ConvertDataTable<CompanyMaster>(tab);
        }

        public List<CompanyMaster> GetComapnyByName(string name)
        {
            _q = new QueryExecutor();
            DataTable tab = _q.NonTransaction(string.Format("Select * from companymaster where company='{0}'",name));
            return DataConvertor.ConvertDataTable<CompanyMaster>(tab);
        }

        public bool Save()
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format
                ("insert into companymaster (company,headoffice,state,city,logo,pincode,website)"+
                "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')"
                ,this.company.Trim().ToUpper(),this.headOffice,this.State,this.City,this.Logo,this.PinCode,this.Website))>0;
        }

        public bool Update(decimal id)
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format
                ("Update companymaster set company='{0}', headoffice='{1}' "+
                ", state='{2}' , city='{3}' , logo='{4}' , pincode='{5}' , website='{6}' " +
                "where id={7}"
                , this.company.Trim().ToUpper(), this.headOffice, this.State, this.City, this.Logo, this.PinCode, this.Website, id)) > 0;
        }


    }
}