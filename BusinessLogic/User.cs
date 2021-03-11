using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Security.Claims;

namespace BusinessLogic
{
    public class User : ClaimsIdentity
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
    
}
