using System.Data;
using Microsoft.AspNetCore.Components;

namespace API.Models
{
    public class ATTSignup
    {
        //you could use attribute of same name as table or you could use different
        public int? id { get; set; }
        public string username { get; set; } = "";
        public string password { get; set; } = "";
        public string fullname { get; set; } = "";
        public string dob { get; set; } = "";
        public object Dob { get; internal set; }
        public string email { get; set; } = "";
        public object Email { get; internal set; }
    }
}
