using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication4
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataClasses1DataContext dataContext = new DataClasses1DataContext();
            
            GridView1.DataSource = from quote in dataContext.Quotes
                                   select quote;
                                   

            GridView1.DataBind();
        }

        protected void btn_filter_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext dataContext = new DataClasses1DataContext();
            var query = dataContext.Quotes.AsQueryable();
            if(txt_symbol.Text != "")
            {
                query = query.Where(p => p.symbol == txt_symbol.Text);
            }
            if (txt_high.Text != "")
            {
                query = query.Where(p => p.high == Convert.ToDouble(txt_high.Text));
            }
            if (txt_low_min.Text != "")
            {
                query = query.Where(p => p.low > Convert.ToDouble(txt_low_min.Text));
            }
            if (txt_low_max.Text != "")
            {
                query = query.Where(p => p.low < Convert.ToDouble(txt_low_max.Text));
            }

            GridView1.DataSource = query;
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("/History.aspx");
        }
    }
}