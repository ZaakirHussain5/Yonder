using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;


namespace Yonder.Models
{
    public class offerGroup 
    {
        public string category { get; set; }
        public List<Offer> offers { get; set; }
    }
    public class Offer
    {
        public decimal slno { get; set; }

        public decimal offerid { get; set; }

        [Required(ErrorMessage="*")]
        [Display(Name="Offer Category")]
        public string category { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Offer Valid From")]
        public DateTime Valid_From { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Offer Validity")]
        public DateTime Valid_Till { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        public string Image { get; set; }

        QueryExecutor _q;
        public bool AddOffer()
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format("insert into offers(category,valid_from,valid_till,image)"+
                "values('{0}','{1}','{2}','{3}')",this.category,this.Valid_From,this.Valid_Till,this.Image)) > 0;
        }

        public bool RemoveOffer()
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format("delete from offers where offerid={0}",this.offerid)) > 0;
        }

        public List<ShopCategory> offerTypes { get; set; }

        public List<offerGroup> getOffers()
        {
            _q = new QueryExecutor();

            return ProcessData(DataConvertor.ConvertDataTable<Offer>(
                _q.NonTransaction("Select * from offers")));
        }

        private List<offerGroup> ProcessData(List<Offer> list)
        {
            List<offerGroup> og = new List<offerGroup>();
            foreach (var offer in list)
            {
                offerGroup o = new offerGroup();
                o.category = offer.category;
                o.offers = list.Where(x => x.category == offer.category).ToList();
                if (og.Where(x => x.category == o.category).ToList().Count == 0)
                    og.Add(o);
            }

            return og;
        }
    }
}
