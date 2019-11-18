using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yonder.Models
{
    public class StoreSupplier
    {
        public decimal slno { get; set; }

        public decimal Supplier_Id { get; set; }

        [Required(ErrorMessage="*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Mobile Number")]
        public string Mobile_Number { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Pin Code")]
        public string pinCode { get; set; }

        public string GSTIN { get; set; }

        QueryExecutor _q;
        public bool AddSupplier()
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format("insert into store_suppliers("+
                "name,mobile_number,address,pincode,gstin)VALUES("+
                "'{0}','{1}','{2}','{3}','{4}')",this.Name,this.Mobile_Number,
                this.Address,this.pinCode,this.GSTIN)) > 0;
        }

        public bool UpdateSupplier()
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format("Update store_suppliers set " +
                "name = '{0}',mobile_number='{1}',address='{2}',"+
                "pincode='{3}',gstin='{4}' " +
                "where supplier_id={5}", this.Name, this.Mobile_Number,
                this.Address, this.pinCode, this.GSTIN,this.Supplier_Id)) > 0;
        }

        public List<StoreSupplier> GetSuppliers()
        {
            _q = new QueryExecutor();

            return DataConvertor.ConvertDataTable<StoreSupplier>(
                _q.NonTransaction("Select * from store_suppliers"));
        }



        public bool DeleteSupplier(int Supplier_id)
        {
            _q = new QueryExecutor();

            return _q
                .Transaction("delete from store_suppliers where supplier_id=" + Supplier_id) > 0;
        }
    }
}