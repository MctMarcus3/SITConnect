using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SITConnect.Model;

namespace SITConnect
{
    public class SITConnectDbContext: IdentityDbContext<ApplicationUser>
    {
        public SITConnectDbContext(DbContextOptions<SITConnectDbContext> options): base(options)
        {        
        }

    }
}
