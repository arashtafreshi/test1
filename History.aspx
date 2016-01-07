<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="WebApplication4.History" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Get History</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div class="jumbotron">
            <h2>History</h2>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Navigate to Quote" CssClass="btn btn-primary"/>
        </div>
        <div class="form-group">
            <label for="inputApiUrl">Insert API query in History:</label>
            <input type="text" name="value" class="form-control" id="inputApiUrl" placeholder="example: http://marketdata.websol.barchart.com/getQuote.xml?key=ab1ed28767aa90d41bdb1d112aebed99&symbols=ZC*1"/>
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
                   <asp:TextBox ID="txt_Timestamp" runat="server"></asp:TextBox>
               </td>
               <td>
                   <asp:Label ID="Label10" runat="server" Text="High(Min)::"></asp:Label>
                   <asp:TextBox ID="txt_high_min" runat="server"></asp:TextBox>
               </td>
               <td>
                   <asp:Label ID="Label11" runat="server" Text="High(Max):"></asp:Label>
                   <asp:TextBox ID="txt_high_max" runat="server"></asp:TextBox>
               </td>
               <td>
                   <asp:Label ID="Label12" runat="server" Text="Low(Min):"></asp:Label>
                   <asp:TextBox ID="txt_Low_min" runat="server"></asp:TextBox>
               </td>
               <td>
                   <asp:Label ID="Label13" runat="server" Text="Low(Max):"></asp:Label>
                   <asp:TextBox ID="txt_Low_max" runat="server"></asp:TextBox>
               </td>
               <td>
                   <asp:Button ID="btn_filter" runat="server" OnClick="btn_filter_Click" Text="Filter" />
               </td>
           </tr>
           <tr>

           </tr>
       </table>
        

    </div>
        <div>
            &nbsp;&nbsp;&nbsp;
            <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
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
            xhttp.open("POST", "api/MyHistorys", true);
            xhttp.setRequestHeader("Content-type", " application/json");
            //xhttp.send("="+text);
            xhttp.send(JSON.stringify(Myjson));
        }

        function Navigate() {
            //window.location.replace("/WebForm1.aspx");
            window.location.href = 'http://localhost:62467/WebForm1.aspx';
        }
    </script>


    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.0.3.min.js"></script>
    

    
</body>
</html>
