using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptographyH2_PBKD
{
    class DatabaseDAL
    {
        /// <summary>
        /// Creates a user in a json file, or adds it to an existing file
        /// </summary>
        /// <param name="uName">Unique username</param>
        /// <param name="psw">The users password</param>
        /// <returns></returns>
        public static string CreateUser(string uName, string psw)
        {
            List<User> users;
            try
            {
                //Make salt
                byte[] uSalt = HashData.GenerateSalt();
                //Save hashed pass as base64string
                string uHashString = Convert.ToBase64String(HashData.HashMyPassword(Encoding.UTF8.GetBytes(psw), uSalt, 50000));
                //Create a user object we can save as json for test db
                User user = new User(uName, uSalt, uHashString);
                try
                {

                    users = GetUsers();

                    for (int i = 0; i < users.Count; i++)
                    {
                        if (users[i].UserName == uName)
                        {
                            return "User already exists";
                        }                        
                    }

                    users.Add(user);

                }
                catch (Exception)
                {
                    users = new List<User>() { user };
                }

                using (StreamWriter file = File.CreateText(Directory.GetCurrentDirectory() + @"\Database.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, users);
                    file.Close();
                    return "User created successfully";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets all the users in the json file
        /// </summary>
        /// <returns></returns>
        private static List<User> GetUsers()
        {
            StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + @"\Database.json");
            string json = sr.ReadToEnd();
            if (json != null)
            {
                sr.Close();
                return JsonConvert.DeserializeObject<List<User>>(json);
            }
            return null;
        }

        /// <summary>
        /// Get a specific user searched from the username
        /// </summary>
        /// <param name="uName"></param>
        /// <returns>returns the users hashed byte array</returns>
        public static User GetUser(string uName)
        {
            List<User> users = GetUsers();
            
            try
            {
                foreach (User user in users)
                {
                    if (user.UserName == uName)
                    {
                        return user;
                    }
                }
                return null;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        /// <summary>
        /// Method for logging in
        /// </summary>
        /// <param name="username">Attempted username to login with</param>
        /// <param name="password">Attempted password to login with</param>
        /// <returns>Returns true if the login is successful</returns>
        internal static bool Login(string username, string password)
        {
            User dbUser = GetUser(username);
            string attemptedPass = Convert.ToBase64String(HashData.HashMyPassword(Encoding.UTF8.GetBytes(password), dbUser.USalt, 50000));
            if (attemptedPass == dbUser.UPass)
            {
                return true;
            }
            return false;
        }
    }
}
