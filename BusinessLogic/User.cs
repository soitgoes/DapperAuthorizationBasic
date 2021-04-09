using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Security.Claims;

namespace BusinessLogic
{
    /// <summary>
    /// Only needed because dapper contrib doesn't support ClaimsIdentities into their parameters
    /// </summary>
    [Table("user")]
    public class UserDao
    {
        public UserDao(User user)
        {
            Email = user.Email;
            Password = user.Password;
        }
        public string Email { get; set; }
        public string Password { get; set; }
    }


    public class User : ClaimsIdentity
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }
    }
    
}
