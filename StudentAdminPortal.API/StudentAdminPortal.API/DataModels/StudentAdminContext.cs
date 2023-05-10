using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StudentAdminPortal.API.DataModels
{
    public class StudentAdminContext :DbContext
    {
        //Connect to Database SQL
        public StudentAdminContext(DbContextOptions<StudentAdminContext> options) : base(options)
        {
        }

        public DbSet<Student> Student { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Address> Address { get; set; }

        
    }
}
