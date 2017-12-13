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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker1.WorkerSupportsCancellation = true;
            

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

            var TblMySqlDepto = new DataTable(); // postgres
            TblMySqlDepto.Columns.Add("category_id", typeof(string));
            TblMySqlDepto.Columns.Add("name", typeof(string));
            TblMySqlDepto.Columns.Add("language_id", typeof(string));

            string PSql = " SELECT * from web.prd_cad where uad = 1 order by descricao limit 10";


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

            TblPMySql = DadosMysql.RetornaDados("SELECT * FROM occe_product  limit 10");


            valorgeral = "Carregando dados de produtos postgres..";

            TblPostrgres = DadosPostgres.RetornaDados(PSql);


            valorgeral = "Carregando dados de categorias postgres..";

            TblPostrgresDepto = DadosPostgres.RetornaDadosDepto("select * from web.cad_sec  limit 10");


            valorgeral = "Carregando dados de categorias opencart..";

            TblMySqlDepto = DadosMysql.RetornaDadosDepto("SELECT * FROM occe_category_description  limit 10");

            int UlID = DadosMysql.RetornaMaxId("SELECT max(category_id) as category_id FROM occe_category_description");
            int UlIP = DadosMysql.RetornaMaxId("SELECT max(product_id) as product_id FROM occe_product");

            UlID++;
            UlIP++;

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

    ////////////// INSERINDO CATEGORIA ///////////////////////////////////////////////////////////////////////////////

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

                DadosMysql.CloseConnection();

                if (Registros == 0)
                {
                    // DadosMysql.CloseConnection();
                   //DadosMysql.Insert("INSERT INTO occe_category_description(category_id,language_id, name) VALUES ('" + UlID.ToString() + "','1', '" + descricao.Trim() + "'); ");
                   
                    //var Linha = "Novo Depto. Adicionado: " + l_adDeptoRow["cod"].ToString() + " - " + l_adDeptoRow["sec"].ToString();
                    ////   tbDeptosNovos.Rows.Add(Linha);
                    ////   Lista.Items.Add(Linha);
                    //Thread.Sleep(1000);
              //      DadosMysql.Insert("INSERT INTO occe_category(`category_id`, `image`, `parent_id`, `top`, `column`, `sort_order`, `status`, `date_added`, `date_modified`) VALUES ('" + UlID.ToString() + "','catalog/DEPARTAMENTOS/DNW_HIGB_00.png','0','3','3','1','2017-12-11 19:48:07','2017-12-11 19:48:07'); ");

                    UlID++;

                }
                total++;

                valorgeral = total.ToString();

                


            }

            this.Invoke((MethodInvoker)delegate ()
            {


                progressBar1.Value += 10;

            });

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

                tbDeptosEncontrados2 = DadosMysql.RetornaDadosDepto("SELECT * FROM occe_category_description where name = '" + descricao + "' ");

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
                    //   tbDeptosNovos.Rows.Add(Linha);
                    //   Lista.Items.Add(Linha);
                   // Thread.Sleep(1000);
                 //   DadosMysql.Insert("INSERT INTO occe_category(`category_id`, `image`, `parent_id`, `top`, `column`, `sort_order`, `status`, `date_added`, `date_modified`) VALUES ('" + UlID.ToString() + "','catalog/DEPARTAMENTOS/DNW_HIGB_00.png','0','3','3','1','2017-12-11 19:48:07','2017-12-11 19:48:07'); ");

                    UlID++;

                }
                total++;

                valorgeral = total.ToString();
            }


            //GridView2.DataSource = tbDeptosNovos;
            //GridView2.DataBind();

            ////////////// INSERINDO PRODUTOS ///////////////////////////////////////////////////////////////////////////////

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
                     //   DadosMysql.Insert("update occe_product set model = '" + descricao + "', quantity = '" + quantidade + "', date_modified = '" + DateTime.Now.ToString() + "' ");
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
                    // DadosMysql.CloseConnection();

                    //DadosMysql.Insert("INSERT INTO occe_product (product_id, model, sku, upc, ean, jan, isbn, mpn, location, quantity, stock_status_id, image, manufacturer_id, shipping, price, points, tax_class_id, date_available, weight, weight_class_id, length, width, height, length_class_id, subtract, minimum, sort_order, status, viewed, date_added, date_modified) VALUES ('" + UlIP.ToString() + "', '" + descricao + "', '', '', '" + ean + "', '', '', '', '', '" + quantidade + "', '7', 'catalog/PRODUTOS/DNW_PRO_GEN1_00.png', '5','1', '" + valor + "', '0', '0', '" + DateTime.Now.ToString() + "', '5.00000000', '2', '1.00000000', '2.00000000', '3.00000000', '1', '1', '1', '1', '1', '1', '" + datahoje + "', '0000-00-00'); ");
                    //DadosMysql.Insert("INSERT INTO occe_product_description` (product_id, language_id, name, description, tag, meta_title, meta_description, meta_keyword) VALUES ('"+ UlIP.ToString() + "', 2, '" + descricao + "', '" + descricao + "', 'xx', 'xx', 'xx', 'xx'); ");
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

            //GridView2.DataSource = tbDadosNovos;
            //GridView2.DataBind();
            ///////////////////////////////////////////////////////////////////////////////////////////////




            /////////////////////////////////////////////////////////////////////////////////////////////



            var TblIMySqlKeys = TblPMySql.Select().Select((r) => (string)r["ean"]);
            var l_iguaisRows = TblPostrgres.Select().Where((r) => TblIMySqlKeys.Contains((string)r["barras"]));


            //this.Invoke((MethodInvoker)delegate ()
            //{
            //    progressBar1.Maximum = l_iguaisRows.Count();
            //});
            
            //progressBar1.Minimum = 0;

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
                       // DadosMysql.Insert("update occe_product set model = '" + descricao + "', quantity = '" + quantidade + "', date_modified = '" + dtfi + "' where ean =  '" + ean + "' ");
                        var Linha = "Produto Atualizado: " + ean + " - " + descricao + " ";
                        // tbDadosIguais.Rows.Add(Linha);
                        
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


            this.Invoke((MethodInvoker)delegate ()
            {


                progressBar1.Value += 10;

            });


            //GridView2.DataSource = tbDadosIguais;
            //GridView2.DataBind();
            ///////////////////////////////////////////////////////////////////////////////////////////////

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

                tbDadosADExcluir = DadosMysql.RetornaDados("SELECT * FROM occe_product where ean = '" + ean + "' and status = 1");
                int Registros = 0;
                DataRow[] rows;

                rows = tbDadosADExcluir.Select("ean = '" + ean + "' ");
                Registros = rows.Length;
                string num = Registros.ToString();

                if (Registros > 0)
                {
                    DadosMysql.CloseConnection();
                   // DadosMysql.Insert("update occe_product set status = '0' where ean = '" + ean + "' ");// (product_id, model, sku, upc, ean, jan, isbn, mpn, location, quantity, stock_status_id, image, manufacturer_id, shipping, price, points, tax_class_id, date_available, weight, weight_class_id, length, width, height, length_class_id, subtract, minimum, sort_order, status, viewed, date_added, date_modified) VALUES (NULL, '"+descricao+"', '', '', '"+ean+"', '', '', '', '', '"+quantidade+"', '7', NULL, '5','1', '"+valor+ "', '0', '0', '" + DateTime.Now.ToString() + "', '0.00000000', '0', '0.00000000', '0.00000000', '0.00000000', '0', '1', '1', '0', '0', '0', '" + DateTime.Now.ToString()+"', '0000-00-00'); ");
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

            
            //GridView1.DataSource = tbDadosExcluidos;
            //GridView1.DataBind();

            /////////////////////////////////////////////////////////////////////////////////////////////


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
                label2.Text = "ATUALIZAÇÃO DAS " + lblHora.Text + " FINALIXADA..";
                btnIniciarProcesso.Enabled = true;
            });
            
            //GridView3.DataSource = tbDadosModificados2;
            //GridView3.DataBind();

            TimerInicio.Enabled = true;
            

        }

        public Form1()
        {
            InitializeComponent();

            DateTime dt = DateTime.Now;

            dt.AddHours(5);
            lblHora.Text = DateTime.Now.AddHours(5).ToString();

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
            DateTime dt = DateTime.Now;

            dt.AddHours(4);
            lblHora.Text = DateTime.Now.AddHours(4).ToString();
            // AtualizaDados();
        }

        private void btnIniciarProcesso_Click(object sender, EventArgs e)
        {



            btnIniciarProcesso.Enabled = false;
            // wait cursor
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

            tbDeptosEncontrados2 = DadosMysql.RetornaDadosDepto("SELECT * FROM occe_category_description where name = '" + descricao + "' ");


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
