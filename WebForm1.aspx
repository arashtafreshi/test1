<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication4.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quote</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
</head>
<body>


    <form id="form1" runat="server">

    <div class="container">
        <div class="jumbotron">
            <h2>Quote</h2>
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Navigate to History" CssClass="btn btn-primary"/>
        </div>
        <div class="form-group">
            <label for="inputApiUrl">Insert API query in Quotes:</label>
            <input type="text" name="value" class="form-control" id="inputApiUrl" placeholder="example: http://marketdata.websol.barchart.com/getQuote.json?key=ab1ed28767aa90d41bdb1d112aebed99&symbols=ZC*1"/>
        </div>
        <button class="btn btn-success" onclick="sendUrl()">Insert</button>
        

        <br />
        <br />
        <hr />
        <h3>Filter by:</h3>

       <table>
           <tr>
               <td>
                   <asp:Label ID="Label8" runat="server" Text="Symbol:"></asp:Label>
                   <asp:TextBox ID="txt_symbol" runat="server"></asp:TextBox>
               </td>
               <td>
                   <asp:Label ID="Label9" runat="server" Text="Timestamp:"></asp:Label>
                   <asp:TextBox ID="txt_high" runat="server"></asp:TextBox>
               </td>
               <td>
                   <asp:Label ID="Label10" runat="server" Text="High(Min)::"></asp:Label>
                   <asp:TextBox ID="txt_low_min" runat="server"></asp:TextBox>
               </td>
               <td>
                   <asp:Label ID="Label11" runat="server" Text="High(Max):"></asp:Label>
                   <asp:TextBox ID="txt_low_max" runat="server"></asp:TextBox>
               </td>
               <td>
                   <asp:Label ID="Label12" runat="server" Text="Low(Min):"></asp:Label>
                   <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
               </td>
               <td>
                   <asp:Label ID="Label13" runat="server" Text="Low(Max):"></asp:Label>
                   <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
               </td>
               <td>
                   <asp:Button ID="Button1" runat="server" OnClick="btn_filter_Click" Text="Filter" />
               </td>
           </tr>
           <tr>

           </tr>
       </table>
        

        <br />

    </div>


    <div>
        <br />
        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
    </div>
        
    </form>

    <script>
        function sendUrl() {
            //var Myjson = { Id: 2, Name: 'arash', Price: 100 };
            var text = $("input[name = 'value']").val();
            var Myjson = { url: text };

            console.info(text);
            var xhttp = new XMLHttpRequest();
            xhttp.open("POST", "api/MyQuotes", true);
            xhttp.setRequestHeader("Content-type", " application/json");
            //xhttp.send("="+text);
            xhttp.send(JSON.stringify(Myjson));
        }

        function Navigate() {
            window.location.href = 'History.aspx';
        }
    </script>

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.0.3.min.js"></script>
</body>
</html>
