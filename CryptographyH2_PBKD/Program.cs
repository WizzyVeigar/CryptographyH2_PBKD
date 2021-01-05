using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyH2_PBKD
{
    class Program
    {
        static void Main(string[] args)
        {
            string username;
            string password;

            while (true)
            {
                Console.Clear();

                Console.WriteLine("Would you like to login or create a user?");
                string answer = Console.ReadLine().ToLower();

                if (answer == "create")
                {
                    Console.WriteLine("Please enter username");
                    username = Console.ReadLine();
                    Console.WriteLine("Please enter password");
                    password = Console.ReadLine();

                    try
                    {
                        Console.WriteLine(DatabaseDAL.CreateUser(username, password));
                        Console.ReadKey();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Something went wrong with your login");
                    }
                }
                else if (answer == "login")
                {
                    for (int i = 0; i < 5;)
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter username");
                        username = Console.ReadLine();
                        Console.WriteLine("Please enter password");
                        password = Console.ReadLine();

                        if (DatabaseDAL.Login(username, password))
                        {
                            Console.WriteLine("Login successful");
                            i = 6;
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Failed login, try again");
                            i++;
                        }
                    }
                    Console.WriteLine("Login Failed too many times, account has been locked");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Choose valid operation");
                    Console.ReadKey();
                }
            }
        }
    }
}
