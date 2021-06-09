﻿using Microsoft.EntityFrameworkCore;
using PF6_Team1_DotNetAssignment.Models;

namespace PF6_Team1_DotNetAssignment.Database
{
    public class Team1DbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectUserBacker> ProjectUserBackers { get; set; }

        public Team1DbContext(DbContextOptions<Team1DbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)                   
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=NewPF6_Team1_New;User Id=sa;Password=admin!@#123");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ProjectUserBacker>().HasKey(pu => new { pu.ProjectKey, pu.UserKey });
            //modelBuilder.Entity<ProjectUserBacker>().HasKey(pu => new { pu.ProjectUserBackerId, pu.ProjectKey, pu.UserKey });

            modelBuilder.Entity<ProjectUserBacker>()
                .HasOne<Project>(pu => pu.Project)
                .WithMany(u => u.UserBackerList)
                .HasForeignKey(pu => pu.ProjectKey);

            modelBuilder.Entity<ProjectUserBacker>()
                .HasOne<User>(pu => pu.User)
                .WithMany(u => u.BackedProjects)
                .HasForeignKey(pu => pu.UserKey);


            //modelBuilder.Entity<Project>().HasKey(pu => new { pu.ProjectId, pu.UserId });

            //modelBuilder.Entity<Project>()
            //    .HasOne<User>(pu => pu.UserId)
            //    .WithMany(u => u.MyProjects)
            //    .HasForeignKey(pu => pu.UserId);

        }
    }
}
