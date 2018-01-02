using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace Intergra.Opencar.Web.DAL
{
    public class ConnMySql
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        public string retorno;

        public string Retornar(string retorno)
        {
            return retorno;
        }
        //Constructor
        public ConnMySql()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {

            //Base de Desenvolvimento
            server = "mysql5018.site4now.net"; //208.91.199.145
            database = "db_a1938f_loja_1";
            uid = "a1938f_loja_1";
            password = "4rtr4x2308";

            //server = "208.91.199.145";
            //database = "drogaria_ocar121";
            //uid = "drogaria_ocar121";
            //password = "9!vS5(6Sp3";


            


            string connectionString;

            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";Convert Zero Datetime=True; Allow Zero Datetime=True";

            connection = new MySqlConnection(connectionString);
        }


        //open connection to database
        public bool OpenConnection() // 208.91.199.145
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();   
                }
                    connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                retorno = "";
                switch (ex.Number)
                {
                    case 0:
                        retorno = "Cannot connect to server.  Contact administrator";
                        Retornar(retorno);
                        break;

                    case 1045:
                        retorno = "Invalid username/password, please try again";
                        Retornar(retorno);
                        break;
                }
                return false;
            }
        }

        //Close connection
        public bool CloseConnection()
        {
            retorno = "";
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                retorno = ex.Message;
                Retornar(retorno);
                return false;
            }
        }

        //Insert statement
        public void Insert(string query)
        {
            //string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update(string query)
        {
            //string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete(string query)
        {
            // string query = "DELETE FROM tableinfo WHERE name='John Smith'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public int RetornaMaxId(string query)
        {
            int ID = 0;
            try
            {
                if (OpenConnection())
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    while (dr.Read())
                    {
                        
                        DataRow row = dt.NewRow();
                        ID =  query.Contains("manufacturer_id") ? Convert.ToInt32(dr["manufacturer_id"].ToString()) 
                            : query.Contains("category_id") ? Convert.ToInt32(dr["category_id"].ToString()) 
                            : Convert.ToInt32(dr["product_id"].ToString());
                        dt.Rows.Add(row);
                    }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                    return ID;

                }
                else
                {
                    return ID;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }
            return ID;
        }

        public DataTable RetornaDados(string query)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("product_id", typeof(int)); //11) NOT NULL AUTO_INCREMENT,
            dt.Columns.Add("model", typeof(string)); //64) NOT NULL,
            dt.Columns.Add("sku", typeof(string)); //64) NOT NULL,
            dt.Columns.Add("upc", typeof(string)); //12) NOT NULL,
            dt.Columns.Add("ean", typeof(string)); //14) NOT NULL,
            dt.Columns.Add("jan", typeof(string)); //13) NOT NULL,
            dt.Columns.Add("isbn", typeof(string)); //17) NOT NULL,
            dt.Columns.Add("mpn", typeof(string)); //64) NOT NULL,
            dt.Columns.Add("location", typeof(string)); //128) NOT NULL,
            dt.Columns.Add("quantity", typeof(int)); //4) NOT NULL DEFAULT '0',
            dt.Columns.Add("stock_status_id", typeof(int)); //11) NOT NULL,
            dt.Columns.Add("image", typeof(string)); //255) DEFAULT NULL,
            dt.Columns.Add("manufacturer_id", typeof(int)); //11) NOT NULL,
            dt.Columns.Add("shipping", typeof(int)); //11) NOT NULL,
            dt.Columns.Add("price", typeof(decimal)); //15,4) NOT NULL DEFAULT '0.0000',
            dt.Columns.Add("points", typeof(int)); //8) NOT NULL DEFAULT '0',
            dt.Columns.Add("tax_class_id", typeof(int)); //11) NOT NULL,
            dt.Columns.Add("date_available", typeof(int)); //11) NOT NULL,
            dt.Columns.Add("weight", typeof(decimal)); //15,8) NOT NULL DEFAULT '0.00000000',
            dt.Columns.Add("weight_class_id", typeof(int)); //11) NOT NULL DEFAULT '0',
            dt.Columns.Add("length", typeof(decimal)); //15,8) NOT NULL DEFAULT '0.00000000',
            dt.Columns.Add("width", typeof(decimal)); //15,8) NOT NULL DEFAULT '0.00000000',
            dt.Columns.Add("height", typeof(decimal)); //15,8) NOT NULL DEFAULT '0.00000000',
            dt.Columns.Add("length_class_id", typeof(int)); //11) NOT NULL DEFAULT '0',
            dt.Columns.Add("subtractl_table", typeof(int)); //11) NOT NULL,
            dt.Columns.Add("minimum", typeof(int)); //11) NOT NULL DEFAULT '1',
            dt.Columns.Add("sort_order", typeof(int)); //11) NOT NULL DEFAULT '0',
            dt.Columns.Add("statusl_table", typeof(int)); //11) NOT NULL,
            dt.Columns.Add("viewed", typeof(int)); //5) NOT NULL DEFAULT '0',
            dt.Columns.Add("date_added", typeof(DateTime)); //

            //Open connection
            try
            {
                if (OpenConnection())
                {

                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        DataRow row = dt.NewRow();
                        row["product_id"] = dr["product_id"].ToString();
                        row["model"] = dr["model"].ToString();
                        row["sku"] = dr["sku"].ToString();
                        row["upc"] = dr["upc"].ToString();
                        row["ean"] = dr["ean"].ToString();
                        row["jan"] = dr["jan"].ToString();
                        row["isbn"] = dr["isbn"].ToString();
                        row["mpn"] = dr["mpn"].ToString();
                        row["location"] = dr["location"].ToString();
                        row["quantity"] = dr["quantity"].ToString();
                        row["stock_status_id"] = dr["stock_status_id"].ToString();
                        row["image"] = dr["image"].ToString();
                        row["manufacturer_id"] = dr["manufacturer_id"].ToString();
                        // row["shipping"] = dr["shipping"].ToString();
                        row["price"] = dr["price"].ToString();
                        row["points"] = dr["points"].ToString();
                        row["tax_class_id"] = dr["tax_class_id"].ToString();
                        //      row["date_available"] = dr["date_available"].ToString();
                        row["weight"] = dr["weight"].ToString();
                        row["weight_class_id"] = dr["weight_class_id"].ToString();
                        row["length"] = dr["length"].ToString();
                        row["width"] = dr["width"].ToString();
                        row["height"] = dr["height"].ToString();
                        row["length_class_id"] = dr["length_class_id"].ToString();
                        //      row["subtractl_table"] = dr["subtractl_table"].ToString();
                        row["minimum"] = dr["minimum"].ToString();
                        row["sort_order"] = dr["sort_order"].ToString();
                        //          row["statusl_table"] = dr["statusl_table"].ToString();
                        row["viewed"] = dr["viewed"].ToString();
                        row["date_added"] = dr["date_added"].ToString();


                        dt.Rows.Add(row);
                    }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                    return dt;

                }
                else
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }
            return dt;
        }

        public DataTable RetornaDadosDepto(string query)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("category_id", typeof(int)); //11) NOT NULL AUTO_INCREMENT,
            dt.Columns.Add("language_id", typeof(int)); //11) NOT NULL AUTO_INCREMENT,
            dt.Columns.Add("name", typeof(string)); //64) NOT NULL,

            //Open connection
            try
            {
                if (OpenConnection())
                {

                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        DataRow row = dt.NewRow();
                        row["language_id"] = dr["language_id"].ToString();
                        row["name"] = dr["name"].ToString();

                        dt.Rows.Add(row);
                    }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                    return dt;

                }
                else
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }
            return dt;
        }


        public DataTable RetornaDadosFabricante(string query)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("manufacturer_id", typeof(int)); //11) NOT NULL AUTO_INCREMENT,
            dt.Columns.Add("name", typeof(string)); //11) NOT NULL AUTO_INCREMENT,
            dt.Columns.Add("image", typeof(string)); //64) NOT NULL,
            dt.Columns.Add("sort_order", typeof(int)); //11) NOT NULL AUTO_INCREMENT,

            //Open connection
            try
            {
                if (OpenConnection())
                {

                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        DataRow row = dt.NewRow();
                        row["name"] = dr["name"].ToString();
                        row["image"] = dr["image"].ToString();
                        row["sort_order"] = dr["sort_order"].ToString();

                        dt.Rows.Add(row);
                    }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                    return dt;

                }
                else
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }
            return dt;
        }


        public DataTable RetornaDadosCategoryPath(string query)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("category_id", typeof(int)); //11) NOT NULL AUTO_INCREMENT,
            dt.Columns.Add("path_id", typeof(int)); //11) NOT NULL AUTO_INCREMENT,
            dt.Columns.Add("level", typeof(int)); //11) NOT NULL AUTO_INCREMENT,

            //Open connection
            try
            {
                if (OpenConnection())
                {

                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        DataRow row = dt.NewRow();
                        //row["category_id"] = dr["category_id"].ToString();
                        row["path_id"] = dr["path_id"].ToString();
                        row["level"] = dr["level"].ToString();

                        dt.Rows.Add(row);
                    }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                    return dt;

                }
                else
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }
            return dt;
        }


        public DataTable RetornaDadosCategoryLayout(string query)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("category_id", typeof(int)); //11) NOT NULL AUTO_INCREMENT,
            dt.Columns.Add("store_id", typeof(int)); //11) NOT NULL AUTO_INCREMENT,
            dt.Columns.Add("layout_id", typeof(int)); //11) NOT NULL AUTO_INCREMENT,

            //Open connection
            try
            {
                if (OpenConnection())
                {

                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        DataRow row = dt.NewRow();
                        //row["category_id"] = dr["category_id"].ToString();
                        row["store_id"] = dr["store_id"].ToString();
                        row["layout_id"] = dr["layout_id"].ToString();

                        dt.Rows.Add(row);
                    }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                    return dt;

                }
                else
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }
            return dt;
        }


        public DataTable RetornaDadosCategoryStore(string query)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("category_id", typeof(int)); //11) NOT NULL AUTO_INCREMENT,
            dt.Columns.Add("store_id", typeof(int)); //11) NOT NULL AUTO_INCREMENT,

            //Open connection
            try
            {
                if (OpenConnection())
                {

                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        DataRow row = dt.NewRow();
                        //row["category_id"] = dr["category_id"].ToString();
                        row["store_id"] = dr["store_id"].ToString();

                        dt.Rows.Add(row);
                    }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                    return dt;

                }
                else
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }
            return dt;
        }


        public DataTable RetornaDadosProductCategory(string query)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("product_id", typeof(int)); //11) NOT NULL AUTO_INCREMENT,
            dt.Columns.Add("category_id", typeof(int)); //11) NOT NULL AUTO_INCREMENT,

            //Open connection
            try
            {
                if (OpenConnection())
                {

                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        DataRow row = dt.NewRow();
                        //row["product_id"] = dr["product_id"].ToString();
                        row["category_id"] = dr["category_id"].ToString();

                        dt.Rows.Add(row);
                    }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                    return dt;

                }
                else
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }
            return dt;
        }


        public DataTable RetornaDadosProductStore(string query)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("product_id", typeof(int)); //11) NOT NULL AUTO_INCREMENT,
            dt.Columns.Add("store_id", typeof(int)); //11) NOT NULL AUTO_INCREMENT,

            //Open connection
            try
            {
                if (OpenConnection())
                {

                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        DataRow row = dt.NewRow();
                        //row["product_id"] = dr["product_id"].ToString();
                        row["store_id"] = dr["store_id"].ToString();

                        dt.Rows.Add(row);
                    }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                    return dt;

                }
                else
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }
            return dt;
        }


        public DataTable RetornaDadosManufacturerStore(string query)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("manufacturer_id", typeof(int)); //11) NOT NULL AUTO_INCREMENT,
            dt.Columns.Add("store_id", typeof(int)); //11) NOT NULL AUTO_INCREMENT,

            //Open connection
            try
            {
                if (OpenConnection())
                {

                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        DataRow row = dt.NewRow();
                        //row["manufacturer_id"] = dr["manufacturer_id"].ToString();
                        row["store_id"] = dr["store_id"].ToString();

                        dt.Rows.Add(row);
                    }

                    //close Data Reader
                    dr.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                    return dt;

                }
                else
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }
            return dt;
        }

        //Select statement
        public List<string>[] Select(string query)
        {
            // string query = "SELECT * FROM tableinfo";

            //Create a list to store the result
            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                int linha = 0;
                while (dataReader.Read())
                {
                    list[linha].Add(dataReader[linha] + "");
                    //list[1].Add(dataReader["name"] + "");
                    //list[2].Add(dataReader["age"] + "");
                    linha++;
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        //Count statement
        public int Count(string query)
        {
            //string query = "SELECT Count(*) FROM tableinfo";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        //Backup
        public void Backup(string caminho)
        {
            retorno = "";
            try
            {
                DateTime Time = DateTime.Now;
                int year = Time.Year;
                int month = Time.Month;
                int day = Time.Day;
                int hour = Time.Hour;
                int minute = Time.Minute;
                int second = Time.Second;
                int millisecond = Time.Millisecond;

                //Save file to C:\ with the current date as a filename
                string path;
                path = caminho + "\\" + year + "-" + month + "-" + day + "-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";
                StreamWriter file = new StreamWriter(path);


                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysqldump";
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
                psi.UseShellExecute = false;

                Process process = Process.Start(psi);

                string output;
                output = process.StandardOutput.ReadToEnd();
                file.WriteLine(output);
                process.WaitForExit();
                file.Close();
                process.Close();
            }
            catch (IOException ex)
            {
                retorno = "Error , unable to backup!" + ex;
                Retornar(retorno);
            }
        }

        //Restore
        public void Restore(string caminho)
        {
            retorno = "";
            try
            {
                //Read file from C:\
                string path;
                path = caminho + "\\MySqlBackup.sql";
                StreamReader file = new StreamReader(path);
                string input = file.ReadToEnd();
                file.Close();


                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysql";
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = false;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
                psi.UseShellExecute = false;


                Process process = Process.Start(psi);
                process.StandardInput.WriteLine(input);
                process.StandardInput.Close();
                process.WaitForExit();
                process.Close();
            }
            catch (IOException ex)
            {
                retorno = "Error , unable to Restore!";
                Retornar(retorno);
            }
        }
    }
}