using System;
using System.Collections.Generic;
using System.Text;
using enjaz.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace enjaz.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Function> Function { get; set; }
        public DbSet<UserTask> UserTask { get; set; }
        public DbSet<TaskList> TaskList { get; set; }

    }
}
