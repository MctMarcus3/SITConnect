using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SITConnect.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreditCard { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string PhotoPath { get; set; }
    }
}
