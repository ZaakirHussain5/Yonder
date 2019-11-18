using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Yonder.Models
{
    public class WebContent
    {
        public decimal slno { get; set; }

        public decimal ContentId { get; set; }

        public string content_Type { get; set; }

        [Display(Name="Title")]
        public string title { get; set; }

        [Display(Name = "Short Description")]
        public string short_description { get; set; }

        [Display(Name = "Detailed Description")]
        public string detailed_description { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        public string Image { get; set; }


        QueryExecutor _q;

        public WebContent getAboutContent()
        {
            _q = new QueryExecutor();

            DataTable tab = _q.NonTransaction(
                string.Format("select * from web_contents where content_type='About'"));

            return tab.Rows.Count != 0 ?
                DataConvertor.ConvertDataTable<WebContent>(tab)[0] : new WebContent();
        }

        public WebContent getWhatsNewContent()
        {
            _q = new QueryExecutor();

            DataTable tab = _q.NonTransaction(
                string.Format("select * from web_contents where content_type='WhatsNew'"));

            return tab.Rows.Count != 0 ?
                DataConvertor.ConvertDataTable<WebContent>(tab)[0] : new WebContent();
        }

        public bool SaveAbout()
        {
            bool res = false;
            if (getAboutContent().slno == 0)
            {
                _q = new QueryExecutor();

                res = _q.Transaction(
                    string.Format("insert into web_contents("+
                    "title,short_description,detailed_description,image,content_type)"+
                    "Values('{0}','{1}','{2}','{3}','About')",this.title,this.short_description,
                    this.detailed_description,this.Image)) > 0;
            }
            else
            {
                _q = new QueryExecutor();

                res = _q.Transaction(
                    string.Format("update web_contents set title='{0}', short_description='{1}',"+
                    "detailed_description='{2}',image='{3}' where content_type='About'",this.title,
                    this.short_description,this.detailed_description,this.Image)) > 0;
            }

            return res;
        }

        public bool SaveWhatsNew()
        {
            bool res = false;
            if (getWhatsNewContent().slno == 0)
            {
                _q = new QueryExecutor();

                res = _q.Transaction(
                    string.Format("insert into web_contents(" +
                    "title,short_description,detailed_description,image,content_type)" +
                    "Values('{0}','{1}','{2}','{3}','WhatsNew')", this.title, this.short_description,
                    this.detailed_description, this.Image)) > 0;
            }
            else
            {
                _q = new QueryExecutor();

                res = _q.Transaction(
                    string.Format("update web_contents set title='{0}', short_description='{1}'," +
                    "detailed_description='{2}',image='{3}' where content_type='WhatsNew'", this.title,
                    this.short_description, this.detailed_description, this.Image)) > 0;
            }

            return res;
        }
    }
}