using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;



namespace Yonder.Models
{
    public class QueryExecutor
    {
        NpgsqlConnection con = null;
        NpgsqlCommand cmd = null;
        NpgsqlDataAdapter adp = null;

        public QueryExecutor()
        {
            con = new NpgsqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            if (con.State != ConnectionState.Open) con.Open();
            cmd = new NpgsqlCommand();
            cmd.Connection = con;
        }

        public int Transaction(string Query)
        {
            int res = 0;
            try
            {
                cmd.CommandText = Query;
                res = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch(NpgsqlException ex)
            {
                res = 0;
            }
            return res;
        }

        public DataTable NonTransaction(string Query)
        {
            DataTable res = new DataTable();
            try
            {
                cmd.CommandText = Query;
                adp = new NpgsqlDataAdapter(cmd);
                adp.Fill(res);
                con.Close();
            }
            catch (NpgsqlException ex)
            {
                res = null;
            }
            return res;
        }

        public double Aggregate(string Query)
        {
            double res = 0;
            try
            {
                cmd.CommandText = Query;
                res = Convert.ToDouble(cmd.ExecuteScalar());
                con.Close();
            }
            catch (NpgsqlException ex)
            {
                res = 0;
            }
            return res;
        }
    }
}