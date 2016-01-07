using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebApplication4
{
    public class HistoryApiHandler
    {

        static String conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\MyProducts.mdf;Integrated Security=True;Connect Timeout=30";

        static List<myColumn> historyColumns = new List<myColumn>();
        static Dictionary<String, String> historyDic = new Dictionary<string, string>();

        public HistoryApiHandler()
        {
            if (historyColumns.Count == 0)
            {
                getHistoryTableColumns();
            }
        }

        public void getHistoryTableColumns()
        {
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Hist", conn);
            reader = command.ExecuteReader();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                historyColumns.Add(new myColumn { Name = reader.GetName(i), type = reader.GetDataTypeName(i) });
                //Debug.WriteLine("public " + reader.GetDataTypeName(i) + " " + reader.GetName(i) + " { get; set; }");
                Debug.WriteLine(reader.GetDataTypeName(i) + "\t");
                if (!historyDic.ContainsKey(reader.GetName(i)))
                {
                    historyDic.Add(reader.GetName(i), reader.GetDataTypeName(i));
                }

            }
            conn.Close();
        }

        public Boolean insertToHistTable(List<Dictionary<String, String>> dicList)
        {

            foreach(Dictionary<String,String>  dic in dicList)
            {
                try
                {
                    
                        SqlDataReader reader;
                        String sqlStatement = makeStatement(dic);

                        SqlConnection conn = new SqlConnection(conString);
                        conn.Open();
                        SqlCommand sc = new SqlCommand(sqlStatement, conn);
                        sc.ExecuteNonQuery();
                        conn.Close();
                        

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    return false;
                }
            }
            return true;
            

        }


        public Boolean DeleteHistory(String symbol)
        {
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                if (isSymbolExists(symbol))
                {
                    conn.Open();
                    SqlCommand sc = new SqlCommand("DELETE FROM Hist WHERE symbol='" + symbol + "'", conn);
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

        public Boolean isSymbolExists(String symbol)
        {
            SqlDataReader reader;
            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            SqlCommand sc = new SqlCommand("SELECT symbol FROM Hist WHERE symbol='" + symbol + "'", conn);
            reader = sc.ExecuteReader();
            if (reader.HasRows)
            {
                Debug.WriteLine("Symbol " + symbol + " already exists!");
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
                if (historyDic.ContainsKey(key))
                {
                    
                    switch (historyDic[key])
                    {
                        case "int":
                            Value += dic[key];
                            fields += ("[" + key + "]");
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

            Debug.WriteLine("INSERT into Hist (" + fields + ") VALUES (" + Value + ")");
            return "INSERT into Hist (" + fields + ") VALUES (" + Value + ")";
        }








    }
}