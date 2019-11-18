using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yonder.Models
{
    public class Material_In_Items
    {
        public int slno { get; set; }

        public int minumber { get; set; }

        [Required(ErrorMessage="*")]
        [Display(Name = "Particulars")]
        public string particulars { get; set; }

        [Required(ErrorMessage = "*")]
        public string Quantity { get; set; }

        QueryExecutor _q;

        public List<Material_In_Items> getMaterialInItems(int mi_number)
        {
            _q = new QueryExecutor();

            return DataConvertor.ConvertDataTable<Material_In_Items>(
                _q.NonTransaction("select * from material_inword_items where minumber="+mi_number));
        }

        public bool addMaterialInItem(int mi_number)
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format("insert into material_inword_items(minumber,particulars,quantity)"+
                "values ({0},'{1}','{2}')", mi_number, this.particulars, this.Quantity)) > 0;
        }

        public bool removeMaterialInItem(int slno)
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format("delete from material_inword_items where slno={0}", slno)) > 0;
        }

      
    }
}