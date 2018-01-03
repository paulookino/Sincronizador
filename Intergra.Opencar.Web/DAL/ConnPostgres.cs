using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace Intergra.Opencar.Web.DAL
{
    public class ConnPostgres
    {
        private NpgsqlConnection connection;
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
        public ConnPostgres()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "201.6.100.80";
            database = "SIM_LIDER20170511";
            uid = "postgres";
            password = "postgres";

            //server = "helofarma021.ddns.net";
            //database = "SIM_ADM";
            //uid = "postgres";
            //password = "platinsiad";

            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new NpgsqlConnection(connectionString);
        }


        //open connection to database
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (NpgsqlException ex)
            {
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                retorno = "";
                switch (ex.ErrorCode)
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
            catch (NpgsqlException ex)
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
                NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

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
                NpgsqlCommand cmd = new NpgsqlCommand();
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
                NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public DataTable RetornaDados(string query)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("nome", typeof(string));
            dt.Columns.Add("descricao", typeof(string));
            dt.Columns.Add("modelo", typeof(string));
            dt.Columns.Add("departamento", typeof(string));
            dt.Columns.Add("preco", typeof(string));
            dt.Columns.Add("quantidade", typeof(string));
            dt.Columns.Add("barras", typeof(string));

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                //  NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
                using (var command = new NpgsqlCommand(query, connection))
                {
                    var dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        DataRow row = dt.NewRow();
                        row["nome"] = dr["nome"].ToString();
                        row["descricao"] = dr["descricao"].ToString();
                        row["modelo"] = dr["modelo"].ToString();
                        row["departamento"] = dr["departamento"].ToString();
                        row["preco"] = dr["preco"].ToString();
                        row["quantidade"] = dr["quantidade"].ToString();
                        row["barras"] = dr["barras"].ToString();
                        dt.Rows.Add(row);
                    }
                    //close Data Reader
                    dr.Close();

                    //close Connection
                    CloseConnection();
                }

                return dt;
            }
            else
            {
                return dt;
            }
        }

        public DataTable RetornaDadosDepto(string query)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("cod", typeof(string));
            dt.Columns.Add("sec", typeof(string));

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                //  NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
                using (var command = new NpgsqlCommand(query, connection))
                {
                    var dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        DataRow row = dt.NewRow();
                        row["cod"] = dr["cod"].ToString();
                        row["sec"] = dr["sec"].ToString();
                        dt.Rows.Add(row);
                    }
                    //close Data Reader
                    dr.Close();

                    //close Connection
                    CloseConnection();
                }

                return dt;
            }
            else
            {
                return dt;
            }
        }


        public DataTable RetornaDadosFabricante(string query)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("cod", typeof(string));
            dt.Columns.Add("des", typeof(string));
            dt.Columns.Add("upd", typeof(string));
            dt.Columns.Add("atv", typeof(string));

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                //  NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
                using (var command = new NpgsqlCommand(query, connection))
                {
                    var dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        DataRow row = dt.NewRow();
                        row["cod"] = dr["cod"].ToString();
                        row["des"] = dr["des"].ToString();
                        row["upd"] = dr["upd"].ToString();
                        row["atv"] = dr["atv"].ToString();
                        dt.Rows.Add(row);
                    }
                    //close Data Reader
                    dr.Close();

                    //close Connection
                    CloseConnection();
                }

                return dt;
            }
            else
            {
                return dt;
            }
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
                NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
                //Create a data reader and Execute the command
                NpgsqlDataReader dr = cmd.ExecuteReader();

                //Read the data and store them in the list
                int linha = 0;
                while (dr.Read())
                {
                    list[linha].Add(dr[linha] + "");
                    //list[1].Add(dataReader["name"] + "");
                    //list[2].Add(dataReader["age"] + "");
                    linha++;
                }

                //close Data Reader
                dr.Close();

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
                NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

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