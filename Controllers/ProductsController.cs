using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication4.Controllers
{
    public class ProductsController : ApiController
    {
        Product[] products1 = new Product[] 
        { 
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 }, 
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M }, 
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M } 
        };

        public IEnumerable<Product> GetAllProducts()
        {
            contodb c = new contodb();
            List<Product> products = c.dothis();
            return products.ToArray();
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = products1.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [Route("api/quotes")]
        [HttpGet]
        public IEnumerable<Product> GetAllQuotes()
        {
            contodb c = new contodb();
            List<Product> products = c.dothis();
            return products.ToArray();
        }

        [Route("api/Quote/{symbol}")]
        [HttpGet]
        public void GetQuote(String symbol)
        {
            Dictionary<String, String> data = new Dictionary<string, string>();
            data.Add("symbol", symbol);
            contodb c = new contodb();
            //c.insertToQuoteTable(data);
        }

        [Route("api/Quote/{symbol}")]
        [HttpDelete]
        public IHttpActionResult DeleteQuote(String symbol)
        {
            contodb c = new contodb();
            if (c.DeleteQuote(symbol))
            {
                return Ok();
            }
            else
            {
                return null;
            }
            
        }

        [Route("api/Quote/insertQuote")]
        [HttpPost]
        public IHttpActionResult InsertQuote(HttpRequestMessage request)
        {
            
            Debug.WriteLine(request.Content.ReadAsStringAsync().Result);
            Dictionary<String, String> data = new Dictionary<string, string>();
            //data.Add("symbol", symbol);
            contodb c = new contodb();
            //c.insertToQuoteTable(data);
            return Ok();
        }


    }
}
