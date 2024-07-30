using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Jelszavazo
{
     class heart:Database //CORE!!!!!!!
    {
        protected DateTime date = DateTime.Now;
        protected string platform;
        protected int length;
        protected string notgood;
        protected string good;
        protected string username;      
        protected Random rnd = new Random();
        protected string chars= "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";



        public heart()
        {
            
           
            
            Initialize();

        }

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
        public void insert_data(int id,string platform,string username="",string notgood="",int length=10)
        {
            if (notgood.Length != null)
            {
                good = new string(Enumerable.Repeat(chars, length)
               .Select(s => s[rnd.Next(s.Length)]).ToArray());
                //Console.WriteLine(password); ;

            }
            string query = "INSERT INTO core (user_id,username,platform,password,generated_password,gen_pw_length,date) VALUES (@user_id,@username,@platform,@pw1,@pw2,@gen_pw_length,@date)";
            
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@user_id", id);
                cmd.Parameters.AddWithValue("@platform", platform);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@pw1", notgood);
                cmd.Parameters.AddWithValue("@pw2", good);
                cmd.Parameters.AddWithValue("@gen_pw_length", length);
                cmd.Parameters.AddWithValue("@date", date);

                cmd.ExecuteNonQuery();
               
                
               
                

                //open connection

            }
            this.CloseConnection();
        }
        public void view_data(int id)
        {
            string query = "select username,platform,generated_password,gen_pw_length,date from core inner join users on user_id = users.id where users.id=@user_id";
            List<string> list = new List<string>();


            {
                if (this.OpenConnection() == true)
                {
                    //create command and assign the query and connection from the constructor
                    int db = 0;
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@user_id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.Clear();
                        while (reader.Read())
                        {
                            db++;
                            Console.WriteLine("Név:" + reader[0]+"\nPlatform:" + reader[1]+"\nGenerált jelszó:" + reader[2]+"\n");
                            Thread.Sleep(500);
                            Console.WriteLine(db);


                        }
                        if (db == 0)
                        {
                            Console.WriteLine("Nincs jelszó mentve");
                        }
                        Console.WriteLine("Beolvasás megtörtént.");
                        //close connection
                        this.CloseConnection();
                    }
                }
            }
        }
        public void search_data(int id, string password)
        {
            string query = "select username,platform,generated_password,gen_pw_length,date from core inner join users on user_id = users.id where user_id=@user_id and core.password=@password group by platform Order By date Desc";
            List<string> list = new List<string>();


            {
                if (this.OpenConnection() == true)
                {
                    //create command and assign the query and connection from the constructor

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@user_id", id);
                    cmd.Parameters.AddWithValue("@password", password);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.Clear();
                        while (reader.Read())
                        {
                           
                                Console.WriteLine("Név:" + reader[0] + "\nPlatform:" + reader[1] + "\nGenerált jelszó:" + reader[2] + "\nGenerált hossz:"+reader[3]+ "\n");
                                Thread.Sleep(500);
                            
                        }
                       
                        //close connection
                        this.CloseConnection();
                    }
                }
            }
        }
        public void view_pass(int id)
        {
            string query = "select username,platform,generated_password,gen_pw_length,date from core inner join users on user_id = users.id where users.id=@user_id order by date DESC LIMIT 1;";
            


            {
                if (this.OpenConnection() == true)
                {
                    //create command and assign the query and connection from the constructor
                   
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@user_id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.Clear();
                        while (reader.Read())
                        {
                            
                            Console.WriteLine("Név:" + reader[0] + "\nPlatform:" + reader[1] + "\nGenerált jelszó:" + reader[2] + "\n");
                            Thread.Sleep(500);
                            

                        }
                        Console.WriteLine("Beolvasás megtörtént!");
                        //close connection
                        this.CloseConnection();
                    }
                }
            }
        }
        public override string? ToString()
        {
            return string.Format("Megadott jelszó:"+notgood+"\nGenerált Jelszó:" + good);
        }
    }
}
