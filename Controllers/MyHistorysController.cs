using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;

namespace WebApplication4.Controllers
{
    public class MyHistorysController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public String Post(myUrlApiDataSource data)
        {
            
            List<Dictionary<String, String>> nodeDicList = new List<Dictionary<string, string>>();
            String url = data.url;
            url = url.Replace(".json", ".xml");
            url = url.Replace(".csv", ".xml");
            Debug.WriteLine("the url is: " + url);
            WebRequest request = WebRequest.Create(url);
            // Create a request for the URL. 
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            //Debug.WriteLine(responseFromServer);

            // Clean up the streams and the response.
            reader.Close();
            response.Close();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseFromServer);
            

            // returns the number of items in the xml file
            int items = xmlDoc.GetElementsByTagName("item").Count;

            
                
                XmlNodeList nodeList = xmlDoc.GetElementsByTagName("item");
                string Short_Fall = string.Empty;
                foreach (XmlNode node in nodeList)
                {
                    Dictionary<String, String> nodeDic = new Dictionary<string, string>();
                    //Debug.WriteLine(Short_Fall = node.InnerText);
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                    {
                        Debug.WriteLine("-- "+node.ChildNodes.Item(i).Name);
                        if (node.ChildNodes.Item(i).InnerText != "")
                        {
                            nodeDic.Add(node.ChildNodes.Item(i).Name, node.ChildNodes.Item(i).InnerText);
                            //Debug.WriteLine( node.ChildNodes.Item(i).Name);
                        }

                    }

                    nodeDicList.Add(nodeDic);
                    //Debug.WriteLine(Short_Fall = node.InnerXml);
                }

                

            

            HistoryApiHandler c = new HistoryApiHandler();
            c.insertToHistTable(nodeDicList);

            //Debug.WriteLine(data.ToString());
            Debug.WriteLine("post: - " + data.url);
            return "post: " + data.url;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}