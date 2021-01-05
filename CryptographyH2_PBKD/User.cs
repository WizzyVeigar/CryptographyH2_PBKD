using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyH2_PBKD
{
    class User
    {
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private byte[] uSalt;

        public byte[] USalt
        {
            get { return uSalt; }
            set { uSalt = value; }
        }

        private string uPass;
        /// <summary>
        /// The users password from the database in base64 string format
        /// </summary>
        public string UPass
        {
            get { return uPass; }
            set { uPass = value; }
        }
        public User(string userName, byte[] uSalt, string uPass)
        {
            UserName = userName;
            USalt = uSalt;
            UPass = uPass;
        }

    }
}
