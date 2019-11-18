using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yonder.Models
{
    public class StoreInventory
    {
        [Required(ErrorMessage="*")]
        [Display(Name="Item Code/Bar code")]
        public string item_Code { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Category")]
        public string category { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Item")]
        public string item { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "UOM")]
        public string UOM { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "GST")]
        public decimal gst { get; set; }

        [Display(Name = "HSN Code")]
        public string HSN_Code { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Price")]
        public decimal price { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Available Quantity")]
        public decimal avl_Qty { get; set; }

        public string Expiry_Date { get; set; }

        QueryExecutor _q;

        public List<StoreInventory> getInventory()
        {
            _q = new QueryExecutor();

            return DataConvertor.ConvertDataTable<StoreInventory>
                (_q.NonTransaction("select * from item_inventory"));
        }

        public bool AddInventory()
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format("INSERT INTO item_inventory("+
                "item_code, category, item, hsn_code, uom, gst, "+
                "price, avl_qty, expiry_date)"+
                "VALUES ('{0}', '{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                this.item_Code,this.category,this.item,this.HSN_Code,this.UOM,this.gst,
                this.price,this.avl_Qty,this.Expiry_Date)) > 0;
        }



        public List<StoreCategory> StoreCategories { get; set; }

        public List<UOM> StoreUOMs { get; set; }

        public List<GST> StoreGSTs { get; set; }

        public int status { get; set; }

        internal bool UpdateInvenory()
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format("update item_inventory set " +
                " category='{1}', item='{2}', hsn_code='{3}', uom='{4}', "+
                "gst='{5}',price='{6}', avl_qty='{7}', " +
                " expiry_date = '{8}' where item_code='{0}'" ,
                this.item_Code, this.category, this.item, this.HSN_Code, this.UOM, this.gst,
                this.price, this.avl_Qty, this.Expiry_Date)) > 0;
        }

        internal StoreInventory getInventory_ItemCode(string id)
        {
            _q = new QueryExecutor();

            return DataConvertor.ConvertDataTable<StoreInventory>
                (_q.NonTransaction(
                string.Format("select * from item_inventory where item_code='{0}'",id)))[0];
        }
    }
}