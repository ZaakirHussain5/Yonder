using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Yonder.Models
{
    public class CardType
    {
        public int CardTypeId { get; set; }
        
        [Display(Name="Card Type")]
        [Required(ErrorMessage="*")]
        public string Type { get; set; }

        [Display(Name="Card Value")]
        [Required(ErrorMessage = "*")]
        public decimal value { get; set; }

        [Display(Name="Discount in %")]
        [Required(ErrorMessage = "*")]
        public int discount { get; set; }

        public List<CardType> CardTypesCollection { get; set; }

        QueryExecutor _q = null;
        
        public List<CardType> getCardTypes()
        {
            _q = new QueryExecutor();
            DataTable tab = _q.NonTransaction("select * from cardtypes");
            CardTypesCollection = new List<CardType>();
            CardTypesCollection = DataConvertor.ConvertDataTable<CardType>(tab);
            return CardTypesCollection;
        }

        public DataTable getCardTypesTable()
        {
            _q = new QueryExecutor();
            DataTable tab = _q.NonTransaction("select * from cardtypes");
            return tab;
        }

        public CardType getCardTypeById(int id)
        {
            _q = new QueryExecutor();
            DataTable tab = _q.NonTransaction("select * from cardtypes where cardTypeid="+id);
            CardTypesCollection = new List<CardType>();
            CardTypesCollection = DataConvertor.ConvertDataTable<CardType>(tab);
            return CardTypesCollection[0];
        }

        public CardType getCardTypeByName(string card)
        {
            _q = new QueryExecutor();
            DataTable tab = _q.NonTransaction(
                string.Format("select * from cardtypes where type='{0}'" , card));
            CardTypesCollection = new List<CardType>();
            CardTypesCollection = DataConvertor.ConvertDataTable<CardType>(tab);
            return CardTypesCollection[0];
        }

        public bool Save()
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format(
                "insert into cardtypes (type,value,discount)" +
                "values ('{0}',{1},{2})"
                , this.Type.ToUpper(), this.value, this.discount)) > 0;
        }

        public bool updateCardType(int id)
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format(
                "Update cardtypes set type='{0}' , value={1}, discount={2} where cardtypeid={3}"
                , this.Type.ToUpper(), this.value, this.discount,id)) > 0;
        }

        public bool deleteCardType(int id)
        {
            _q = new QueryExecutor();

            return _q.Transaction(
                string.Format(
                "Delete from cardtypes where cardtypeid={0}"
                ,id)) > 0;
        }



    }
}