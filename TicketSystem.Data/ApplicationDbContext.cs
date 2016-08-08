using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Domain;

namespace TicketSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("name=TicketSystem", throwIfV1Schema:false)
        {
            
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }    
        
        public DbSet<Ticket> Tickets { get; set; }    
        public DbSet<Category> Categories { get; set; }
        public DbSet<Priority> Priorities { get; set; }        

    }
}
