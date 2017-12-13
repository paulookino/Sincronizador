<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Intergra.Opencar.Web.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
<asp:Repeater ID="rptCustomers" runat="server">
    <HeaderTemplate>
        <table cellspacing="0" rules="all" border="1">
            <tr>
                <th scope="col" style="width: 80px">
                    Customer Id
                </th>
                <th scope="col" style="width: 120px">
                    Customer Name
                </th>
                <th scope="col" style="width: 100px">
                    Country
                </th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td>
                <asp:Label ID="lblCustomerId" runat="server" Text='<%# Eval("ean") %>' />
            </td>
            <td>
                <asp:Label ID="lblContactName" runat="server" Text='<%# Eval("model") %>' />
            </td>
            <td>
                <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("quantity") %>' />
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
</asp:Content>
