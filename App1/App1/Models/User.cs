using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using SQLite;

namespace App1.Models
{
    public class User
    {
        [Table("User")] 
        public class Benutzer
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public string Benutzername { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }

        }

        
    }

    
}
