using Microsoft.EntityFrameworkCore;
using SampleApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApi.Data
{
    public class SampleApiContext : DbContext
    {
        const string _localDbConnectionString = "Server=(localdb)\\MSSQLLocalDB; Database=SampleApi;Integrated Security = True;";
        public SampleApiContext()
        {

        }

        public SampleApiContext(DbContextOptions<SampleApiContext> options) : base(options)
        {

        }

        public virtual DbSet<Location> Locations { get; set; }

        public virtual DbSet<Apartment> Apartments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_localDbConnectionString);
            }
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
