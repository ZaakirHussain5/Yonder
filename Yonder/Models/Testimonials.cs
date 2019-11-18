using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yonder.Models
{
    public class Testimonials
    {
        public decimal id { get; set; }

        public string Testimonial { get; set; }

        public string UserName { get; set; }

        QueryExecutor _q;

        public List<Testimonials> getTesimonials()
        {
            _q = new QueryExecutor();

            return DataConvertor.ConvertDataTable<Testimonials>
                (_q.NonTransaction("select * from testimonials"));
        }

    }
}