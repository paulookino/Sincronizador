using Intergra.Opencar.Web.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Intergra.Opencar.Web
{
    public partial class _Default : Page
    {
        ConnMySql DadosMysql = new ConnMySql();
        ConnPostgres DadosPostgres = new ConnPostgres();

        string Mdescricao = string.Empty;
        string Mvalor = string.Empty;
        string Mean = string.Empty;
        string Mquantidade = string.Empty;
        string descricao = string.Empty;
        string valor = string.Empty;
        string ean = string.Empty;
        string quantidade = string.Empty;
        bool conectado = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime dt = DateTime.Now;

                dt.AddHours(5);
                lblHora.Text = DateTime.Now.AddHours(5).ToString();

                if(DadosPostgres.OpenConnection())
                {
                    Label4.Text = "Postgres";
                    DadosPostgres.CloseConnection();
                    conectado = true;
                }
                if (DadosMysql.OpenConnection())
                {
                    Label4.Text = "Mysql";
                    DadosMysql.CloseConnection();
                    conectado = true;
                }
                if (conectado == true)
                {
                 //   AtualizaDados();
                }
                else
                {
                    Label3.Visible = true;
                    Label4.Visible = true;
                }
            }
        }

        public void AtualizaDados()
        {
            var TblPMySql = new DataTable(); // mysql
            TblPMySql.Columns.Add("product_id", typeof(int)); //11) NOT NULL AUTO_INCREMENT,
            TblPMySql.Columns.Add("model", typeof(string)); //64) NOT NULL,
            TblPMySql.Columns.Add("sku", typeof(string)); //64) NOT NULL,
            TblPMySql.Columns.Add("upc", typeof(string)); //12) NOT NULL,
            TblPMySql.Columns.Add("ean", typeof(string)); //14) NOT NULL,
            TblPMySql.Columns.Add("jan", typeof(string)); //13) NOT NULL,
            TblPMySql.Columns.Add("isbn", typeof(string)); //17) NOT NULL,
            TblPMySql.Columns.Add("mpn", typeof(string)); //64) NOT NULL,
            TblPMySql.Columns.Add("location", typeof(string)); //128) NOT NULL,
            TblPMySql.Columns.Add("quantity", typeof(int)); //4) NOT NULL DEFAULT '0',
            TblPMySql.Columns.Add("stock_status_id", typeof(int)); //11) NOT NULL,
            TblPMySql.Columns.Add("image", typeof(string)); //255) DEFAULT NULL,
            TblPMySql.Columns.Add("manufacturer_id", typeof(int)); //11) NOT NULL,
            TblPMySql.Columns.Add("shipping", typeof(string)); //11) NOT NULL,
            TblPMySql.Columns.Add("price", typeof(decimal)); //15,4) NOT NULL DEFAULT '0.0000',
            TblPMySql.Columns.Add("points", typeof(int)); //8) NOT NULL DEFAULT '0',
            TblPMySql.Columns.Add("tax_class_id", typeof(int)); //11) NOT NULL,
            TblPMySql.Columns.Add("date_available", typeof(int)); //11) NOT NULL,
            TblPMySql.Columns.Add("weight", typeof(decimal)); //15,8) NOT NULL DEFAULT '0.00000000',
            TblPMySql.Columns.Add("weight_class_id", typeof(int)); //11) NOT NULL DEFAULT '0',
            TblPMySql.Columns.Add("length", typeof(decimal)); //15,8) NOT NULL DEFAULT '0.00000000',
            TblPMySql.Columns.Add("width", typeof(decimal)); //15,8) NOT NULL DEFAULT '0.00000000',
            TblPMySql.Columns.Add("height", typeof(decimal)); //15,8) NOT NULL DEFAULT '0.00000000',
            TblPMySql.Columns.Add("length_class_id", typeof(int)); //11) NOT NULL DEFAULT '0',
            TblPMySql.Columns.Add("subtractl_table", typeof(int)); //11) NOT NULL,
            TblPMySql.Columns.Add("minimum", typeof(int)); //11) NOT NULL DEFAULT '1',
            TblPMySql.Columns.Add("sort_order", typeof(int)); //11) NOT NULL DEFAULT '0',
            TblPMySql.Columns.Add("statusl_table", typeof(int)); //11) NOT NULL,
            TblPMySql.Columns.Add("viewed", typeof(int)); //5) NOT NULL DEFAULT '0',
            TblPMySql.Columns.Add("date_added", typeof(DateTime)); //

            var TblPostrgres = new DataTable(); // postgres
            TblPostrgres.Columns.Add("nome", typeof(string));
            TblPostrgres.Columns.Add("descricao", typeof(string));
            TblPostrgres.Columns.Add("modelo", typeof(string));
            TblPostrgres.Columns.Add("departamento", typeof(string));
            TblPostrgres.Columns.Add("preco", typeof(string));
            TblPostrgres.Columns.Add("quantidade", typeof(string));
            TblPostrgres.Columns.Add("barras", typeof(string));

            var TblPostrgresDepto = new DataTable(); // postgres
            TblPostrgresDepto.Columns.Add("cod", typeof(string));
            TblPostrgresDepto.Columns.Add("sec", typeof(string));

            var TblMySqlDepto = new DataTable(); // postgres
            TblMySqlDepto.Columns.Add("category_id", typeof(string));
            TblMySqlDepto.Columns.Add("name", typeof(string));
            TblMySqlDepto.Columns.Add("language_id", typeof(string));

            string PSql = " SELECT * from web.prd_cad where uad = 1 order by descricao ";


            // populando os Datatables
            TblPMySql = DadosMysql.RetornaDados("SELECT * FROM occe_product ");
            TblPostrgres = DadosPostgres.RetornaDados(PSql);

            TblPostrgresDepto = DadosPostgres.RetornaDadosDepto("select * from web.cad_sec ");

            TblMySqlDepto = DadosMysql.RetornaDadosDepto("SELECT * FROM occe_category_description");
            int UlID = DadosMysql.RetornaMaxId("SELECT max(category_id) as category_id FROM occe_category_description");
            UlID++;


            List<DataRow> ListM = TblPMySql.AsEnumerable().ToList();
            List<DataRow> ListP = TblPostrgres.AsEnumerable().ToList();
            List<DataRow> ListMD = TblMySqlDepto.AsEnumerable().ToList();
            List<DataRow> ListPD = TblPostrgresDepto.AsEnumerable().ToList();

            int P = TblPostrgres.Rows.Count;
            int M = TblPMySql.Rows.Count;
            int PD = TblPostrgresDepto.Rows.Count;
            int MD = TblMySqlDepto.Rows.Count;


            var TblDeptoMySqlKeys = TblMySqlDepto.Select().Select((r) => (string)r["name"]);
            var l_adDeptoRows = TblPostrgresDepto.Select().Where((r) => !TblDeptoMySqlKeys.Contains((string)r["sec"]));

            DataTable tbDeptosEncontrados = new DataTable();
            DataTable tbDeptosNovos = new DataTable();
            tbDeptosNovos.Columns.Add("Registros", typeof(String));

      
                int total = 0;
                foreach (var l_adDeptoRow in l_adDeptoRows)
                {
                    ean = l_adDeptoRow["cod"].ToString();
                    descricao = l_adDeptoRow["sec"].ToString();
                    descricao = descricao.Replace("'", "");

                    tbDeptosEncontrados = DadosMysql.RetornaDadosDepto("SELECT * FROM occe_category_description where name = '" + descricao + "' ");

                    int Registros = 0;
                    DataRow[] rows;

                    rows = tbDeptosEncontrados.Select("name = '" + descricao + "' ");
                    // tbDeptosEncontrados.Select("category_id").Max().ToString().FirstOrDefault();

                    foreach (DataRow dr in rows)
                    {
                        Mdescricao = dr["name"].ToString();
                        Mean = dr["category_id"].ToString();
                    }

                    Registros = rows.Length;
                    string num = Registros.ToString();

                    if (Registros == 0)
                    {
                        // DadosMysql.CloseConnection();
                        DadosMysql.Insert("INSERT INTO occe_category_description(category_id,language_id, name) VALUES ('" + UlID.ToString() + "','1', '" + descricao.Trim() + "'); ");
                        var Linha = "Novo Depto. Adicionado: " + l_adDeptoRow["cod"].ToString() + " - " + l_adDeptoRow["sec"].ToString();
                        tbDeptosNovos.Rows.Add(Linha);
                        total++;
                        UlID++;
                    }
                }
                GridView2.DataSource = tbDeptosNovos;
                GridView2.DataBind();

                /////////////////////////////////////////////////////////////////////////////////////////////
                var TblIMySqlKeys = TblPMySql.Select().Select((r) => (string)r["ean"]);
                var l_iguaisRows = TblPostrgres.Select().Where((r) => TblIMySqlKeys.Contains((string)r["barras"]));

                DataTable tbDadosIEncontrados = new DataTable();
                DataTable tbDadosIguais = new DataTable();

                foreach (var l_igualRow in l_iguaisRows)
                {

                    descricao = l_igualRow["descricao"].ToString();
                    ean = l_igualRow["barras"].ToString();
                    valor = l_igualRow["preco"].ToString();
                    quantidade = l_igualRow["quantidade"].ToString();

                    tbDadosIEncontrados = DadosMysql.RetornaDados("SELECT * FROM occe_product where ean = '" + ean + "' ");
                    int Registros = 0;
                    DataRow[] rows;

                    rows = tbDadosIEncontrados.Select("ean = '" + ean + "' ");

                    foreach (DataRow dr in rows)
                    {
                        Mdescricao = dr["model"].ToString();
                        Mvalor = dr["price"].ToString();
                        Mean = dr["ean"].ToString();
                        Mquantidade = dr["quantity"].ToString();
                    }

                    Registros = rows.Length;
                    string num = Registros.ToString();

                    CultureInfo enUS = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = enUS.DateTimeFormat;
                    dtfi.FullDateTimePattern = "yyyy-MM-dd 00:00:00";
                    descricao = descricao.Replace("'", "");

                    if (Registros > 0)
                    {
                        if (Mdescricao != descricao || Mvalor != valor || Mean != ean || Mquantidade != quantidade)
                        {
                            // DadosMysql.CloseConnection();
                            DadosMysql.Insert("update occe_product set model = '" + descricao + "', quantity = '" + quantidade + "', date_modified = '" + dtfi + "' where ean =  '" + ean + "' ");
                            var Linha = "Produto Atualizado: " + ean + " - " + descricao + " ";
                            // tbDadosIguais.Rows.Add(Linha);
                        }
                    }
                }

                GridView2.DataSource = tbDadosIguais;
                GridView2.DataBind();
                ///////////////////////////////////////////////////////////////////////////////////////////////

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // Find deleted rows:
                var TblPMySqlKeys = TblPostrgres.Select().Select((r) => (string)r["barras"]);
                var l_deletedRows = TblPMySql.Select().Where((r) => !TblPMySqlKeys.Contains((string)r["ean"]));

                DataTable tbDadosADExcluir = new DataTable();
                DataTable tbDadosExcluidos = new DataTable();
                tbDadosExcluidos.Columns.Add("Registros", typeof(String));

                foreach (var l_deletedRow in l_deletedRows)
                {
                    var ean = l_deletedRow["ean"].ToString();

                    tbDadosADExcluir = DadosMysql.RetornaDados("SELECT * FROM occe_product where ean = '" + ean + "' and status = 1");
                    int Registros = 0;
                    DataRow[] rows;

                    rows = tbDadosADExcluir.Select("ean = '" + ean + "' ");
                    Registros = rows.Length;
                    string num = Registros.ToString();

                    if (Registros > 0)
                    {
                        DadosMysql.CloseConnection();
                        DadosMysql.Insert("update occe_product set status = '0' where ean = '" + ean + "' ");// (product_id, model, sku, upc, ean, jan, isbn, mpn, location, quantity, stock_status_id, image, manufacturer_id, shipping, price, points, tax_class_id, date_available, weight, weight_class_id, length, width, height, length_class_id, subtract, minimum, sort_order, status, viewed, date_added, date_modified) VALUES (NULL, '"+descricao+"', '', '', '"+ean+"', '', '', '', '', '"+quantidade+"', '7', NULL, '5','1', '"+valor+ "', '0', '0', '" + DateTime.Now.ToString() + "', '0.00000000', '0', '0.00000000', '0.00000000', '0.00000000', '0', '1', '1', '0', '0', '0', '" + DateTime.Now.ToString()+"', '0000-00-00'); ");
                        var Linha = "Produto Excluido: " + l_deletedRow["ean"].ToString() + " - " + l_deletedRow["model"].ToString();
                        tbDadosExcluidos.Rows.Add(Linha);
                    }

                }

                GridView1.DataSource = tbDadosExcluidos;
                GridView1.DataBind();

                /////////////////////////////////////////////////////////////////////////////////////////////
          


            var TblPMySqlKeys1 = TblPMySql.Select().Select((r) => (string)r["ean"]);
            var l_addedRows = TblPostrgres.Select().Where((r) => !TblPMySqlKeys1.Contains((string)r["barras"]));

            DataTable tbDadosEncontrados = new DataTable();
            DataTable tbDadosNovos = new DataTable();

            tbDadosNovos.Columns.Add("Registros", typeof(String));

            foreach (var l_addedRow in l_addedRows)
            {

                descricao = l_addedRow["descricao"].ToString();
                ean = l_addedRow["barras"].ToString();
                valor = l_addedRow["preco"].ToString();
                quantidade = l_addedRow["quantidade"].ToString();

                tbDadosEncontrados = DadosMysql.RetornaDados("SELECT * FROM occe_product where ean = '" + ean + "' ");
                int Registros = 0;
                DataRow[] rows;

                rows = tbDadosEncontrados.Select("ean = '" + ean + "' ");

                foreach (DataRow dr in rows)
                {
                    Mdescricao = dr["model"].ToString();
                    Mvalor = dr["price"].ToString();
                    Mean = dr["ean"].ToString();
                    Mquantidade = dr["quantity"].ToString();
                }

                Registros = rows.Length;
                string num = Registros.ToString();
                descricao = descricao.Replace("'", "");
                if (Registros > 0 && ean != "")
                {
                    if (Mdescricao != descricao || Mvalor != valor || Mean != ean || Mquantidade != quantidade)
                    {
                        // DadosMysql.CloseConnection();
                        DadosMysql.Insert("update occe_product set model = '" + descricao + "', quantity = '" + quantidade + "', date_modified = '" + DateTime.Now.ToString() + "' ");
                        var Linha = "Produto Atualizado: " + ean + " - " + descricao + " ";
                        tbDadosNovos.Rows.Add(Linha);
                        GridView2.DataSource = tbDadosNovos;
                        GridView2.DataBind();
                    }
                }

                if (Registros == 0)
                {
                    string datahoje = Convert.ToString(DateTime.Now);
                    // DadosMysql.CloseConnection();
                    DadosMysql.Insert("INSERT INTO occe_product (product_id, model, sku, upc, ean, jan, isbn, mpn, location, quantity, stock_status_id, image, manufacturer_id, shipping, price, points, tax_class_id, date_available, weight, weight_class_id, length, width, height, length_class_id, subtract, minimum, sort_order, status, viewed, date_added, date_modified) VALUES (NULL, '" + descricao + "', '', '', '" + ean + "', '', '', '', '', '" + quantidade + "', '7', NULL, '5','1', '" + valor + "', '0', '0', '" + DateTime.Now.ToString() + "', '0.00000000', '0', '0.00000000', '0.00000000', '0.00000000', '0', '1', '1', '0', '0', '0', '" + datahoje + "', '0000-00-00'); ");
                    var Linha = "Novo Produto Adicionado: " + l_addedRow["barras"].ToString() + " - " + l_addedRow["descricao"].ToString();
                    tbDadosNovos.Rows.Add(Linha);
                    GridView2.DataSource = tbDadosNovos;
                    GridView2.DataBind();
                }
            }

            GridView2.DataSource = tbDadosNovos;
            GridView2.DataBind();
            ///////////////////////////////////////////////////////////////////////////////////////////////


            // Find modified rows:
            var l_modifiedRows = TblPostrgres.Select()
                                         .Join(
                                            TblPMySql.Select(),
                                            r => (string)r["barras"],
                                            r => (string)r["ean"],
                                            (r1, r2) => new
                                            {
                                                Row1 = r1,
                                                Row2 = r2
                                            })
                                        .Where(
                                            values => !(values.Row1["modelo"].Equals(values.Row2["model"]) &&
                                                         values.Row1["quantidade"].Equals(values.Row2["quantity"]) &&
                                                         values.Row1["preco"].Equals(values.Row2["price"]) &&
                                                         values.Row1["barras"].Equals(values.Row2["ean"])
                                                         ))
                                        .Select(values => values.Row2);

            DataTable tbDadosModificados2 = new DataTable();
            tbDadosModificados2.Columns.Add("Registros", typeof(String));

            foreach (var l_modifiedRow in l_modifiedRows)
            {
                var Linha = "Produtos Modificados: " + l_modifiedRow["ean"].ToString() + " - " + l_modifiedRow["model"].ToString();
                tbDadosModificados2.Rows.Add(Linha);
            }

            GridView3.DataSource = tbDadosModificados2;
            GridView3.DataBind();

            TimerInicio.Enabled = true;
        }


        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Label1.Text = DateTime.Now.ToString();
            DateTime dt = DateTime.Now;

            dt.AddHours(4);
            lblHora.Text = DateTime.Now.AddHours(4).ToString();
            AtualizaDados();
        }

        protected void TimerInicio_Tick(object sender, EventArgs e)
        {
            Label2.Text = "Atualizando..";
            Thread.Sleep(5000);
            TimerInicio.Enabled = false;
            if(DateTime.Now.ToString() == lblHora.Text)
            {
                DateTime dt = DateTime.Now;

                dt.AddHours(5);
                lblHora.Text = DateTime.Now.AddHours(5).ToString();

                AtualizaDados();
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AtualizaDados();
        }
    }
}