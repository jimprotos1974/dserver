using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
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

        public System.Data.Entity.DbSet<D.Models.Teacher> Teachers { get; set; }

        public System.Data.Entity.DbSet<D.Models.Student> Students { get; set; }

        public System.Data.Entity.DbSet<D.Models.Building> Buildings { get; set; }

        public System.Data.Entity.DbSet<D.Models.Room> Rooms { get; set; }

        public System.Data.Entity.DbSet<D.Models.Class> Classes { get; set; }

        public System.Data.Entity.DbSet<D.Models.Country> Countries { get; set; }

        public Dictionary<string, string> GetForeignKeyProperties<DBType>()
        {
            EntityType table = GetTableEntityType<DBType>();
            Dictionary<string, string> foreignKeys = new Dictionary<string, string>();

            foreach (NavigationProperty np in table.NavigationProperties)
            {
                var association = (np.ToEndMember.DeclaringType as AssociationType);
                var constraint = association.ReferentialConstraints.FirstOrDefault();



                if (constraint != null && constraint.ToRole.GetEntityType() == table)
                    foreignKeys.Add(np.Name, constraint.ToProperties.First().Name);

                if (constraint != null && constraint.FromRole.GetEntityType() == table)
                    foreignKeys.Add(np.Name, constraint.ToProperties.First().DeclaringType.Name + "." + constraint.ToProperties.First().Name);
            }

            return foreignKeys;
        }

        private EntityType GetTableEntityType<DBType>()
        {
            return GetTableEntityType(typeof(DBType));
        }

        private EntityType GetTableEntityType(Type DBType)
        {
            ObjectContext objContext = ((IObjectContextAdapter)this).ObjectContext;
            MetadataWorkspace workspace = objContext.MetadataWorkspace;
            EntityType table = workspace.GetEdmSpaceType((StructuralType)workspace.GetItem<EntityType>(DBType.FullName, DataSpace.OSpace)) as EntityType;
            return table;
        }

        public static IEnumerable<string> GetTableName(Type type, DbContext context)
        {
            var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;

            // Get the part of the model that contains info about the actual CLR types
            var objectItemCollection = ((ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace));

            // Get the entity type from the model that maps to the CLR type
            var entityType = metadata
                    .GetItems<EntityType>(DataSpace.OSpace)
                    .Single(e => objectItemCollection.GetClrType(e) == type);

            // Get the entity set that uses this entity type
            var entitySet = metadata
                .GetItems<EntityContainer>(DataSpace.CSpace)
                .Single()
                .EntitySets
                .Single(s => s.ElementType.Name == entityType.Name);

            // Find the mapping between conceptual and storage model for this entity set
            var mapping = metadata.GetItems<EntityContainerMapping>(DataSpace.CSSpace)
                    .Single()
                    .EntitySetMappings
                    .Single(s => s.EntitySet == entitySet);

            // Find the storage entity sets (tables) that the entity is mapped
            var tables = mapping
                .EntityTypeMappings.Single()
                .Fragments;

            // Return the table name from the storage entity set
            return tables.Select(f => (string)f.StoreEntitySet.MetadataProperties["Table"].Value ?? f.StoreEntitySet.Name);
        }
    }
}
