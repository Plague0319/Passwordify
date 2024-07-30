using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;

namespace Jelszavazo
{
    class Database
    {
        
        protected int session_id;
        protected MySqlDataReader Reader;
        protected MySqlConnection connection;
        protected string server;
        protected string database;
        protected string uid;
        protected string password;
        protected string errors;
        protected bool logged=false;
        protected string pw1;
        protected string pw2;
        

        

        public override string? ToString()
        {
            return base.ToString();
        }

        //Constructor
        public Database()
        {
            Initialize();
        }

        //Initialize values
        protected void Initialize()
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
        protected bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        errors=("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        errors=("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        protected bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                errors=("Something went wrong");
                return false;
            }
        }

        
        
        //Update statement
        public void Update()
        {
        }

        //Delete statement
        public void Delete()
        {
        }

        //Select statement
        /*public List<string>[] Select()
        {
        }

        //Count statement
        public int Count()
        {
        }

        //Backup
        public void Backup()
        {
        }

        //Restore
        public void Restore()
        {
        }
        public override string? ToString()
        {
            return base.ToString();
        }*/

    }
}
