using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication4
{
    public partial class History : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataClasses2DataContext dataContext = new DataClasses2DataContext();

            
            GridView1.DataSource = from h in dataContext.Hists
                                   select h;


            GridView1.DataBind();
        }

        protected void btn_filter_Click(object sender, EventArgs e)
        {
            DataClasses2DataContext dataContext = new DataClasses2DataContext();
            var query = dataContext.Hists.AsQueryable();
            if (txt_symbol.Text != "")
            {
                query = query.Where(p => p.symbol == txt_symbol.Text);
            }
            if (txt_high_min.Text != "")
            {
                query = query.Where(p => p.high > Convert.ToDouble(txt_high_min.Text));
            }
            if (txt_high_max.Text != "")
            {
                query = query.Where(p => p.high < Convert.ToDouble(txt_high_max.Text));
            }
            if (txt_Low_min.Text != "")
            {
                query = query.Where(p => p.low > Convert.ToDouble(txt_Low_min.Text));
            }
            if (txt_Low_max.Text != "")
            {
                query = query.Where(p => p.low < Convert.ToDouble(txt_Low_max.Text));
            }
            if (txt_Timestamp.Text != "")
            {
                query = query.Where(p => p.timestamp == Convert.ToDateTime(txt_Timestamp.Text));
            }

            GridView1.DataSource = query;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/WebForm1.aspx");
        }
    }
}