<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Intergra.Opencar.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Sincronizador Opencart</h1>
        <p>Sincroniza dados entre mysql e postgres da aplicação opencart a cada 5 horas.</p>

    </div>
    <h2>
        <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Red" Text="Falha de conexão no servidor: " Visible="False"></asp:Label>
        <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#0000CC" Text="..." Visible="False"></asp:Label>
    </h2>
    <h3>PAINEL DE ATUALIZAÇÃO OPENCART</h3>
    <address>
        ATUALIZAÇÃO VERIFICADA A CADA 5 HORAS.<br />
        .<br />
        <abbr title="">
            Proxima atualização às:
            <asp:Label ID="lblHora" runat="server" Text="00:00:00"></asp:Label></abbr>

    </address>
    <asp:Timer ID="TimerInicio" runat="server" Interval="4000" OnTick="TimerInicio_Tick" Enabled="False"></asp:Timer>

    <asp:Timer ID="Timer2" runat="server" Enabled="False">
    </asp:Timer>

    <br />
    <asp:Label ID="Label1" runat="server" Text="Resumo das Operações: "></asp:Label>
    <asp:Label ID="Label2" runat="server" Text="Em processamento.."></asp:Label>
    <asp:Panel ID="pnlBalancoComercial" runat="server">
        <div class="jumbotron">
            <ol class="round">
                <li class="one">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>

                </li>
            </ol>
        </div>
        <div class="jumbotron">
            <ol class="round">
                <li class="two">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="true" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </li>
            </ol>
        </div>

        <div class="jumbotron">
            <ol class="round">
                <li class="three">
                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="true" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </li>
            </ol>
        </div>

        <asp:Button ID="Button1" runat="server" Text="Inicia Processo" Width="375px" OnClick="Button1_Click" />
    </asp:Panel>


</asp:Content>
