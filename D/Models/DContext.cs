using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace D.Models
{
    public class DContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public DContext() : base("name=con")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
                        .HasRequired(m => m.Building)
                        .WithMany(t => t.Rooms)
                        .HasForeignKey(m => m.BuildingId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Room>()
                        .HasRequired(m => m.AlternateBuilding)
                        .WithMany(t => t.AlternateRooms)
                        .HasForeignKey(m => m.AlternateBuildingId)
                        .WillCascadeOnDelete(false);
        }

        public System.Data.Entity.DbSet<D.Models.Building> Buildings { get; set; }

        public System.Data.Entity.DbSet<D.Models.Room> Rooms { get; set; }
        
    }
}
