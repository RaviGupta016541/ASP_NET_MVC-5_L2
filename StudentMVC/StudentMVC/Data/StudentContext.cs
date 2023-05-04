using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentMVC.Models;

namespace StudentMVC.Data
{
    public class StudentContext:DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options):base(options)
        {

        }
        public DbSet<Student> Student { get; set; }
    }
}
