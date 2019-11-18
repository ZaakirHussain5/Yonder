using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yonder.Models
{
    public class Shop
    {
        public int ShopNumber { get; set; }

        [Required(ErrorMessage="*")]
        [Display(Name="Shop Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Incharge Name")]
        public string Owner { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage="Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Display(Name = "Optional Contact Number")]
        public string optional_cont_number { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Permanent Address")]
        [DataType(DataType.MultilineText)]
        public string Permanent_Address { get; set; }

        [Display(Name = "Recuring Address")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "Bank")]
        public string Bank { get; set; }

        [Display(Name = "Branch")]
        public string branch { get; set; }

        [Display(Name = "Account Number")]
        public string Account_Number { get; set; }

        [Display(Name = "IFSC Code")]
        public string IFSC { get; set; }

        [Display(Name = "Cuisine Type")]
        public string ShopType { get; set; }

        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }

        [Display(Name = "GST Number")]
        public string GSTNumber { get; set; }

        [Display(Name = "Commence Date")]
        [DisplayFormat(DataFormatString = "{0:d-MM-yyyy}")]
        public string CommenceDate { get; set; }

        [Display(Name = "Launch Certification Date")]
        [DisplayFormat(DataFormatString = "{0:d-MM-yyyy}")]
        public string launch_cert_date { get; set; }

        [Display(Name = "Policy Expiry Date")]
        [DisplayFormat(DataFormatString = "{0:d-MM-yyyy}")]
        public string Policy_Exp_Date{get;set;}

       
        [Display(Name = "Deposit Interest")]
        public decimal deposit_intrst { get; set; }

        [Display(Name = "is Active ?")]
        public bool isActive { get; set; }

        [Display(Name = "Partner Need Percentage")]
        public decimal partner_need_percent { get; set; }

        public List<ShopType> shopTypes
        {
            get
            {
                return new ShopType().getShopTypes();
            }
        }

        QueryExecutor _q;

        public List<Shop> getShops()
        {
            _q = new QueryExecutor();

            return DataConvertor
                .ConvertDataTable<Shop>(_q
                .NonTransaction("Select * from shopdetails"));
        }

        public Shop getShopById(int id)
        {
            _q = new QueryExecutor();

            List<Shop> result= DataConvertor
                .ConvertDataTable<Shop>(_q
                .NonTransaction("Select * from shopdetails where shopnumber=" + id));

            return result.Count != 0 ? result[0] : null;
        }

        public bool Save()
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format(
                "INSERT INTO shopdetails("+
                "shopnumber,name, owner, email, mobilenumber,optional_cont_number," +
                "address, account_number, bank, ifsc, branch, shoptype,"+
                "category, registrationnumber, gstnumber, commencedate,"+
                "launch_cert_date, policy_exp_date, deposit_intrst,"+
                "isactive,partner_need_percent, permanent_address,password)" +
                "VALUES ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',"+
                "'{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}',"+
                "'{19}','{20}','{21}','{22}')",this.ShopNumber,this.name,this.Owner,this.Email,this.MobileNumber,this.optional_cont_number,
                this.Address,this.Account_Number,this.Bank,this.IFSC,this.branch,this.ShopType,this.category,
                this.RegistrationNumber, this.GSTNumber, this.CommenceDate, this.launch_cert_date,
                this.Policy_Exp_Date,this.deposit_intrst,this.isActive,this.partner_need_percent,this.Permanent_Address,new Random().Next(1000,9999))) > 0;
        }

        public Shop getShopByEmail(string Email)
        {
            _q = new QueryExecutor();

            List<Shop> result = DataConvertor
                .ConvertDataTable<Shop>(_q
                .NonTransaction(string.Format("Select * from shopdetails where email='{0}' or mobilenumber='{0}'", Email)));

            return result.Count != 0 ? result[0] : null;
        }

        public bool UpdateDetails()
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format(
                "Update shopdetails set " +
                "name='{0}', owner='{1}', optional_cont_number='{2}',address='{3}',registrationnumber='{4}',"+
                "gstnumber='{5}',permanent_address='{6}' where shopnumber='{7}'" ,  this.name, this.Owner, this.optional_cont_number,
                this.Address,this.RegistrationNumber, this.GSTNumber,this.Permanent_Address,this.ShopNumber)) > 0;
        }

        [Required(ErrorMessage="*")]
        [Display(Name="Category")]
        public string category { get; set; }

        public List<ShopCategory> shopCategories { get; set; }
    }
}