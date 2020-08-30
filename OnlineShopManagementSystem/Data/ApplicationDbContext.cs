using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShopManagementSystem.Models;

namespace OnlineShopManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ProductTypes>ProductTypes { get; set; }
        public DbSet<Tag>Tags { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
