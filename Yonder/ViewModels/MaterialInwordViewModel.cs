using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yonder.Models;

namespace Yonder.ViewModels
{
    public class MaterialInwordViewModel
    {
        public Material_In Material_Inword { get; set; }

        public Material_In_Items Material_Inword_items { get; set; }

        public List<Shop> ShopList { get; set; }
        
        public bool Save(ref int miNumber) {
            List<bool> results=new List<bool>();
            Material_In min = new Material_In();
            results.Add(Material_Inword.addMaterialIn());
            miNumber = min.getLatestMiNumber();
            results.Add(Material_Inword_items.addMaterialInItem(miNumber));
            return !results.Contains(false);
        }

        public bool AddItem(int Minumber)
        {
            return Material_Inword_items.addMaterialInItem(Minumber);
        }

    }
}