using AzureCashCDN.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCashCDN.Data
{
    public class SchoolContext:DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext>options):base(options)
        {

        }

        public DbSet<Class> Class { get; set; }
        public DbSet<Student> Student { get; set; }
    }
}
