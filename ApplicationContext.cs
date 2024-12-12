﻿
using AllFunctionalityNetCore.Models;

using Microsoft.EntityFrameworkCore;

namespace AllFunctionalityNetCore.Data
{
    public class ApplicationContext:DbContext
    {
      public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
      public DbSet<User> Users { get; set; }
      public DbSet<Employee> Employees { get; set; }
     
    }
}
