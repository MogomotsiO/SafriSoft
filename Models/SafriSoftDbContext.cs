using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SafriSoftv1._3.Models.Data;

namespace SafriSoftv1._3.Models
{
    public class SafriSoftDbContext : DbContext
    {
        public SafriSoftDbContext() : base("name=SafriSoftDbContext")
        {
        }

        public virtual DbSet<Organisation> Organisations { get; set; }
        public virtual DbSet<OrganisationSoftware> OrganisationSoftwares { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PackageFeature> PackageFeatures { get; set; }
        public virtual DbSet<Software> Softwares { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public static SafriSoftDbContext Create()
        {
            return new SafriSoftDbContext();
        }
    }
}