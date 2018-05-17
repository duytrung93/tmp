using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Quanlyphuongtien
{
    public class UserLogin
    {
        public string Tockenkey { get; set; }
        public string UserName { get; set; }
        public uint Customer_id { get; set; }
        public string CustomerName { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public ushort UserType_id { get; set; }
        public DateTime TimeLogin { get; set; }
        public string ZaloId { get; set; }

        public UserLogin()
        {

        }
        public UserLogin(string json)
        {
            UserLogin obj = JsonConvert.DeserializeObject<UserLogin>(json);
            Tockenkey = obj.Tockenkey;
            UserName = obj.UserName;
            Customer_id = obj.Customer_id;
            CustomerName = obj.CustomerName;
            FullName = obj.FullName;
            Phone = obj.Phone;
            UserType_id = obj.UserType_id;
            TimeLogin = obj.TimeLogin;
            ZaloId = obj.ZaloId;
        }
    }
}
