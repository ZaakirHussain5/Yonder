using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yonder.Models
{
    public class ShopCategory
    {
        public int floorNumber { get; set; }

        [Required(ErrorMessage="*")]
        [Display(Name="Floor Type")]
        public string  FloorType { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Floor Type")]
        public int numberOfShops { get; set; }

        QueryExecutor _q;
        public List<ShopCategory> getCategories()
        {
            _q = new QueryExecutor();

            return DataConvertor.ConvertDataTable<ShopCategory>(
                _q.NonTransaction("select * from floordetails"));
        }
    }
}