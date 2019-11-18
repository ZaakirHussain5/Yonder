using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yonder.Models
{
    public class ShopType
    {
        public int ShopTypeId { get; set; }

        public string shopType { get; set; }

        public List<ShopType> getShopTypes() {
            return new List<ShopType>{
                new ShopType()
                {
                    ShopTypeId=1,
                    shopType="Non-Veg"
                },
                   new ShopType()
                {
                    ShopTypeId=1,
                    shopType="Veg"
                },
                   new ShopType()
                {
                    ShopTypeId=1,
                    shopType="North Indian"
                },
                   new ShopType()
                {
                    ShopTypeId=1,
                    shopType="South Indian"
                },
                       new ShopType()
                {
                    ShopTypeId=1,
                    shopType="Continental"
                }

            };
        }

    }
}