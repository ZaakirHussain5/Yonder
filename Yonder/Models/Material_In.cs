using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yonder.Models
{
	public class Material_In
	{
		public int slno { get; set; }

		public int mi_number { get; set; }

		[Required(ErrorMessage="*")]
		[Display(Name="Inword By")]
		public string inword_By { get; set; }

		[Required(ErrorMessage = "*")]
		[Display(Name = "Inword From")]
		public string inword_From { get; set; }

		[Required(ErrorMessage = "*")]
		[Display(Name = "Inword For")]
		public string inword_for { get; set; }

		[Display(Name = "Invoice Number")]
		public string invoiceNumber { get; set; }

		[DisplayFormat(DataFormatString="{0:d-MMM-yyyy}")]
		public DateTime InwordDate { get; set; }

		public string InwordTime { get; set; }

		[Display(Name = "Mode of Transport")]
		public string mode_of_transport{ get; set; }

		[Display(Name = "Vehicle Number")]
		public string vehicle_no { get; set; }

		[Display(Name = "Incharge")]
		public string incharge { get; set; }

		[Display(Name = "Incharge Phone Number")]
		[RegularExpression("^[0-9]{10}$", ErrorMessage = "Invalid Mobile Number")]
		public string inc_phoneNumber { get; set; }

		QueryExecutor _q;

		public List<Material_In> getMaterialIn()
		{
			_q = new QueryExecutor();

			return DataConvertor.ConvertDataTable<Material_In>(
				_q.NonTransaction("select * from material_inword"));
		}

		public Material_In getMaterialIn(int Min)
		{
			_q = new QueryExecutor();

			return DataConvertor.ConvertDataTable<Material_In>(
				_q.NonTransaction("select * from material_inword where mi_number="+Min))[0];
		}

		public int getLatestMiNumber() {
			_q = new QueryExecutor();

			return Convert.ToInt32(_q.Aggregate
				("select max(mi_number) from material_inword"));
		}

		public bool addMaterialIn()
		{
			_q = new QueryExecutor();

			return _q.Transaction(
				string.Format("INSERT INTO material_inword("+
							  "invoicenumber,inword_from, inword_by, inworddate, inwordtime, inword_for, incharge, inc_phonenumber, "+
							  "mode_0f_transport,vehicle_no)" +
							  "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}','{7}','{8}','{9}');",
							  this.invoiceNumber,this.inword_From,this.inword_By,this.InwordDate,this.InwordTime,
							  this.inword_for,this.incharge,this.inc_phoneNumber,this.mode_of_transport,this.vehicle_no))>0;
		}
	}
}