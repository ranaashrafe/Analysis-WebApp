using AnalysisWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalysisWebApp.AnalysisDb
{
    public class AnalysisDbContext : DbContext
    {
        public AnalysisDbContext(DbContextOptions<AnalysisDbContext> options): base(options)
        {

        }
        public DbSet<Organ> Organs { get; set; }
        public DbSet<User>  Users { get; set; }
        public DbSet<UserChoice>  UserChoices { get; set; }
        public DbSet<Message>  Messages{ get; set; }   
        public DbSet<OrganDetail> OrganDetails { get; set; }

   
        public AnalysisDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=SQL5031.site4now.net;Initial Catalog=DB_A55F31_Sagdabakr;User Id=DB_A55F31_Sagdabakr_admin;Password=sagda1996;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
