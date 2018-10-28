using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmployeeBusinessCard.Entities
{
    public class EmployeeDbContext : DbContext
    {       

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
            : base(options)
        {

            Database.EnsureCreated();
        }  


        public DbSet<Employee> Employees { get; set; }
    }
}
