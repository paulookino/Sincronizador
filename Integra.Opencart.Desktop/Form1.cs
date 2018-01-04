using Intergra.Opencar.Web.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Integra.Opencart.Desktop
{
    public partial class Form1 : Form
    {
        ConnMySql DadosMysql = new ConnMySql();
        ConnPostgres DadosPostgres = new ConnPostgres();

        CIniFile Ini = new CIniFile();

        private DataSet ds;

        string Mdescricao = string.Empty;
        string Mvalor = string.Empty;
        string Mean = string.Empty;
        string Mquantidade = string.Empty;
        string descricao = string.Empty;
        string valor = string.Empty;
        string ean = string.Empty;
        string quantidade = string.Empty;
        bool conectado = false;
        public int total = 0;
        string valorgeral = string.Empty;


        string variavelhora = string.Empty;
        //private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    SqlConnection conn = new SqlConnection(Conexao); // A variável conn é do tipo SqlConnection e tem como parâmetro a string de conexão.

        //    for (int i = 0; i < 30; i++) // Se este Loop for executado rapidamente aumente o número de loops.
        //    {
        //        SqlCommand cmd = new SqlCommand("INSERT INTO Funcionario (CodFunc,Nome) VALUES (" + i + ",'Teste')", conn);
        //        /* O loop vai fazer o SqlCommand inserir 30 valores no banco de dados que nós importamos, o objetivo disso é mostrar uma operação que gasta muito tempo, e que enquanto ele está executando esta ação nesta thread  em paralelo você pode chamar o Form2, porque o Form1 não esta travado.
        //        */

        //        try
        //        {
        //            conn.Open();
        //            cmd.ExecuteNonQuery();
        //        }

        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Erro" + ex.Message);
        //        }

        //        finally { conn.Close(); }
        //    }

        //}

        //private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    SqlConnection conn = new SqlConnection(Conexao);
        //    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Funcionario", conn);
        //    ds = new DataSet();
        //    da.Fill(ds); // Preencho o DataSet com o SqlDataAdapter           
        //    dataGridView1.DataSource = ds.Tables[0]; // Agora é exibido no DataGridView as informações do banco
        //}

        private void Thread_Segura()
        {
            // Thread.Sleep(2000);

        }


        private void Atualizar()
        {

            try
            {
                DateTime dt = DateTime.Now;

                dt.AddHours(5);
                
                backgroundWorker1.WorkerSupportsCancellation = true;
                
                //var variavelHoraConvertida = Convert.ToInt32(variavelhora);
                this.Invoke((MethodInvoker)delegate ()
                {
                    //lblHora.Text = DateTime.Now.AddHours(variavelHoraConvertida).ToString();

                    lblHora.Text = DateTime.Now.AddHours(5).ToString();

                });

                this.Invoke((MethodInvoker)delegate ()
                {
                    
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = 100;
                    progressBar1.Value = 1;
                    
                });


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

                var TblPostrgresFabricante = new DataTable(); // postgres
                //TblPostrgresFabricante.Columns.Add("ide", typeof(string));
                TblPostrgresFabricante.Columns.Add("cod", typeof(string));
                TblPostrgresFabricante.Columns.Add("des", typeof(string));
                TblPostrgresFabricante.Columns.Add("upd", typeof(string));
                TblPostrgresFabricante.Columns.Add("atv", typeof(string));


                var TblPostrgresCategoriaProduto = new DataTable(); // postgres
                //TblPostrgresCategoriaProduto.Columns.Add("ide", typeof(string));
                TblPostrgresCategoriaProduto.Columns.Add("cod", typeof(string));
                TblPostrgresCategoriaProduto.Columns.Add("des", typeof(string));
                TblPostrgresCategoriaProduto.Columns.Add("tpo", typeof(string));
                TblPostrgresCategoriaProduto.Columns.Add("atv", typeof(string));


                var TblPostrgresCategoria = new DataTable(); // postgres
                //TblPostrgresCategoria.Columns.Add("ide", typeof(string));
                TblPostrgresCategoria.Columns.Add("cod", typeof(string));
                TblPostrgresCategoria.Columns.Add("des", typeof(string));
                TblPostrgresCategoria.Columns.Add("sgl", typeof(string));
                TblPostrgresCategoria.Columns.Add("tme", typeof(string));
                TblPostrgresCategoria.Columns.Add("fam", typeof(string));
                TblPostrgresCategoria.Columns.Add("atv", typeof(string));



                var TblPostrgresLoja = new DataTable(); // postgres
                //TblPostrgresLoja.Columns.Add("ide", typeof(string));
                TblPostrgresLoja.Columns.Add("lot", typeof(string));
                TblPostrgresLoja.Columns.Add("uad", typeof(string));
                TblPostrgresLoja.Columns.Add("ope", typeof(string));
                TblPostrgresLoja.Columns.Add("ger", typeof(string));
                TblPostrgresLoja.Columns.Add("maq", typeof(string));
                TblPostrgresLoja.Columns.Add("dat", typeof(string));
                TblPostrgresLoja.Columns.Add("hor", typeof(string));
                TblPostrgresLoja.Columns.Add("vlc", typeof(string));
                TblPostrgresLoja.Columns.Add("mov", typeof(string));
                TblPostrgresLoja.Columns.Add("vap", typeof(string));
                TblPostrgresLoja.Columns.Add("dta", typeof(string));
                TblPostrgresLoja.Columns.Add("stt", typeof(string));
                TblPostrgresLoja.Columns.Add("cod", typeof(string));
                TblPostrgresLoja.Columns.Add("grp", typeof(string));
                TblPostrgresLoja.Columns.Add("txt", typeof(string));
                TblPostrgresLoja.Columns.Add("log", typeof(string));
                TblPostrgresLoja.Columns.Add("lg2", typeof(string));

                var TblMySqlDepto = new DataTable(); // postgres
                TblMySqlDepto.Columns.Add("category_id", typeof(string));
                TblMySqlDepto.Columns.Add("name", typeof(string));
                TblMySqlDepto.Columns.Add("language_id", typeof(string));


                var TblMySqlFabricante = new DataTable(); 
                TblMySqlFabricante.Columns.Add("manufacturer_id", typeof(string));
                TblMySqlFabricante.Columns.Add("name", typeof(string));
                TblMySqlFabricante.Columns.Add("image", typeof(string));
                TblMySqlFabricante.Columns.Add("sort_order", typeof(string));


                var TblMySqlCategoriaPath = new DataTable();
                TblMySqlCategoriaPath.Columns.Add("category_id", typeof(string));
                TblMySqlCategoriaPath.Columns.Add("path_id", typeof(string));
                TblMySqlCategoriaPath.Columns.Add("level", typeof(string));


                var TblMySqlCategoriaLayout = new DataTable();
                TblMySqlCategoriaLayout.Columns.Add("category_id", typeof(string));
                TblMySqlCategoriaLayout.Columns.Add("store_id", typeof(string));
                TblMySqlCategoriaLayout.Columns.Add("layout_id", typeof(string));


                var TblMySqlCategoriaStore = new DataTable();
                TblMySqlCategoriaStore.Columns.Add("category_id", typeof(string));
                TblMySqlCategoriaStore.Columns.Add("store_id", typeof(string));


                var TblMySqlProductCategory = new DataTable();
                TblMySqlProductCategory.Columns.Add("product_id", typeof(string));
                TblMySqlProductCategory.Columns.Add("category_id", typeof(string));


                var TblMySqlProductStore = new DataTable();
                TblMySqlProductStore.Columns.Add("product_id", typeof(string));
                TblMySqlProductStore.Columns.Add("store_id", typeof(string));


                var TblMySqlManufactureStore = new DataTable();
                TblMySqlManufactureStore.Columns.Add("manufacturer_id", typeof(string));
                TblMySqlManufactureStore.Columns.Add("store_id", typeof(string));

                this.Invoke((MethodInvoker)delegate ()
                {

                    progressBar1.Value += 5;

                });

                // populando os Datatables
                //Thread th = new Thread(new ThreadStart(this.Thread_Segura));
                //th.Start();
                valorgeral = "Carregando dados de produtos Opencart..";


                this.Invoke((MethodInvoker)delegate ()
                {

                    Lista.Items.Add(valorgeral);

                });

                //TblPMySql = DadosMysql.RetornaDados("SELECT * FROM occe_product  limit 10");
                TblPMySql = DadosMysql.RetornaDados("SELECT * FROM oc_product  limit 10");

                TblPostrgres = DadosPostgres.RetornaDados("SELECT * from web.prd_cad where uad = 1 order by descricao limit 10");

                TblPostrgresDepto = DadosPostgres.RetornaDadosDepto("select * from web.cad_sec  limit 10");

                //TblMySqlDepto = DadosMysql.RetornaDadosDepto("SELECT * FROM occe_category_description  limit 10");
                TblMySqlDepto = DadosMysql.RetornaDadosDepto("SELECT * FROM oc_category_description  limit 10");

                TblPostrgresFabricante = DadosPostgres.RetornaDadosFabricante("select * from estoque.prd_lab limit 10");
                //TblMySqlFabricante = DadosMysql.RetornaDadosFabricante("SELECT * FROM occe_manufacturer limit 10");
                TblMySqlFabricante = DadosMysql.RetornaDadosFabricante("SELECT * FROM oc_manufacturer limit 10");

                //TblMySqlCategoriaPath = DadosMysql.RetornaDadosCategoryPath("SELECT * FROM occe_category_path limit 10");
                TblMySqlCategoriaPath = DadosMysql.RetornaDadosCategoryPath("SELECT * FROM oc_category_path limit 10");
                
                //TblMySqlCategoriaLayout = DadosMysql.RetornaDadosCategoryLayout("SELECT * FROM occe_category_to_layout limit 10");
                TblMySqlCategoriaLayout = DadosMysql.RetornaDadosCategoryLayout("SELECT * FROM oc_category_to_layout limit 10");
                
                //TblMySqlCategoriaStore = DadosMysql.RetornaDadosCategoryStore("SELECT * FROM occe_category_to_store limit 10");
                TblMySqlCategoriaStore = DadosMysql.RetornaDadosCategoryStore("SELECT * FROM oc_category_to_store limit 10");
                
                //TblMySqlProductCategory = DadosMysql.RetornaDadosProductCategory("SELECT * FROM occe_product_to_category limit 10");
                TblMySqlProductCategory = DadosMysql.RetornaDadosProductCategory("SELECT * FROM oc_product_to_category limit 10");

                //TblMySqlProductStore = DadosMysql.RetornaDadosProductStore("SELECT * FROM occe_product_to_store limit 10");
                TblMySqlProductStore = DadosMysql.RetornaDadosProductStore("SELECT * FROM oc_product_to_store limit 10");

                //TblMySqlManufactureStore = DadosMysql.RetornaDadosManufacturerStore("SELECT * FROM occe_manufacturer_to_store limit 10");
                TblMySqlManufactureStore = DadosMysql.RetornaDadosManufacturerStore("SELECT * FROM oc_manufacturer_to_store limit 10");

                //int UlID = DadosMysql.RetornaMaxId("SELECT max(category_id) as category_id FROM occe_category_description");
                int UlID = DadosMysql.RetornaMaxId("SELECT max(category_id) as category_id FROM oc_category_description");

                //int UlIP = DadosMysql.RetornaMaxId("SELECT max(product_id) as product_id FROM occe_product");
                int UlIP = DadosMysql.RetornaMaxId("SELECT max(product_id) as product_id FROM oc_product");
                
                //int manufacturerId = DadosMysql.RetornaMaxId("SELECT max(manufacturer_id) as manufacturer_id FROM occe_manufacturer");
                int manufacturerId = DadosMysql.RetornaMaxId("SELECT max(manufacturer_id) as manufacturer_id FROM oc_manufacturer");



                UlID++;
                UlIP++;
                manufacturerId++;


                List<DataRow> ListM = TblPMySql.AsEnumerable().ToList();
                List<DataRow> ListP = TblPostrgres.AsEnumerable().ToList();
                List<DataRow> ListMD = TblMySqlDepto.AsEnumerable().ToList();
                List<DataRow> ListPD = TblPostrgresDepto.AsEnumerable().ToList();

                List<DataRow> ListManufacture = TblMySqlManufactureStore.AsEnumerable().ToList();
                List<DataRow> ListCategoriaPath = TblMySqlCategoriaPath.AsEnumerable().ToList();
                List<DataRow> ListCategoriaCategoriaLayout = TblMySqlCategoriaLayout.AsEnumerable().ToList();
                List<DataRow> ListCategoriaCategoriaStore = TblMySqlCategoriaStore.AsEnumerable().ToList();
                List<DataRow> ListProductCategory = TblMySqlProductCategory.AsEnumerable().ToList();
                List<DataRow> ListProductStore = TblMySqlProductStore.AsEnumerable().ToList();
                List<DataRow> ListManufactureStore = TblMySqlManufactureStore.AsEnumerable().ToList();

                int P = TblPostrgres.Rows.Count;
                int M = TblPMySql.Rows.Count;
                int PD = TblPostrgresDepto.Rows.Count;
                int MD = TblMySqlDepto.Rows.Count;
                
                var TblDeptoMySqlKeys = TblMySqlDepto.Select().Select((r) => (string)r["name"]);
                var TblFabricanteMySqlKeys = TblMySqlFabricante.Select().Select((r) => (string)r["name"]);
                var TblCategoriaPathMySqlKeys = TblMySqlCategoriaPath.Select().Select((r) => (string)r["category_id"]);
                var TblCategoriaLayoutMySqlKeys = TblMySqlCategoriaLayout.Select().Select((r) => (string)r["category_id"]);
                var TblCategoriaStoreMySqlKeys = TblMySqlCategoriaStore.Select().Select((r) => (string)r["category_id"]);
                var TblProductCategoryMySqlKeys = TblMySqlProductCategory.Select().Select((r) => (string)r["product_id"]);
                var TblProductStoreMySqlKeys = TblMySqlProductStore.Select().Select((r) => (string)r["product_id"]);
                var TblManufactureStoreMySqlKeys = TblMySqlManufactureStore.Select().Select((r) => (string)r["manufacturer_id"]);
                
                var l_adDeptoRows = TblPostrgresDepto.Select().Where((r) => !TblDeptoMySqlKeys.Contains((string)r["sec"]));
                var l_adFabricanteRows = TblPostrgresDepto.Select().Where((r) => !TblFabricanteMySqlKeys.Contains((string)r["des"]));
                var l_adCategoriaPathRows = TblMySqlCategoriaPath.Select().Where((r) => !TblCategoriaPathMySqlKeys.Contains((string)r["category_id"]));
                var l_adCategoriaLayoutRows = TblMySqlCategoriaLayout.Select().Where((r) => !TblCategoriaLayoutMySqlKeys.Contains((string)r["category_id"]));
                var l_adCategoriaStoreRows = TblMySqlCategoriaStore.Select().Where((r) => !TblCategoriaStoreMySqlKeys.Contains((string)r["category_id"]));
                var l_adProductCategoryRows = TblMySqlProductCategory.Select().Where((r) => !TblProductCategoryMySqlKeys.Contains((string)r["product_id"]));
                var l_adProductStoreRows = TblMySqlProductStore.Select().Where((r) => !TblProductStoreMySqlKeys.Contains((string)r["product_id"]));
                var l_adManufacturerStoreRows = TblMySqlManufactureStore.Select().Where((r) => !TblManufactureStoreMySqlKeys.Contains((string)r["manufacturer_id"]));


                #region Inseri Manufacturer
                DataTable tbFabricantesEncontrados = new DataTable();
                DataTable tbFabricantesNovos = new DataTable();
                tbFabricantesNovos.Columns.Add("Registros", typeof(String));

                total = 0;

                foreach (var l_adFabricanteRow in l_adFabricanteRows)
                {

                    ean = l_adFabricanteRow["cod"].ToString();
                    descricao = l_adFabricanteRow["des"].ToString();
                    descricao = descricao.Replace("'", "");
                    descricao = descricao.Trim();

                    //tbFabricantesEncontrados = DadosMysql.RetornaDadosFabricante("SELECT * FROM occe_manufacturer where name = '" + descricao + "' ");
                    tbFabricantesEncontrados = DadosMysql.RetornaDadosFabricante("SELECT * FROM oc_manufacturer where name = '" + descricao + "' ");

                    int Registros = 0;
                    DataRow[] rows;

                    rows = tbFabricantesEncontrados.Select("name = '" + descricao + "' ");
                    // tbDeptosEncontrados.Select("category_id").Max().ToString().FirstOrDefault();

                    foreach (DataRow dr in rows)
                    {
                        Mdescricao = dr["name"].ToString();
                        Mean = dr["manufacturer_id"].ToString();
                    }
                    
                    Registros = rows.Length;
                    string num = Registros.ToString();

                    DadosMysql.CloseConnection();
                    
                    if (Registros == 0)
                    {
                        // DadosMysql.CloseConnection();

                        //DadosMysql.Insert("INSERT INTO occe_manufacturer (manufacturer_id, name, image, sort_order) VALUES ('" + manufacturerId.ToString() + "',' " + descricao.Trim() + " ', 'catalog / demo / htc_logo.jpg' , '0'); ");
                        DadosMysql.Insert("INSERT INTO oc_manufacturer (manufacturer_id, name, image, sort_order) VALUES ('" + manufacturerId.ToString() + "',' " + descricao.Trim() + " ', 'catalog / demo / htc_logo.jpg' , '0'); ");
                        var Linha = "Novo Fabricante Adicionado: " + l_adFabricanteRow["cod"].ToString() + " - " + l_adFabricanteRow["des"].ToString();
                        tbFabricantesNovos.Rows.Add(Linha);
                        this.Invoke((MethodInvoker)delegate ()
                        {

                            Lista.Items.Add(Linha);

                        });
                        //Thread.Sleep(1000);
                        //      DadosMysql.Insert("INSERT INTO occe_category(`category_id`, `image`, `parent_id`, `top`, `column`, `sort_order`, `status`, `date_added`, `date_modified`) VALUES ('" + UlID.ToString() + "','catalog/DEPARTAMENTOS/DNW_HIGB_00.png','0','3','3','1','2017-12-11 19:48:07','2017-12-11 19:48:07'); ");

                        manufacturerId++;

                    }
                    total++;

                    valorgeral = total.ToString();

                }
                #endregion

                #region Inseri Categoria Path
                DataTable tbCategoryPathEncontrados = new DataTable();
                DataTable tbCategoryPathNovos = new DataTable();
                tbCategoryPathNovos.Columns.Add("Registros", typeof(String));

                total = 0;

                foreach (var l_adCategoriaPathRow in l_adCategoriaPathRows)
                {

                    string categoryId = l_adCategoriaPathRow["category_id"].ToString();
                    string pathId = l_adCategoriaPathRow["path_id"].ToString();
                    string level = l_adCategoriaPathRow["level"].ToString();


                    //tbCategoryPathEncontrados = DadosMysql.RetornaDadosCategoryPath("SELECT * FROM occe_category_path where category_id = '" + descricao + "' and path_id = '"+ descricao +"' ");
                    tbCategoryPathEncontrados = DadosMysql.RetornaDadosCategoryPath("SELECT * FROM oc_category_path where category_id = '" + categoryId + "' and path_id = '"+ pathId +"' ");

                    int Registros = 0;
                    DataRow[] rows;

                    rows = tbCategoryPathEncontrados.Select("category_id = '" + categoryId + "' and path_id = '" + pathId + "'");
                    // tbDeptosEncontrados.Select("category_id").Max().ToString().FirstOrDefault();

                    foreach (DataRow dr in rows)
                    {
                        Mdescricao = dr["category_id"].ToString();
                        Mdescricao = dr["path_id"].ToString();
                        Mean = dr["level"].ToString();
                    }

                    Registros = rows.Length;
                    string num = Registros.ToString();

                    DadosMysql.CloseConnection();
                    
                    if (Registros == 0)
                    {
                        // DadosMysql.CloseConnection();

                        //DadosMysql.Insert("INSERT INTO occe_category_path (category_id, path_id, level) VALUES ('" + manufacturerId.ToString() + "',' " + manufacturerId.ToString() + " ',' "+ manufacturerId.ToString() +"); ");
                        DadosMysql.Insert("INSERT INTO oc_category_path (category_id, path_id, level) VALUES (1 , 2, 3); ");

                        //INSERT INTO `occe_category_path` (`category_id`, `path_id`, `level`) VALUES (5, 101, 0) (5, 102, 0)
                        var Linha = "Novo Categoria Path Adicionado: " + l_adCategoriaPathRow["category_id"].ToString() + " - " + l_adCategoriaPathRow["path_id"].ToString();
                        tbCategoryPathNovos.Rows.Add(Linha);
                        this.Invoke((MethodInvoker)delegate ()
                        {

                            Lista.Items.Add(Linha);

                        });
                        //Thread.Sleep(1000);
                        //      DadosMysql.Insert("INSERT INTO occe_category(`category_id`, `image`, `parent_id`, `top`, `column`, `sort_order`, `status`, `date_added`, `date_modified`) VALUES ('" + UlID.ToString() + "','catalog/DEPARTAMENTOS/DNW_HIGB_00.png','0','3','3','1','2017-12-11 19:48:07','2017-12-11 19:48:07'); ");

                       // manufacturerId++;

                    }
                    total++;

                    valorgeral = total.ToString();

                }
                #endregion

                #region Inseri Categoria Layout
                DataTable tbCategoryLayoutEncontrados = new DataTable();
                DataTable tbCategoryLayoutNovos = new DataTable();
                tbCategoryLayoutNovos.Columns.Add("Registros", typeof(String));

                total = 0;

                foreach (var l_adCategoriaLayoutRow in l_adCategoriaLayoutRows)
                {

                    string categoryId = l_adCategoriaLayoutRow["category_Id"].ToString();
                    string storeId = l_adCategoriaLayoutRow["store_id"].ToString();
                    string layoutId = l_adCategoriaLayoutRow["layout_id"].ToString();

                    //tbCategoryLayoutEncontrados = DadosMysql.RetornaDadosCategoryLayout("SELECT * FROM occe_category_to_layout where category_id = '" + descricao + "' and store_id = '" + descricao + "' and layout_id = '" + descricao + "' ");
                    tbCategoryLayoutEncontrados = DadosMysql.RetornaDadosCategoryLayout("SELECT * FROM oc_category_to_layout where category_id = '" + categoryId + "' and store_id = '" + storeId + "' and layout_id = '" + layoutId + "' ");

                    int Registros = 0;
                    DataRow[] rows;

                    rows = tbCategoryPathEncontrados.Select("category_id = '" + categoryId + "' and store_id = '" + storeId + "' and layout_id = '" + layoutId + "'");
                    // tbDeptosEncontrados.Select("category_id").Max().ToString().FirstOrDefault();

                    foreach (DataRow dr in rows)
                    {
                        categoryId = dr["category_id"].ToString();
                        storeId = dr["store_id"].ToString();
                        layoutId = dr["layout_id"].ToString();
                    }

                    Registros = rows.Length;
                    string num = Registros.ToString();

                    DadosMysql.CloseConnection();



                    if (Registros == 0)
                    {
                        // DadosMysql.CloseConnection();

                        //DadosMysql.Insert("INSERT INTO occe_category_to_layout (category_id, store_id, layout_id) VALUES ('" + manufacturerId.ToString() + "',' " + manufacturerId.ToString() + " ',' " + manufacturerId.ToString() + "); ");
                        DadosMysql.Insert("INSERT INTO oc_category_to_layout (category_id, store_id, layout_id) VALUES (1, 2, 3); ");
                        

                        //INSERT INTO `occe_category_path` (`category_id`, `path_id`, `level`) VALUES (5, 101, 0) (5, 102, 0)
                        var Linha = "Novo Categoria Layout Adicionado: " + l_adCategoriaLayoutRow["category_id"].ToString() + " - " + l_adCategoriaLayoutRow["store_id"].ToString();
                        tbCategoryLayoutNovos.Rows.Add(Linha);
                        this.Invoke((MethodInvoker)delegate ()
                        {

                            Lista.Items.Add(Linha);

                        });
                        //Thread.Sleep(1000);
                        //      DadosMysql.Insert("INSERT INTO occe_category(`category_id`, `image`, `parent_id`, `top`, `column`, `sort_order`, `status`, `date_added`, `date_modified`) VALUES ('" + UlID.ToString() + "','catalog/DEPARTAMENTOS/DNW_HIGB_00.png','0','3','3','1','2017-12-11 19:48:07','2017-12-11 19:48:07'); ");

                        // manufacturerId++;

                    }
                    total++;

                    valorgeral = total.ToString();

                }
                #endregion
                
                #region Inseri Categoria Store
                DataTable tbCategoryStoreEncontrados = new DataTable();
                DataTable tbCategoryStoretNovos = new DataTable();
                tbCategoryStoretNovos.Columns.Add("Registros", typeof(String));

                total = 0;

                foreach (var l_adCategoriaStoreRow in l_adCategoriaStoreRows)
                {

                    string categoryId = l_adCategoriaStoreRow["category_id"].ToString();
                    string storeId = l_adCategoriaStoreRow["store_id"].ToString();

                    //tbCategoryStoreEncontrados = DadosMysql.RetornaDadosCategoryStore("SELECT * FROM occe_category_to_store where category_id = '" + descricao + "' and store_id = '" + descricao + "'");
                    tbCategoryStoreEncontrados = DadosMysql.RetornaDadosCategoryStore("SELECT * FROM oc_category_to_store where category_id = '" + categoryId + "' and store_id = '" + storeId + "'");

                    int Registros = 0;
                    DataRow[] rows;

                    rows = tbCategoryPathEncontrados.Select("category_id = '" + categoryId + "' and store_id = '" + storeId + "'");
                    // tbDeptosEncontrados.Select("category_id").Max().ToString().FirstOrDefault();

                    foreach (DataRow dr in rows)
                    {
                        Mdescricao = dr["category_id"].ToString();
                        Mdescricao = dr["store_id"].ToString();
                    }

                    Registros = rows.Length;
                    string num = Registros.ToString();

                    DadosMysql.CloseConnection();
                    
                    if (Registros == 0)
                    {
                        // DadosMysql.CloseConnection();

                        //DadosMysql.Insert("INSERT INTO occe_category_to_store (category_id, store_id) VALUES ('" + manufacturerId.ToString() + "',' " + manufacturerId.ToString() + "); ");
                        DadosMysql.Insert("INSERT INTO oc_category_to_store (category_id, store_id) VALUES (1 , 2); ");

                        //INSERT INTO `occe_category_to_store` (`category_id`, `store_id`) VALUES (5, 0)  
                        
                        var Linha = "Novo Categoria Store Adicionado: " + l_adCategoriaStoreRow["category_id"].ToString() + " - " + l_adCategoriaStoreRow["store_id"].ToString();
                        tbCategoryStoretNovos.Rows.Add(Linha);
                        this.Invoke((MethodInvoker)delegate ()
                        {

                            Lista.Items.Add(Linha);

                        });
                        //Thread.Sleep(1000);
                        //      DadosMysql.Insert("INSERT INTO occe_category(`category_id`, `image`, `parent_id`, `top`, `column`, `sort_order`, `status`, `date_added`, `date_modified`) VALUES ('" + UlID.ToString() + "','catalog/DEPARTAMENTOS/DNW_HIGB_00.png','0','3','3','1','2017-12-11 19:48:07','2017-12-11 19:48:07'); ");

                        // manufacturerId++;

                    }
                    total++;

                    valorgeral = total.ToString();

                }
                #endregion

                #region Inseri Product Category
                DataTable tbProductCategoryEncontrados = new DataTable();
                DataTable tbProductCategoryNovos = new DataTable();
                tbProductCategoryNovos.Columns.Add("Registros", typeof(String));

                total = 0;

                foreach (var l_adProductCategoryRow in l_adProductCategoryRows)
                {

                    string productId = l_adProductCategoryRow["product_id"].ToString();
                    string categoryId = l_adProductCategoryRow["category_id"].ToString();
                    
                    //tbProductCategoryEncontrados = DadosMysql.RetornaDadosProductCategory("SELECT * FROM occe_product_to_category where product_id = '" + descricao + "' and category_id = '" + descricao + "'");
                    tbProductCategoryEncontrados = DadosMysql.RetornaDadosProductCategory("SELECT * FROM oc_product_to_category where product_id = '" + productId + "' and category_id = '" + categoryId + "'");

                    int Registros = 0;
                    DataRow[] rows;

                    rows = tbProductCategoryEncontrados.Select("product_id = '" + productId + "' and category_id = '" + categoryId + "'");
                    // tbDeptosEncontrados.Select("category_id").Max().ToString().FirstOrDefault();

                    foreach (DataRow dr in rows)
                    {
                        Mdescricao = dr["product_id"].ToString();
                        Mdescricao = dr["category_id"].ToString();
                    }

                    Registros = rows.Length;
                    string num = Registros.ToString();

                    DadosMysql.CloseConnection();



                    if (Registros == 0)
                    {
                        // DadosMysql.CloseConnection();

                        //DadosMysql.Insert("INSERT INTO occe_product_to_category (product_id, category_id) VALUES ('" + manufacturerId.ToString() + "',' " + manufacturerId.ToString() + "); ");
                        DadosMysql.Insert("INSERT INTO oc_product_to_category (product_id, category_id) VALUES (1 , 2); ");
                        //INSERT INTO `occe_product_to_category` (`product_id`, `category_id`) VALUES (101, 5)
                        

                        var Linha = "Novo Produto Categoria Adicionado: " + l_adProductCategoryRow["product_id"].ToString() + " - " + l_adProductCategoryRow["category_id"].ToString();
                        tbProductCategoryNovos.Rows.Add(Linha);
                        this.Invoke((MethodInvoker)delegate ()
                        {

                            Lista.Items.Add(Linha);

                        });
                        //Thread.Sleep(1000);
                        //      DadosMysql.Insert("INSERT INTO occe_category(`category_id`, `image`, `parent_id`, `top`, `column`, `sort_order`, `status`, `date_added`, `date_modified`) VALUES ('" + UlID.ToString() + "','catalog/DEPARTAMENTOS/DNW_HIGB_00.png','0','3','3','1','2017-12-11 19:48:07','2017-12-11 19:48:07'); ");

                        // manufacturerId++;

                    }
                    total++;

                    valorgeral = total.ToString();

                }
                #endregion

                #region Inseri Product Store
                DataTable tbProductStoreEncontrados = new DataTable();
                DataTable tbProductStoreNovos = new DataTable();
                tbProductStoreNovos.Columns.Add("Registros", typeof(String));

                total = 0;

                foreach (var l_adProductStoreRow in l_adProductStoreRows)
                {

                    string productId = l_adProductStoreRow["product_id"].ToString();
                    string storeId = l_adProductStoreRow["store_id"].ToString();
                    

                    //tbProductStoreEncontrados = DadosMysql.RetornaDadosProductStore("SELECT * FROM occe_product_to_store where product_id = '" + descricao + "' and store_id = '" + descricao + "'");
                    tbProductStoreEncontrados = DadosMysql.RetornaDadosProductStore("SELECT * FROM oc_product_to_store where product_id = '" + productId + "' and store_id = '" + storeId + "'");

                    int Registros = 0;
                    DataRow[] rows;

                    rows = tbProductStoreEncontrados.Select("product_id = '" + productId + "' and store_id = '" + storeId + "'");
                    // tbDeptosEncontrados.Select("category_id").Max().ToString().FirstOrDefault();

                    foreach (DataRow dr in rows)
                    {
                        Mdescricao = dr["product_id"].ToString();
                        Mdescricao = dr["store_id"].ToString();
                    }

                    Registros = rows.Length;
                    string num = Registros.ToString();

                    DadosMysql.CloseConnection();
                    
                    if (Registros == 0)
                    {
                        // DadosMysql.CloseConnection();

                        //DadosMysql.Insert("INSERT INTO occe_product_to_store  (product_id, store_id) VALUES ('" + manufacturerId.ToString() + "',' " + manufacturerId.ToString() + "); ");
                        DadosMysql.Insert("INSERT INTO oc_product_to_store  (product_id, store_id) VALUES (1, 2); ");
                        //INSERT INTO `occe_product_to_store` (`product_id`, `store_id`) VALUES (101, 0)

                        var Linha = "Novo Produto Store Adicionado: " + l_adProductStoreRow["product_id"].ToString() + " - " + l_adProductStoreRow["store_id"].ToString();
                        tbProductStoreNovos.Rows.Add(Linha);
                        this.Invoke((MethodInvoker)delegate ()
                        {

                            Lista.Items.Add(Linha);

                        });
                        //Thread.Sleep(1000);
                        //      DadosMysql.Insert("INSERT INTO occe_category(`category_id`, `image`, `parent_id`, `top`, `column`, `sort_order`, `status`, `date_added`, `date_modified`) VALUES ('" + UlID.ToString() + "','catalog/DEPARTAMENTOS/DNW_HIGB_00.png','0','3','3','1','2017-12-11 19:48:07','2017-12-11 19:48:07'); ");

                        // manufacturerId++;

                    }
                    total++;

                    valorgeral = total.ToString();

                }
                #endregion

                #region Inseri Manufacturer Store
                DataTable tbManufacturerStoreEncontrados = new DataTable();
                DataTable tbManufacturerStoreNovos = new DataTable();
                tbManufacturerStoreNovos.Columns.Add("Registros", typeof(String));

                total = 0;

                foreach (var l_adManufacturerStoreRow in l_adManufacturerStoreRows)
                {

                    string manufacturerIdRow = l_adManufacturerStoreRow["manufacturer_id"].ToString();
                    string storeId = l_adManufacturerStoreRow["store_id"].ToString();

                    //tbManufacturerStoreEncontrados = DadosMysql.RetornaDadosManufacturerStore("SELECT * FROM occe_manufacturer_to_store where manufacturer_id = '" + descricao + "' and store_id = '" + descricao + "'");
                    tbManufacturerStoreEncontrados = DadosMysql.RetornaDadosManufacturerStore("SELECT * FROM oc_manufacturer_to_store where manufacturer_id = '" + manufacturerIdRow + "' and store_id = '" + storeId + "'");

                    int Registros = 0;
                    DataRow[] rows;

                    rows = tbManufacturerStoreEncontrados.Select("manufacturer_id = '" + manufacturerIdRow + "' and store_id = '" + storeId + "'");
                    // tbDeptosEncontrados.Select("category_id").Max().ToString().FirstOrDefault();

                    foreach (DataRow dr in rows)
                    {
                        Mdescricao = dr["manufacturer_id"].ToString();
                        Mdescricao = dr["store_id"].ToString();
                    }

                    Registros = rows.Length;
                    string num = Registros.ToString();

                    DadosMysql.CloseConnection();
                    
                    if (Registros == 0)
                    {
                        // DadosMysql.CloseConnection();

                        //DadosMysql.Insert("INSERT INTO occe_manufacturer_to_store  (manufacturer_id, store_id) VALUES ('" + manufacturerId.ToString() + "',' " + manufacturerId.ToString() + "); ");
                        DadosMysql.Insert("INSERT INTO oc_manufacturer_to_store  (manufacturer_id, store_id) VALUES (1, 2); ");
                        //INSERT INTO `occe_manufacturer_to_store` (`manufacturer_id`, `store_id`) VALUES (0001, 0)

                        var Linha = "Novo Fabricante Store Adicionado: " + l_adManufacturerStoreRow["manufacturer_id"].ToString() + " - " + l_adManufacturerStoreRow["store_id"].ToString();
                        tbProductStoreNovos.Rows.Add(Linha);
                        this.Invoke((MethodInvoker)delegate ()
                        {

                            Lista.Items.Add(Linha);

                        });
                        //Thread.Sleep(1000);
                        //      DadosMysql.Insert("INSERT INTO occe_category(`category_id`, `image`, `parent_id`, `top`, `column`, `sort_order`, `status`, `date_added`, `date_modified`) VALUES ('" + UlID.ToString() + "','catalog/DEPARTAMENTOS/DNW_HIGB_00.png','0','3','3','1','2017-12-11 19:48:07','2017-12-11 19:48:07'); ");

                        // manufacturerId++;

                    }
                    total++;

                    valorgeral = total.ToString();

                }
                #endregion

                #region Altera Manufacturer


                var TblMySqlFabricanteKeys = TblMySqlFabricante.Select().Select((r) => (string)r["manufactuer_id"]);
                var l_FabricanteiguaisRows = TblPostrgres.Select().Where((r) => TblMySqlFabricanteKeys.Contains((string)r["des"]));


                //this.Invoke((MethodInvoker)delegate ()
                //{
                //    progressBar1.Maximum = l_iguaisRows.Count();
                //});

                //progressBar1.Minimum = 0;

                DataTable tbDadosFabricantesEncontrados = new DataTable();
                DataTable tbDadosFabricantesIguais = new DataTable();

                total = 0;
                valorgeral = total.ToString();
                
                foreach (var l_FabricanteiguaisRow in l_FabricanteiguaisRows)
                {

                    descricao = l_FabricanteiguaisRow["des"].ToString();
                    ean = l_FabricanteiguaisRow["cod"].ToString();
                    valor = l_FabricanteiguaisRow["preco"].ToString();
                    quantidade = l_FabricanteiguaisRow["quantidade"].ToString();

                    //tbDadosFabricantesEncontrados = DadosMysql.RetornaDadosFabricante("SELECT * FROM occe_manufacturer where manufacturer_id = '" + ean + "' ");
                    tbDadosFabricantesEncontrados = DadosMysql.RetornaDadosFabricante("SELECT * FROM oc_manufacturer where name = '" + descricao + "' ");
                    int Registros = 0;
                    DataRow[] rows;

                    rows = tbDadosFabricantesEncontrados.Select("manufacturer_id = '" + ean + "'");

                    foreach (DataRow dr in rows)
                    {
                        Mdescricao = dr["name"].ToString();
                        Mean = dr["manufacturer_id"].ToString();
                    }

                    Registros = rows.Length;
                    string num = Registros.ToString();

                    CultureInfo enUS = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = enUS.DateTimeFormat;
                    dtfi.FullDateTimePattern = "yyyy-MM-dd 00:00:00";
                    descricao = descricao.Replace("'", "");
                    
                    if (Registros > 0)
                    {
                        if (Mdescricao != descricao || Mean != ean )
                        {
                            DadosMysql.CloseConnection();
                            //DadosMysql.Insert("update occe_product set model = '" + descricao + "', quantity = '" + quantidade + "', date_modified = '" + dtfi + "' where ean =  '" + ean + "' ");
                            DadosMysql.Insert("update occe_manufacturer set name = '" + descricao + "', image = 'img', sort_order = 0 where name =  '" + descricao + "' ");
                            var Linha = "Fabricante Atualizado: " + ean + " - " + descricao + " ";
                            tbDadosFabricantesIguais.Rows.Add(Linha);

                            this.Invoke((MethodInvoker)delegate ()
                            {

                                Lista.Items.Add(Linha);

                            });
                        }
                    }
                    total++;
                    valorgeral = total.ToString();


                    //  progressBar1.Step = total;
                }

                #endregion

                #region Altera Category Path


                var TblMySqlCategoryPathKeys = TblMySqlCategoriaPath.Select().Select((r) => (string)r["category_id"]);
                var l_CategoryPathIguaisRows = TblPostrgres.Select().Where((r) => TblMySqlFabricanteKeys.Contains((string)r["barras"]));


                //this.Invoke((MethodInvoker)delegate ()
                //{
                //    progressBar1.Maximum = l_iguaisRows.Count();
                //});

                //progressBar1.Minimum = 0;

                DataTable tbDadosCategoryPathEncontrados = new DataTable();
                DataTable tbDadosCategoryPathIguais = new DataTable();

                total = 0;
                valorgeral = total.ToString();

                foreach (var l_igualRow in l_CategoryPathIguaisRows)
                {

                    descricao = l_igualRow["descricao"].ToString();
                    ean = l_igualRow["barras"].ToString();
                    valor = l_igualRow["preco"].ToString();
                    quantidade = l_igualRow["quantidade"].ToString();

                    //tbDadosCategoryPathEncontrados = DadosMysql.RetornaDadosCategoryPath("SELECT * FROM occe_category_path where category_id = '" + ean + "' and path_id = '" + ean + "' ");
                    tbDadosCategoryPathEncontrados = DadosMysql.RetornaDadosCategoryPath("SELECT * FROM oc_category_path where category_id = '" + ean + "' and path_id = '" + ean + "' ");
                    int Registros = 0;
                    DataRow[] rows;

                    rows = tbDadosFabricantesEncontrados.Select("category_id = '" + ean + "' and path_id = '" + ean + "'");

                    foreach (DataRow dr in rows)
                    {
                        Mdescricao = dr["category_id"].ToString();
                        Mvalor = dr["path_id"].ToString();
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
                            DadosMysql.CloseConnection();
                            //DadosMysql.Insert("update occe_category_path set category_id = '" + descricao + "', path_id = '" + quantidade + "', level = '" + dtfi + "' where category_id =  '" + ean + "' ");
                            DadosMysql.Insert("update oc_category_path set category_id = '" + descricao + "', path_id = '" + quantidade + "', level = '" + dtfi + "' where category_id =  '" + ean + "' ");
                            var Linha = "Categoria Path Atualizado: " + ean + " - " + descricao + " ";
                            tbDadosCategoryPathIguais.Rows.Add(Linha);

                            this.Invoke((MethodInvoker)delegate ()
                            {

                                Lista.Items.Add(Linha);

                            });
                        }
                    }
                    total++;
                    valorgeral = total.ToString();


                    //  progressBar1.Step = total;
                }

                #endregion
                
                #region Inseri Departamento
                DataTable tbDeptosEncontrados = new DataTable();
                DataTable tbDeptosNovos = new DataTable();
                tbDeptosNovos.Columns.Add("Registros", typeof(String));


                total = 0;

                foreach (var l_adDeptoRow in l_adDeptoRows)
                {

                    ean = l_adDeptoRow["cod"].ToString();
                    descricao = l_adDeptoRow["sec"].ToString();
                    descricao = descricao.Replace("'", "");
                    descricao = descricao.Trim();

                    //tbDeptosEncontrados = DadosMysql.RetornaDadosDepto("SELECT * FROM occe_category_description where name = '" + descricao + "' ");
                    tbDeptosEncontrados = DadosMysql.RetornaDadosDepto("SELECT * FROM oc_category_description where name = '" + descricao + "' ");

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

                    DadosMysql.CloseConnection();



                    if (Registros == 0)
                    {
                        // DadosMysql.CloseConnection();

                        //DadosMysql.Insert("INSERT INTO occe_category_description(category_id,language_id, name) VALUES ('" + UlID.ToString() + "','1', '" + descricao.Trim() + "'); ");


                        DadosMysql.Insert("INSERT INTO oc_category_description(category_id,language_id, name) VALUES ('" + UlID.ToString() + "','1', '" + descricao.Trim() + "'); ");

                        var Linha = "Novo Depto. Adicionado: " + l_adDeptoRow["cod"].ToString() + " - " + l_adDeptoRow["sec"].ToString();
                        tbDeptosNovos.Rows.Add(Linha);
                        this.Invoke((MethodInvoker)delegate ()
                        {

                            Lista.Items.Add(Linha);

                        });
                        //Thread.Sleep(1000);
                        //      DadosMysql.Insert("INSERT INTO occe_category(`category_id`, `image`, `parent_id`, `top`, `column`, `sort_order`, `status`, `date_added`, `date_modified`) VALUES ('" + UlID.ToString() + "','catalog/DEPARTAMENTOS/DNW_HIGB_00.png','0','3','3','1','2017-12-11 19:48:07','2017-12-11 19:48:07'); ");

                        UlID++;

                    }
                    total++;

                    valorgeral = total.ToString();

                }
                #endregion
                this.Invoke((MethodInvoker)delegate ()
                {

                    progressBar1.Value += 10;

                });
                #region Inseri Departamento 2

                var TblDeptoMySqlKeys2 = TblMySqlDepto.Select().Select((r) => (string)r["category_id"]);
                var l_adDeptoRows2 = TblMySqlDepto.Select().Select((r) => !TblDeptoMySqlKeys.Contains((string)r["sec"]));

                ////////////// INSERINDO CATEGORIA ///////////////////////////////////////////////////////////////////////////////

                DataTable tbDeptosEncontrados2 = new DataTable();
                DataTable tbDeptosNovos2 = new DataTable();
                tbDeptosNovos2.Columns.Add("Registros", typeof(String));

                total = 0;

                foreach (var l_adDeptoRow in l_adDeptoRows)
                {
                    ean = l_adDeptoRow["cod"].ToString();
                    descricao = l_adDeptoRow["sec"].ToString();
                    descricao = descricao.Replace("'", "");
                    descricao = descricao.Trim();

                    //tbDeptosEncontrados2 = DadosMysql.RetornaDadosDepto("SELECT * FROM occe_category_description where name = '" + descricao + "' ");
                    tbDeptosEncontrados2 = DadosMysql.RetornaDadosDepto("SELECT * FROM oc_category_description where name = '" + descricao + "' ");

                    int Registros = 0;
                    DataRow[] rows;

                    rows = tbDeptosEncontrados2.Select("name = '" + descricao + "' ");
                    // tbDeptosEncontrados.Select("category_id").Max().ToString().FirstOrDefault();

                    foreach (DataRow dr in rows)
                    {
                        Mdescricao = dr["name"].ToString();
                        Mean = dr["category_id"].ToString();
                    }

                    Registros = rows.Length;
                    string num = Registros.ToString();

                    DadosMysql.CloseConnection();



                    if (Registros == 0)
                    {
                        // DadosMysql.CloseConnection();
                        var Linha = "Novo Depto. Adicionado: " + l_adDeptoRow["cod"].ToString() + " - " + l_adDeptoRow["sec"].ToString();
                        //DadosMysql.Insert("INSERT INTO occe_category(`category_id`, `image`, `parent_id`, `top`, `column`, `sort_order`, `status`, `date_added`, `date_modified`) " + "VALUES ('" + UlID.ToString() + "','catalog/DEPARTAMENTOS/DNW_HIGB_00.png', '1', '0','3','3','1','2017-12-11 19:48:07','2017-12-11 19:48:07'); ");

                        DadosMysql.Insert("INSERT INTO oc_category(`category_id`, `image`, `parent_id`, `top`, `column`, `sort_order`, `status`, `date_added`, `date_modified`) " + "VALUES ('" + UlID.ToString() + "','catalog/DEPARTAMENTOS/DNW_HIGB_00.png', '1', '0','3','3','1','2017-12-11 19:48:07','2017-12-11 19:48:07'); ");

                        tbDeptosNovos.Rows.Add(Linha);
                        this.Invoke((MethodInvoker)delegate ()
                        {

                            Lista.Items.Add(Linha);

                        });
                        // Thread.Sleep(1000);


                        UlID++;

                    }
                    total++;

                    valorgeral = total.ToString();
                }

                #endregion
                
                #region Atualiza Produto
                var TblPMySqlKeys1 = TblPMySql.Select().Select((r) => (string)r["ean"]);
                var l_addedRows = TblPostrgres.Select().Where((r) => !TblPMySqlKeys1.Contains((string)r["barras"]));

                //progressBar1.Maximum = l_addedRows.Count();
                //progressBar1.Minimum = 0;

                DataTable tbDadosEncontrados = new DataTable();
                DataTable tbDadosNovos = new DataTable();

                tbDadosNovos.Columns.Add("Registros", typeof(String));

                total = 0;

                this.Invoke((MethodInvoker)delegate ()
                {
                    progressBar1.Value += 19;

                });
                foreach (var l_addedRow in l_addedRows)
                {
                    descricao = l_addedRow["descricao"].ToString();
                    ean = l_addedRow["barras"].ToString();
                    valor = l_addedRow["preco"].ToString();
                    quantidade = l_addedRow["quantidade"].ToString();

                    //tbDadosEncontrados = DadosMysql.RetornaDados("SELECT * FROM occe_product where ean = '" + ean + "' ");

                    tbDadosEncontrados = DadosMysql.RetornaDados("SELECT * FROM oc_product where ean = '" + ean + "' ");
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
                            DadosMysql.CloseConnection();
                            //DadosMysql.Insert("update occe_product set model = '" + descricao + "', quantity = '" + quantidade + "', date_modified = '" + DateTime.Now.ToString() + "'  where ean =  '" + ean + "'");

                            DadosMysql.Insert("update oc_product set model = '" + descricao + "', quantity = '" + quantidade + "', date_modified = '" + DateTime.Now.ToString() + "'  where ean =  '" + ean + "'");
                            var Linha = "Produto Atualizado: " + ean + " - " + descricao + " ";
                            tbDadosNovos.Rows.Add(Linha);

                            this.Invoke((MethodInvoker)delegate ()
                            {



                                Lista.Items.Add(Linha);


                            });


                            //GridView2.DataSource = tbDadosNovos;
                            //GridView2.DataBind();


                        }
                    }

                    if (Registros == 0)
                    {
                        string datahoje = Convert.ToString(DateTime.Now);
                        DadosMysql.CloseConnection();

                        //DadosMysql.Insert("INSERT INTO `occe_product` (`product_id`, `model`, `sku`, `upc`, `ean`, `jan`, `isbn`, `mpn`, `location`, `quantity`, `stock_status_id`, `image`, `manufacturer_id`, `shipping`, `price`, `points`, `tax_class_id`, `date_available`, `weight`, `weight_class_id`, `length`, `width`, `height`, `length_class_id`, `subtract`, `minimum`, `sort_order`, `status`, `viewed`, `date_added`, `date_modified`)" + " VALUES ('" + UlIP.ToString() + "', '" + descricao + "', '', '', '" + ean + "', '', '', '', '', '" + quantidade + "', '7', 'catalog/PRODUTOS/DNW_PRO_GEN1_00.png', '5','1', '" + valor + "', '0', '0', '" + DateTime.Now.ToString() + "', '5.00000000', '2', '1.00000000', '2.00000000', '3.00000000', '1', '1', '1', '1', '1', '1', '" + datahoje + "', '0000-00-00'); ");


                        DadosMysql.Insert("INSERT INTO `oc_product` (`product_id`, `model`, `sku`, `upc`, `ean`, `jan`, `isbn`, `mpn`, `location`, `quantity`, `stock_status_id`, `image`, `manufacturer_id`, `shipping`, `price`, `points`, `tax_class_id`, `date_available`, `weight`, `weight_class_id`, `length`, `width`, `height`, `length_class_id`, `subtract`, `minimum`, `sort_order`, `status`, `viewed`, `date_added`, `date_modified`)" + " VALUES ('" + UlIP.ToString() + "', '" + descricao + "', '', '', '" + ean + "', '', '', '', '', '" + quantidade + "', '7', 'catalog/PRODUTOS/DNW_PRO_GEN1_00.png', '5','1', '" + valor + "', '0', '0', '" + DateTime.Now.ToString() + "', '5.00000000', '2', '1.00000000', '2.00000000', '3.00000000', '1', '1', '1', '1', '1', '1', '" + datahoje + "', '0000-00-00'); ");


                        //DadosMysql.Insert("INSERT INTO `occe_product_description` (`product_id`, `language_id`, `name`, `description`, `tag`, `meta_title`, `meta_description`, `meta_keyword`) " + "VALUES ('" + UlIP.ToString() + "', 2, '" + descricao + "', '" + descricao + "', 'xx', 'xx', 'xx', 'xx'); ");

                        DadosMysql.Insert("INSERT INTO `oc_product_description` (`product_id`, `language_id`, `name`, `description`, `tag`, `meta_title`, `meta_description`, `meta_keyword`) " + "VALUES ('" + UlIP.ToString() + "', 2, '" + descricao + "', '" + descricao + "', 'xx', 'xx', 'xx', 'xx'); ");

                        var Linha = "Novo Produto Adicionado: " + l_addedRow["barras"].ToString() + " - " + l_addedRow["descricao"].ToString();
                        tbDadosNovos.Rows.Add(Linha);
                        this.Invoke((MethodInvoker)delegate ()
                        {

                            Lista.Items.Add(Linha);

                        });

                        UlIP++;
                        //GridView2.DataSource = tbDadosNovos;
                        //GridView2.DataBind();
                    }
                    total++;
                    valorgeral = total.ToString();

                    //   progressBar1.Step = total;
                }
                #endregion
                
                #region Atualiza Produto 2
                var TblIMySqlKeys = TblPMySql.Select().Select((r) => (string)r["ean"]);
                var l_iguaisRows = TblPostrgres.Select().Where((r) => TblIMySqlKeys.Contains((string)r["barras"]));


                DataTable tbDadosIEncontrados = new DataTable();
                DataTable tbDadosIguais = new DataTable();

                total = 0;
                valorgeral = total.ToString();


                this.Invoke((MethodInvoker)delegate ()
                {


                    progressBar1.Value += 5;

                });


                foreach (var l_igualRow in l_iguaisRows)
                {

                    descricao = l_igualRow["descricao"].ToString();
                    ean = l_igualRow["barras"].ToString();
                    valor = l_igualRow["preco"].ToString();
                    quantidade = l_igualRow["quantidade"].ToString();

                    //tbDadosIEncontrados = DadosMysql.RetornaDados("SELECT * FROM occe_product where ean = '" + ean + "' ");
                    tbDadosIEncontrados = DadosMysql.RetornaDados("SELECT * FROM oc_product where ean = '" + ean + "' ");
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
                            DadosMysql.CloseConnection();
                            //DadosMysql.Insert("update occe_product set model = '" + descricao + "', quantity = '" + quantidade + "', date_modified = '" + dtfi + "' where ean =  '" + ean + "' ");
                            DadosMysql.Insert("update oc_product set model = '" + descricao + "', quantity = '" + quantidade + "', date_modified = '" + dtfi + "' where ean =  '" + ean + "' ");
                            var Linha = "Produto Atualizado: " + ean + " - " + descricao + " ";
                            tbDadosIguais.Rows.Add(Linha);

                            this.Invoke((MethodInvoker)delegate ()
                            {

                                Lista.Items.Add(Linha);

                            });
                        }
                    }
                    total++;
                    valorgeral = total.ToString();

                }


                #endregion
                this.Invoke((MethodInvoker)delegate ()
                {


                    progressBar1.Value += 10;

                });

                
                #region Deleta Produto
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // Find deleted rows:
                var TblPMySqlKeys = TblPostrgres.Select().Select((r) => (string)r["barras"]);
                var l_deletedRows = TblPMySql.Select().Where((r) => !TblPMySqlKeys.Contains((string)r["ean"]));

                //this.Invoke((MethodInvoker)delegate ()
                //{
                //    progressBar1.Maximum = l_deletedRows.Count();
                //});


                //progressBar1.Minimum = 0;

                DataTable tbDadosADExcluir = new DataTable();
                DataTable tbDadosExcluidos = new DataTable();
                tbDadosExcluidos.Columns.Add("Registros", typeof(String));

                total = 0;

                this.Invoke((MethodInvoker)delegate ()
                {


                    progressBar1.Value += 25;

                });

                foreach (var l_deletedRow in l_deletedRows)
                {
                    var ean = l_deletedRow["ean"].ToString();

                    //tbDadosADExcluir = DadosMysql.RetornaDados("SELECT * FROM occe_product where ean = '" + ean + "' and status = 1");

                    tbDadosADExcluir = DadosMysql.RetornaDados("SELECT * FROM oc_product where ean = '" + ean + "' and status = 1");
                    int Registros = 0;
                    DataRow[] rows;

                    rows = tbDadosADExcluir.Select("ean = '" + ean + "' ");
                    Registros = rows.Length;
                    string num = Registros.ToString();


                    if (Registros > 0)
                    {
                        DadosMysql.CloseConnection();
                        //DadosMysql.Insert("update occe_product set status = '0' where ean = '" + ean + "' ");// (product_id, model, sku, upc, ean, jan, isbn, mpn, location, quantity, stock_status_id, image, manufacturer_id, shipping, price, points, tax_class_id, date_available, weight, weight_class_id, length, width, height, length_class_id, subtract, minimum, sort_order, status, viewed, date_added, date_modified) VALUES (NULL, '"+descricao+"', '', '', '"+ean+"', '', '', '', '', '"+quantidade+"', '7', NULL, '5','1', '"+valor+ "', '0', '0', '" + DateTime.Now.ToString() + "', '0.00000000', '0', '0.00000000', '0.00000000', '0.00000000', '0', '1', '1', '0', '0', '0', '" + DateTime.Now.ToString()+"', '0000-00-00'); ");

                        DadosMysql.Insert("update oc_product set status = '0' where ean = '" + ean + "' ");// (product_id, model, sku, upc, ean, jan, isbn, mpn, location, quantity, stock_status_id, image, manufacturer_id, shipping, price, points, tax_class_id, date_available, weight, weight_class_id, length, width, height, length_class_id, subtract, minimum, sort_order, status, viewed, date_added, date_modified) VALUES (NULL, '"+descricao+"', '', '', '"+ean+"', '', '', '', '', '"+quantidade+"', '7', NULL, '5','1', '"+valor+ "', '0', '0', '" + DateTime.Now.ToString() + "', '0.00000000', '0', '0.00000000', '0.00000000', '0.00000000', '0', '1', '1', '0', '0', '0', '" + DateTime.Now.ToString()+"', '0000-00-00'); ");
                        var Linha = "Produto Excluido: " + l_deletedRow["ean"].ToString() + " - " + l_deletedRow["model"].ToString();
                        tbDadosExcluidos.Rows.Add(Linha);
                        this.Invoke((MethodInvoker)delegate ()
                        {

                            Lista.Items.Add(Linha);

                        });
                    }
                    total++;
                    valorgeral = total.ToString();

                    //this.Invoke((MethodInvoker)delegate ()
                    //{
                    //    progressBar1.Step = total;
                    //});


                }

                #endregion

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
                    this.Invoke((MethodInvoker)delegate ()
                    {

                        Lista.Items.Add(Linha);

                    });
                }

                this.Invoke((MethodInvoker)delegate ()
                {


                    progressBar1.Value += 25;


                    progressBar1.Refresh();
                });


                this.Invoke((MethodInvoker)delegate ()
                {
                    label2.Text = "ATUALIZAÇÃO DAS " + lblHora.Text + " FINALIZADA..";
                    btnIniciarProcesso.Enabled = true;
                });

                
                this.Invoke((MethodInvoker)delegate ()
                {

                    Lista.Items.Add("Atualização realizada com sucesso.");

                });
                TimerInicio.Enabled = true;

            }

            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate ()
                {

                    Lista.Items.Add("Ocorreu um erro interno - " + ex + "");

                });
            }

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Atualizar();

        }

        public Form1()
        {
            InitializeComponent();

         
            //Ini.IniFile("checkout");
            //variavelhora = Ini.IniReadString("TEMPO", "horas", "");

            var worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.DoWork += backgroundWorker1_DoWork;
            //worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;

            

            lblHora.Text = "-";

            if (DadosPostgres.OpenConnection())
            {

                DadosPostgres.CloseConnection();
                conectado = true;
            }
            else { conectado = false; label2.Text = "Postgres"; }

            if (DadosMysql.OpenConnection())
            {

                DadosMysql.CloseConnection();
                conectado = true;
            }
            else { conectado = false; label2.Text = "Mysql"; }

            if (conectado == true)
            {
                label2.Text = "Bases de dados conectadas..";
            }
            else
            {
                label1.Text = "Falta permissão de conexão: ";
                label1.Visible = true;
                label2.Visible = true;
            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                Environment.Exit(1);
            }
        }

        private void TimerInicio_Tick(object sender, EventArgs e)
        {

            if (lblHora.Text == DateTime.Now.ToString())
            {
                btnIniciarProcesso.Enabled = false;
                
                Cursor.Current = Cursors.WaitCursor;

                Atualizar();
            }


        }

        private void btnIniciarProcesso_Click(object sender, EventArgs e)
        {



            btnIniciarProcesso.Enabled = false;
            
            Cursor.Current = Cursors.WaitCursor;
            backgroundWorker1.RunWorkerAsync();

            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            label4.Text = valorgeral;
            Lista.Items.Add(valorgeral);
            label4.Refresh();
            //progressBar1.Step = total;
            //progressBar1.Refresh();

            
        }

        private void button3_Click(object sender, EventArgs e)
        {

            DataTable tbDeptosEncontrados2 = new DataTable();
            DataTable tbDeptosNovos2 = new DataTable();

            //tbDeptosEncontrados2 = DadosMysql.RetornaDadosDepto("SELECT * FROM occe_category_description where name = '" + descricao + "' ");

            tbDeptosEncontrados2 = DadosMysql.RetornaDadosDepto("SELECT * FROM oc_category_description where name = '" + descricao + "' ");


            total = 0;


              //  ean = l_adDeptoRow["cod"].ToString();
             //   descricao = l_adDeptoRow["sec"].ToString();
                descricao = descricao.Replace("'", "");
                descricao = descricao.Trim();

                int UlID = 0;

                int Registros = 0;
                DataRow[] rows;

                rows = tbDeptosEncontrados2.Select("name = '" + descricao + "' ");
                // tbDeptosEncontrados.Select("category_id").Max().ToString().FirstOrDefault();

                foreach (DataRow dr in rows)
                {
                    Mdescricao = dr["name"].ToString();
                    Mean = dr["category_id"].ToString();
                }

                Registros = rows.Length;
                string num = Registros.ToString();

            DadosMysql.CloseConnection();

            if (Registros == 0)
                {
                    // DadosMysql.CloseConnection();
            //        var Linha = "Novo Depto. Adicionado: " + l_adDeptoRow["cod"].ToString() + " - " + l_adDeptoRow["sec"].ToString();
                    //   tbDeptosNovos.Rows.Add(Linha);
                    //   Lista.Items.Add(Linha);
                    // Thread.Sleep(1000);
                 //   DadosMysql.Insert("INSERT INTO occe_category(`category_id`, `image`, `parent_id`, `top`, `column`, `sort_order`, `status`, `date_added`, `date_modified`) " +
                   //     "VALUES ('" + UlID.ToString() + "','catalog/DEPARTAMENTOS/DNW_HIGB_00.png','0','3','3','1', '1', '2017-12-11 19:48:07','2017-12-11 19:48:07'); ");

                    UlID++;

                }
                total++;

                valorgeral = total.ToString();
            

        }



        
    }
}
