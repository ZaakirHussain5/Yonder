using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yonder.Models
{
    public class Package
    {
        public int slno { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name="Card Type")]
        public string CardType { get; set; }

        public List<CardType> CardTypeCollection { get; set; }

        [Required(ErrorMessage="*")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name="Description")]
        public string Desc { get; set; }

        [Required(ErrorMessage="*")]
        [Display(Name = "Package Type")]
        public string Type { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Payable Amount")]
        public decimal payAbleAmt { get; set; }

     

        public bool isActive { get; set; }

        QueryExecutor _q;

        public List<Package> getPackages()
        {
            _q = new QueryExecutor();

            return DataConvertor.ConvertDataTable<Package>
                (_q.NonTransaction("select * from packages"));
        }

        public bool AddPackage()
        {
            _q = new QueryExecutor();

            return _q.Transaction
                (string.Format(
                "insert into packages(cardtype,amount,\"desc\",type,payableamt)"+
                "values ('{0}',{1},'{2}','{3}',{4})", this.CardType, this.Amount, this.Desc,this.Type,this.payAbleAmt)) > 0;
        }

        public bool UpdatePackage(int id)
        {
            _q = new QueryExecutor();

            return _q.Transaction
                (string.Format(
                "Update packages set cardtype='{0}',amount={1}," +
                "\"desc\"='{2}',payableamt='{3}' where slno={4}", this.CardType, this.Amount, this.Desc, this.payAbleAmt, id)) > 0;
        }

        public bool DeletePackage(int id)
        {
            _q = new QueryExecutor();

            return _q.Transaction
                (string.Format(
                "delete from packages where slno={0}",id)) > 0;
        }

        public List<Package> getPackages_cardType(string cardType)
        {
            _q = new QueryExecutor();

            return DataConvertor.ConvertDataTable<Package>
                (_q.NonTransaction(
                string.Format("select * from packages where cardtype='{0}'", cardType)));
        }

        public List<Package> getPackageDescription(int Amount)
        {
            _q = new QueryExecutor();

            return DataConvertor.ConvertDataTable<Package>
                (_q.NonTransaction(
                string.Format("select * from packages where amount='{0}'", Amount)));
        }

        public bool updatePackageStatus(bool status)
        {
            _q = new QueryExecutor();

            return Convert.ToBoolean
                (_q.Transaction(
                string.Format("Update packages set isactive='{0}' where slno={1}", status,this.slno)));
        }

        public Package getPackageById(int id)
        {
            _q = new QueryExecutor();

            return DataConvertor.ConvertDataTable<Package>
                (_q.NonTransaction(
                string.Format("select * from packages where slno='{0}'", id)))[0];
        }

        public Package getPackageByAmount(int id)
        {
            _q = new QueryExecutor();

            return DataConvertor.ConvertDataTable<Package>
                (_q.NonTransaction(
                string.Format("select * from packages where amount='{0}'", id)))[0];
        }

        
    }
}