using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewnetCore.Models
{
    public class NetCoreDB: IdentityDbContext<ApplicationUser>
    {
        public NetCoreDB(DbContextOptions<NetCoreDB> options): base(options)
        {
                
        }
        public DbSet<PersonsP> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Seed();
        }
    }
}
