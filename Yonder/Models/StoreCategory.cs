using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yonder.Models
{
    public class UOM
    {
        public int slno { get; set; }

        public string Uom { get; set; }

        QueryExecutor _q;

        public List<UOM> getUOMs()
        {
            _q = new QueryExecutor();
            return DataConvertor.ConvertDataTable<UOM>
                (_q.NonTransaction("select * from uom"));
        }

        public bool AddUOM()
        {
            _q = new QueryExecutor();

            return _q.Transaction(string.Format
                ("insert into uom(uom) values ('{0}')", this.Uom)) > 0;
        }

        public bool Delete(string id)
        {
            _q = new QueryExecutor();

            return _q.Transaction(string.Format
                ("delete from uom where uom='{0}'", id)) > 0;
        }
    }

    public class GST
    {
        public int slno { get; set; }

        public decimal gst { get; set; }

        QueryExecutor _q;

        public List<GST> getGSTs()
        {
            _q = new QueryExecutor();
            return DataConvertor.ConvertDataTable<GST>
                (_q.NonTransaction("select * from store_gst"));
        }

        public bool AddGST()
        {
            _q = new QueryExecutor();

            return _q.Transaction(string.Format
                ("insert into store_gst(gst) values ('{0}')", this.gst)) > 0;
        }

        public bool Delete(decimal id)
        {
            _q = new QueryExecutor();

            return _q.Transaction(string.Format
                ("delete from store_gst where gst={0}", id)) > 0;
        }
    }

    public class StoreCategory
    {
        public decimal category_id { get; set; }

        public string Category { get; set; }

        QueryExecutor _q;

        public List<StoreCategory> getCategories()
        {
            _q = new QueryExecutor();
            return DataConvertor.ConvertDataTable<StoreCategory>
                (_q.NonTransaction("select * from store_categories"));
        }

        public bool AddCategory()
        {
            _q = new QueryExecutor();

            return _q.Transaction(string.Format
                ("insert into store_categories(category) values ('{0}')",  this.Category)) > 0;
        }

        public bool Delete(string id)
        {
            _q = new QueryExecutor();

            return _q.Transaction(string.Format
                ("delete from store_categories where category='{0}'", id)) > 0;
        }
    }
}