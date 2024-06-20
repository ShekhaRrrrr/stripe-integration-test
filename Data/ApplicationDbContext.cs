﻿using Microsoft.EntityFrameworkCore;
using stripewebapp.Models;

namespace stripewebapp.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Item> Items { get; set; }
    }
}