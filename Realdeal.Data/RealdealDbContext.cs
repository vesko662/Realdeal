﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Realdeal.Data.Models;

namespace Realdeal.Data
{
    public class RealdealDbContext : IdentityDbContext<ApplicationUser>
    {
        public RealdealDbContext(DbContextOptions<RealdealDbContext> options)
          : base(options)
        {
        }


        public DbSet<Advert> Adverts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ОbservedAdvert> ObservedAdverts { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Realdeal;Integrated Security=true;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ОbservedAdvert>().HasKey(x => new { x.AdvertId, x.UserId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
