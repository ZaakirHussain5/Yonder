using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Yonder.Models
{
	public class BillingChild
	{
		public string invoiceNumber { get; set; }

		public int shopNumber { get; set; }

		public string financialYear { get; set; }

		public int slno { get; set; }

		[Required(ErrorMessage="*")]
		[Display(Name="Item")]
		public string itemName { get; set; }

		[Required(ErrorMessage = "*")]
		[Display(Name = "Category")]
		public string category { get; set; }

		[Required(ErrorMessage = "*")]
		[Display(Name = "Price")]
		public decimal price { get; set;}

		[Required(ErrorMessage = "*")]
		[Display(Name = "Quantity")]
		[Range(1,10000,ErrorMessage="Quantity Can't be Zero")]
		public int quantity { get; set; }

		[Required(ErrorMessage = "*")]
		[Display(Name = "Total")]
		public decimal total { get; set; }

		public DateTime BilledDate { get; set; }

		public bool isValid { get; set; }

		public decimal totalWithTax { get; set; }

		public decimal gst { get; set; }

		public decimal cgst { get; set; }

		public decimal sgst { get; set; }

		public decimal gst_amt { get; set; }

		public decimal cgst_amt { get; set; }

		public decimal sgst_amt { get; set; }

		QueryExecutor _q;

		public bool addBillItem()
		{
			_q = new QueryExecutor();

			return _q.Transaction(
				string.Format(
				"INSERT INTO public.billingchild("+
				"invoicenumber, shopnumber,financialyear, slno,"+ 
				"itemname, price, quantity, total, billeddate,"+
				"gst, cgst, sgst, gst_amt, cgst_amt, sgst_amt, totalwithtax)"+
				"VALUES ('{0}', '{1}', '{2}', '{3}', '{4}','{5}','{6}','{7}', '{8}', '{9}',"+
				"'{10}','{11}', '{12}', '{13}', '{14}','{15}');",this.invoiceNumber,this.shopNumber,this.financialYear,this.slno,this.itemName,
				this.price,this.quantity,this.total,this.BilledDate,this.gst,this.cgst,this.sgst,this.gst_amt,this.cgst_amt,
				this.sgst_amt,this.totalWithTax)) > 0;
		}

		public List<BillingChild> getBillItems(int shopNumber,string invNumber)
		{
			_q = new QueryExecutor();

			return DataConvertor.ConvertDataTable<BillingChild>(
			   _q.NonTransaction("select * from billingchild where invoicenumber='" + invNumber + "' and shopnumber=" + shopNumber));
		}
	}
}