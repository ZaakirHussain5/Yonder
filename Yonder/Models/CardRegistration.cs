using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Yonder.Models
{
    public class CardRegistration
    {
        [Display(Name="Card Number")]
        [Required(ErrorMessage="*")]
        public string CardNumber { get; set; }

        [Display(Name = "RFID Number")]
        [Required(ErrorMessage = "*")]
        [StringLength(10, MinimumLength = 10)]
        public string RFID { get; set; }

        [Display(Name = "Policy Number")]
        [Required(ErrorMessage = "*")]
        public string PolicyNumber { get; set; }

        [Display(Name = "Card Type")]
        [Required(ErrorMessage = "*")]
        public string CardType { get; set; }

        [Display(Name = "Card Value")]
        [Required(ErrorMessage = "*")]
        public string Value { get; set; }

        public bool isRegistered { get; set; }

        public bool isActive { get; set; }

        public bool isBlocked { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "*")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage="Invalid Email")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "Employee ID")]
        [Required(ErrorMessage = "*")]
        public string EmpId { get; set; }

        [Display(Name = "Designation")]
        [Required(ErrorMessage = "*")]
        public string Designation { get; set; }

        [Display(Name = "Department")]
        [Required(ErrorMessage = "*")]
        public string Department { get; set; }

        [Display(Name = "Identification Type")]
        [Required(ErrorMessage = "*")]
        public string IdenType { get;set;}
        
        [Required(ErrorMessage="*")]
        [Display(Name="Discount")]
        public int Discount { get; set; }

        public List<SelectListItem> idenTypes
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem{Text="PAN",Value="PAN"},
                    new SelectListItem{Text="Aadhaar Number",Value="Aadhaar Number"},
                    new SelectListItem{Text="Voter ID",Value="Voter ID"},
                    new SelectListItem{Text="Driving License",Value="Driving License"},
                };
            }
        }

        [Display(Name = "Identification Number")]
        [Required(ErrorMessage = "*")]
        public string IdenNo { get; set; }

        [Display(Name = "Registered By")]
        [Required(ErrorMessage = "*")]
        public string RegisteredBy { get; set; }

        [Display(Name = "Registration Identity")]
        [Required(ErrorMessage = "*")]
        public string RegistrationIden { get; set; }

        public int NumberOfCardsIssued { get; set; }

        public int TotalNumberofcards { get; set; }

        public string Name{ get; set; }

        public string nameSetter 
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
            set 
            {
                Name = value;
            }
        }

        public decimal Balance { get; set; }

        public string userType { get; set; }

        public string PIN { get; set; }

        public string regDate { get; set; }

        public string Validity { get; set; }

        [Required(ErrorMessage="*")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name="DOB")]
        public string birthDate { get; set; }

        [Display(Name = "Anniversary Date")]
        public string anniversaryDate { get; set; }

        [Display(Name = "Marital Status")]
        public string maritalStatus { get; set; }

        public List<CardType> CardTypes { get; set; }

        QueryExecutor _q;
        public int getCount(string PolicyNumber)
        {
            _q = new QueryExecutor();

            return Convert.ToInt32(_q.Aggregate(
                string.Format("select count(*) from cardregistration where policynumber='{0}'",PolicyNumber)));
        }

        public bool Register()
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format("insert into cardregistration"+
                "(cardnumber,policynumber,rfid,cardtype,value,isactive,pin,balance)"+
                "Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}',{7})"
                ,this.CardNumber,this.PolicyNumber,this.RFID,this.CardType,this.Value,this.isActive,this.PIN,this.Value)) > 0;
        }

        public bool RegisterWalkin()
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format("insert into cardregistration" +
                "(cardnumber,rfid,cardtype,value,pin,balance,name,email,"+
                "phoneNumber,address,usertype,identype,idenno,isregistered," +
                "regdate, validity, birthdate, gender, maritalstatus, anniversarydate)" +
                "Values ('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}','{8}','{9}','{10}','{11}','{12}',true,"+
                "'{13}','{14}','{15}','{16}','{17}','{18}')"
                , this.CardNumber, this.RFID, this.CardType, this.Value, this.PIN, this.Value,this.nameSetter,
                this.Email,this.PhoneNumber,this.Address,this.userType,this.IdenType,this.IdenNo,this.regDate,this.Validity,this.birthDate,
                this.Gender,this.maritalStatus,this.anniversaryDate)) > 0;
        }

        public List<CardRegistration> getPolicyCards(string policyNumber)
        {
            _q = new QueryExecutor();

            return DataConvertor
                .ConvertDataTable<CardRegistration>(_q
                .NonTransaction(string
                .Format("select * from cardregistration where policynumber='{0}'",policyNumber)));
        }

        public bool check(string CardNumber)
        {
            _q = new QueryExecutor();

            return Convert.ToBoolean(_q.Aggregate(
                string.Format("select count(*) from cardregistration where cardnumber='{0}'", CardNumber)));
        }

        public CardRegistration getCardInfo(string CardNumber)
        {
            _q = new QueryExecutor();
            return DataConvertor.ConvertDataTable<CardRegistration>(_q.NonTransaction(
                string.Format("select * from cardregistration where cardnumber='{0}'", CardNumber)))[0];
        }

        public bool Save()
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format("Update cardregistration " +
                "set isregistered='true', name='{0}',email='{1}',phonenumber='{2}',address='{3}'," +
                "empid='{4}',designation='{5}',department='{6}',usertype='{7}',identype='{8}',"+
                "idenno='{9}',registeredby='{10}',registrationiden='{11}' where cardnumber='{12}'",
                this.Name,this.Email,this.PhoneNumber,this.Address,this.EmpId,this.Designation,this.Department,
                this.userType,this.IdenType,this.IdenNo,this.RegisteredBy,this.RegistrationIden,this.CardNumber)) > 0;
        }

        public bool UpdateBalanceCredit(int amount)
        {
            _q = new QueryExecutor();
            return _q.Transaction(
                string.Format("Update cardregistration " +
                "set balance=balance+{0} where cardnumber='{1}'",
                amount,this.CardNumber)) > 0;
        }

        public bool UpdateBalanceDebit(decimal amount)
        {
            _q = new QueryExecutor();
            return _q.Transaction(
                string.Format("Update cardregistration " +
                "set balance=balance-{0} where cardnumber='{1}'",
                amount, this.CardNumber)) > 0;
        }





        internal object acivateAccount(string cardNumber)
        {
            _q = new QueryExecutor();
            return _q.Transaction(
                string.Format("Update cardregistration " +
                "set isactive='true' where cardnumber='{0}'",
                cardNumber)) > 0;
        }

        internal bool updateProfile()
        {
            _q = new QueryExecutor();
            return _q.Transaction(
                string.Format("Update cardregistration " +
                "set name='{0}',email='{1}',birthdate='{2}',address='{3}' "+
                "where cardnumber='{4}'",this.Name,
                this.Email,this.birthDate,this.Address,this.CardNumber)) > 0;
        }

        internal bool updatePin(string pin, string cardNumber)
        {
            _q = new QueryExecutor();
            return _q.Transaction(
                string.Format("Update cardregistration " +
                "set pin='{1}' where cardnumber='{0}'",
                cardNumber,pin)) > 0;
        }
    }
}