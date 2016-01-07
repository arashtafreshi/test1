using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebApplication4
{
    public class contodb
    {
        //static String conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Home\Desktop\Database2.mdf;Integrated Security=True;Connect Timeout=30";
        static String conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\MyProducts.mdf;Integrated Security=True;Connect Timeout=30";
       
        /*
        static String[] quoteTableFields = {"symbol", "name", "dayCode", "serverTimestamp", "mode", "lastPrice", "tradeSize", "netChange",
            "percentChange", "tick", "previousLastPrice", "previousTimestamp", "bid", "bidSize", "ask", "askSize", "unitCode", "open",
            "high", "low", "close", "numTrades", "dollarVolume", "flag", "previousClose", "settlement", "previousSettlement", "volume",
            "previousVolume", "openInterest"};
        */

        static List<myColumn> quoteColumns= new List<myColumn>();
        static Dictionary<String, String> quoteDic = new Dictionary<string, string>();

        public contodb()
        {
            if(quoteColumns.Count == 0)
            {
                getQuoteTableColumns();
            }
        }

        public List<Product> dothis()
        {
            getQuoteTableColumns();
            //Debug.WriteLine(quoteColumns[0].Name+"---- "+quoteColumns[0].type);
            //Debug.WriteLine("bid ---- " + quoteDic["bid"]);
            List<Product> products = new List<Product>();
            SqlDataReader reader;
            
            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            SqlCommand sc = new SqlCommand("SELECT * FROM ProductTable", conn);
            reader = sc.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    products.Add(new Product() { Id = reader.GetInt32(0), Name = reader.GetString(1), Category = reader.GetString(2), Price = reader.GetInt32(3) });
                    Debug.WriteLine("{0}\t{1}\t{2}\t{3}", reader.GetInt32(0),
                        reader.GetString(1), 
                        reader.GetString(2),
                        reader.GetInt32(3));
                }
            }
            else
            {
                Debug.WriteLine("No rows found.");
            }
            
            conn.Close();
            return products;
        }

        




        public Boolean insertToQuoteTable(List<Dictionary<String, String>> dicList)
        {

            foreach (Dictionary<String, String> dic in dicList)
            {
                try
                {
                    if (!isSymbolExists(dic["symbol"]))
                    {
                        SqlDataReader reader;
                        String sqlStatement = makeStatement(dic);



                        SqlConnection conn = new SqlConnection(conString);
                        conn.Open();
                        SqlCommand sc = new SqlCommand(sqlStatement, conn);
                        sc.ExecuteNonQuery();
                        conn.Close();
                        
                    }
                    else
                    {
                        
                    }

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    return false;
                }
            }
            return true;


        }

        public Boolean DeleteQuote(String symbol)
        {
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                if(isSymbolExists(symbol))
                {
                    conn.Open();
                    SqlCommand sc = new SqlCommand("DELETE FROM Quote WHERE symbol='" + symbol + "'", conn);
                    sc.ExecuteNonQuery();
                    conn.Close();
                    Debug.WriteLine("Symbol " + symbol + " successfully deleted!");
                }
                
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                conn.Close();
                Debug.WriteLine("Symbol " + symbol + " not deleted!");
                return false;
            }
            
          
        }
        
        // gets the name of the columns and their types
        public void getQuoteTableColumns()
        {
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Quote", conn);
            reader=command.ExecuteReader();
           for(int i=0; i<reader.FieldCount; i++)
            { 
                quoteColumns.Add(new myColumn { Name = reader.GetName(i), type = reader.GetDataTypeName(i) });
                //Debug.WriteLine("public " + reader.GetDataTypeName(i) + " " + reader.GetName(i) + " { get; set; }");
                Debug.WriteLine(reader.GetDataTypeName(i)+"\t");
                if (!quoteDic.ContainsKey(reader.GetName(i)))
                {
                    quoteDic.Add(reader.GetName(i), reader.GetDataTypeName(i));
                }
                
            }
            conn.Close();
        }

        public Boolean isSymbolExists(String symbol)
        {
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            SqlCommand sc = new SqlCommand("SELECT symbol FROM Quote WHERE symbol='"+symbol+"'", conn);
            reader = sc.ExecuteReader();
            if (reader.HasRows)
            {
                Debug.WriteLine("Symbol "+symbol+" already exists!");
                conn.Close();
                return true;
            }
            else
            {
                Debug.WriteLine("Symbol " + symbol + " not exists!");
                conn.Close();
                return false;
            }
        }

        public String makeStatement(Dictionary<String, String> dic)
        {
            String Value = "";
            String fields = "";
            foreach (String key in dic.Keys)
            {
                if (quoteDic.ContainsKey(key))
                {
                    switch (quoteDic[key]){
                        case "int":
                            Value += dic[key];
                            fields += ("["+key+"]");
                            break;
                        case "float":
                            Value += dic[key];
                            fields += ("[" + key + "]");
                            break;
                        case "date":
                            DateTime dt = Convert.ToDateTime(dic[key]);
                            Value += ("'" + dt.Date.ToString("yyyy-MM-dd") + "'");
                            fields += ("[" + key + "]");
                            break;
                        case "datetime":
                            DateTime dt2 = Convert.ToDateTime(dic[key]);
                            Value += ("'" + dt2.Date.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                            fields += ("[" + key + "]");
                            break;
                        case "nchar":
                            Value += ("'" + dic[key] + "'");
                            fields += ("[" + key + "]");
                            break;
                    }
                    
                    Value += ",";
                    fields += ",";
                }
                
            }

            Value = Value.Remove(Value.Length - 1, 1);
            fields = fields.Remove(fields.Length - 1, 1);

            Debug.WriteLine("INSERT into Quote (" + fields + ") VALUES (" + Value + ")");
            return "INSERT into Quote ("+fields+") VALUES ("+Value+")";
        }

    }

    

}