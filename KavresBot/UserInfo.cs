using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KavresBot
{
    public class UserInfo
    {
        private readonly int userID;
        public string howOftenRest = default;
        public string restType = default;

        public UserInfo(int id)
        {
            this.userID = id;
        }

        public int UserID { get => userID; }
    }
}
