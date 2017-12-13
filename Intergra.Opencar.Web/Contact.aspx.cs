using Intergra.Opencar.Web.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Intergra.Opencar.Web
{
    public partial class Contact : Page
    {
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
            if (!this.IsPostBack)
            {
                this.BindRepeater();
            }
        }

        private void BindRepeater()
        {
            ConnMySql DadosMysql = new ConnMySql();
            ConnPostgres DadosPostgres = new ConnPostgres();

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
                        rptCustomers.DataSource = tbDadosNovos;
                        rptCustomers.DataBind();
                    }
                }

                if (Registros == 0)
                {
                    // DadosMysql.CloseConnection();
                    DadosMysql.Insert("INSERT INTO occe_product (product_id, model, sku, upc, ean, jan, isbn, mpn, location, quantity, stock_status_id, image, manufacturer_id, shipping, price, points, tax_class_id, date_available, weight, weight_class_id, length, width, height, length_class_id, subtract, minimum, sort_order, status, viewed, date_added, date_modified) VALUES (NULL, '" + descricao + "', '', '', '" + ean + "', '', '', '', '', '" + quantidade + "', '7', NULL, '5','1', '" + valor + "', '0', '0', '" + DateTime.Now.ToString() + "', '0.00000000', '0', '0.00000000', '0.00000000', '0.00000000', '0', '1', '1', '0', '0', '0', '" + DateTime.Now.ToString() + "', '0000-00-00'); ");
                    var Linha = "Novo Produto Adicionado: " + l_addedRow["barras"].ToString() + " - " + l_addedRow["descricao"].ToString();
                    tbDadosNovos.Rows.Add(Linha);
                    rptCustomers.DataSource = tbDadosEncontrados;
                    rptCustomers.DataBind();
                }
            }

            rptCustomers.DataSource = rptCustomers;
            rptCustomers.DataBind();


            }
    }
}