using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Reflection.PortableExecutable;
using Org.BouncyCastle.Tls;
using System.IO;
using Org.BouncyCastle.Bcpg;
using Mysqlx.Crud;


namespace Jelszavazo
{
    class Program
    {
        
        public static void Main(string[] args)
        {
           
            User usr;
            Database db = new Database();
            List<string> users = new List<string>();
            bool user = false;
            string answ;
            string Name;
            string Email;
            string Pw;
            string Pw2;
            int dummy;
            string hash;
            string samepw;
            bool login = false;
            int menu;
            bool registered = false;
          
             
                Console.WriteLine("Rendelkezel fiókkal? Y/N ");
                answ = Console.ReadLine();
                if (answ == "N") {
                    Console.WriteLine("Bejelentkezési név:");
                    Name = Console.ReadLine();
                    string password2;
                    do
                    {
                        Console.WriteLine("Bejelentkezési jelszó:");
                        Pw = Console.ReadLine();
                        Console.WriteLine("Bejelentkezési jelszó megerősítése:");
                        Pw2 = Console.ReadLine();
                    } while (Pw != Pw2);
                    //Console.WriteLine("email:");
                    //string mail = Console.ReadLine();

                    for (int i = 0; i < Pw.Length; i++) //user átverés 
                    {
                        dummy = i + 1;
                        Pw2 = "";
                        for (int j = 0; j < dummy; j++)
                        {
                            Pw2 += "*";
                        }
                    }
                    hash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(Pw)));
                    usr = new User(Name, Pw2);
                    usr.InsertUser(Name, hash);
                    user = true;
                    //Console.WriteLine(usr+"\njelszavad:"+hash);


                }
                else if(answ=="Y"){         
                    user = true;
                    //db.Login(Name, hash);
                    
                   
                }  
           
            //login = true;
            //Menu rendszer kialakítása! jelszó létrehozás, jelszavak megnézése,kilépés Switch case ? 
            Console.Clear();
            Console.WriteLine("Belépés:\nFelhasználónév:");
            Name = Console.ReadLine();
            Console.WriteLine("Jelszó:");
            Console.ForegroundColor = ConsoleColor.Black;
            Pw = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            hash =Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(Pw)));
            usr =new User(Name,hash); //hash titkosítás tesztelése
            //usr.InsertUser(Name, hash);    
            usr.Login(Name, hash);
            if (usr.Session)
            {
                int option;
                Console.WriteLine("Üdvözöllek "+usr.Name+"!");
                Console.WriteLine("1.Jelszó felvétel\n2.Jelszavak megnézése\n3.Jelszó alapján keresés\n4.kilépés");
                option = Convert.ToInt32(Console.ReadLine());
                while (option==null||option<0||option>3)
                {
                    Console.WriteLine("1.Jelszó felvétel\n2.Jelszavak megnézése(összes)\n3.Jelszó alapján keresés (Platformok listázása generált jelszavakkal)\n4.kilépés");
                    option = Convert.ToInt32(Console.ReadLine());
                    
                }
                heart h1 = new heart();
                switch (option){
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Platform");
                        string platform = Console.ReadLine();
                        Console.WriteLine("Felhasználó név");
                        string username = Console.ReadLine();
                        Console.WriteLine("Jelszó megadás");
                        string notgood = Console.ReadLine();
                        Console.WriteLine("[alap:10] Generált jelszó hossza: ");
                        int length = Convert.ToInt32(Console.ReadLine());
                        
                        h1.insert_data(usr.User_session, platform, username, notgood, length);
           
                        Console.WriteLine("adatok rögzítve");
                        h1.view_pass(usr.User_session);
                        break;
                    case 2:
                        h1.view_data(usr.User_session);
                        /*if (usr.Password == null)
                        {
                            Console.WriteLine("Nincs mentett jelszó");
                        }*/
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Jelszó(platformhoz jelszó):");
                        string password=Console.ReadLine();
                        while(password==null) {
                            password = Console.ReadLine();
                            Console.WriteLine("Jelszó(platformhoz jelszó):");
                        }
                        h1.search_data(usr.User_session, password);
                        break;
                        
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
                
                Console.ReadKey();
                /*Console.WriteLine("Platform");
                string platform = Console.ReadLine();
                Console.WriteLine("Felhasználó név");
                string username = Console.ReadLine();
                Console.WriteLine("Jelszó megadás");
                string notgood = Console.ReadLine();
                Console.WriteLine("[alap:10] Generált jelszó hossza: ");
                int length = Convert.ToInt32(Console.ReadLine());
                heart h1 = new heart();
                h1.insert_data(usr.User_session, platform,username,notgood,length);
                //heart h1 = new heart(notgood, length);
                //h1.insert_pw(notgood);
                Console.WriteLine("adatok megnézése");
                Console.ReadKey();
                
                //h1.view_data();*/
            }
           
            
        }
        
    }
}