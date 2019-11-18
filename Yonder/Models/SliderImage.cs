using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yonder.Models
{
    public class SliderImage
    {
        public int Slno { get; set; }

        public string Image { get; set; }

        [Required]
        [Range(1,20,ErrorMessage="Invalid Sequence")]
        public int sequence { get; set; }

        QueryExecutor _q;
        public bool AddImage() 
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                "insert into slider_images (image,sequence) values ('"+this.Image+"',"+this.sequence+")") > 0;
        }

        public bool RemoveImage(int Slno)
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                "delete from slider_images where slno=" + Slno ) > 0;
        }

        public HttpPostedFileBase ImageFile { get; set; }

        internal List<SliderImage> getImages()
        {
            _q = new QueryExecutor();

            return DataConvertor
                .ConvertDataTable<SliderImage>(_q.NonTransaction(
                "Select * from slider_images order by sequence"));
        }
    }
}