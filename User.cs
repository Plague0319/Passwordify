using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System.Diagnostics;

namespace Jelszavazo
{
    class User:Database
    {
        string currentApplication = Process.GetCurrentProcess().MainModule.FileName;
        //private int a;
        private int user_session;
        private string id;
        private string name;
        private string password;
        private string email;
        private bool session;
       

        public User(string name, string password = "password", string email = "null")
        {
            this.name = name;
            this.password = password;
            this.email = email;
         
        }
        //public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public bool Session { get => session; set => session = value; }
        public string ID { get => id; set =>id =value; }
        public int User_session { get => user_session; set => user_session = value; }


        private void Initialize()
        {
            server = "localhost";
            database = "jelszavazo";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }
        public void Login(string name, string password)
        {

            string login = "SELECT * FROM users WHERE name=@name_data and password=@password_data";
            //string query = "Select id from users where name=@name_data";
            


            {
                if (this.OpenConnection() == true)
                {
                    //create command and assign the query and connection from the constructor

                    MySqlCommand cmd = new MySqlCommand(login, connection);

                    //MySqlCommand cmd2 = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name_data", name);
                    cmd.Parameters.AddWithValue("@password_data", password);
                    //int usr_id = Convert.ToInt32(cmd.ExecuteScalar());
                    //cmd.Parameters.IndexOf(name);
                    // cmd2.Parameters.AddWithValue("@name_data", name);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            session = true;
                            Console.Clear();
                            if (session)
                            {
                                //user_id = id;
                                session_id = Convert.ToInt32(reader["id"]);
                                user_session = Convert.ToInt32(reader["id"]);
                                //Console.WriteLine("Sikeres belépés\nuser id:" + session_id);

                            }






                        

                        }
                        else
                        {
                            Console.WriteLine("helytelen név/jelszó");
                            Process.Start(currentApplication);

                        }

                    }
                
                   
                    //close connection
                    this.CloseConnection();
                }
            }
        }

        public void InsertUser(string name, string password, string email = "null")
        {
            string query = "INSERT INTO users (name, password) VALUES (@name,@password)";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@password", password);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }
        
        public override string? ToString()
        {
            return String.Format("Felhasználónév: "+name);
        }
    }
}
