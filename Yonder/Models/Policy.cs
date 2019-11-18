using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Yonder.Models
{
    public class Policy
    {
        public decimal policyid { get; set; }

        [Display(Name="Company")]
        [Required(ErrorMessage="*")]
        public string company { get; set; }

        [Display(Name = "Branch")]
        [Required(ErrorMessage = "*")]
        public string BranchName { get; set; }

        [Display(Name="CardType")]
        [Required(ErrorMessage="*")]
        public string CardType { get; set; }

        [Display(Name = "Policy Number")]
        public string PolicyNumber { get; set; }

        [Display(Name = "Number Of Cards")]
        [Required(ErrorMessage = "*")]
        public int NumberOfCards { get; set; }

        [Display(Name = "Availed Discount in %")]
        [Required(ErrorMessage = "*")]
        public int Discount { get; set; }

        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "*")]
        public decimal PayableAmount { get; set; }

        [Display(Name = "Issued Date")]
        [DisplayFormat(DataFormatString = "{0:d-MM-yyyy}")]
        [Required(ErrorMessage = "*")]
        public DateTime IssuedDate { get; set; }

        [Display(Name = "Issued Time")]
        [Required(ErrorMessage = "*")]
        public string IssuedTime { get; set; }

        [Display(Name = "Issued By")]
        [Required(ErrorMessage = "*")]
        public string IssuedBy { get; set; }

        [Display(Name = "Issued To")]
        [Required(ErrorMessage = "*")]
        public string IssuedTo { get; set; }

        [Display(Name = "Designation")]
        [Required(ErrorMessage = "*")]
        public string Designation { get; set; }

        [Display(Name = "Validity")]
        [Required(ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:d-MM-yyyy}")]
        public DateTime ValidTill { get; set; }

        QueryExecutor _q;

        public List<Policy> PolicyCollection { get; set; }

        public List<Policy> getPolicyCollection()
        {
            _q = new QueryExecutor();
            DataTable tab = _q.NonTransaction("Select * from policies");
            return DataConvertor.ConvertDataTable<Policy>(tab);
        }

        public bool Save()
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format(
                "INSERT INTO public.policies(" +
                "company, branchname, cardtype, numberofcards, discount, payableamount, issueddate, "+
                "issuedtime, issuedby, issuedto, designation, policynumber, validtill)" +
                "VALUES ('{0}', '{1}', '{2}', {3}, {4}, {5}, '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}')",
                this.company,this.BranchName,this.CardType,this.NumberOfCards,this.Discount,this.PayableAmount,this.IssuedDate,
                this.IssuedTime,this.IssuedBy,this.IssuedTo,this.Designation,this.PolicyNumber,this.ValidTill)) > 0;
        }

        public List<Policy> getPolicyByNumber(string PolicyNumber)
        {
            _q = new QueryExecutor();
            DataTable tab = _q.NonTransaction(
                string.Format("Select * from policies where policynumber='{0}'",PolicyNumber));
            return DataConvertor.ConvertDataTable<Policy>(tab);
        }

        public int GetLastId(string comapny)
        {
            _q = new QueryExecutor();

            return Convert
                .ToInt32(_q
                .Aggregate(string
                .Format("select count(*) from policies where company='{0}'",comapny)));
        }
        



    }
}