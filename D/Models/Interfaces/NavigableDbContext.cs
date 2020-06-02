using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace D.Models.Interfaces
{
    public class NavigableDbContext
    {
        DbContext DbContext;

        public NavigableDbContext(DbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public string GetPrimaryKeyColumn(string leftTableName, string rightTableName)
        {
            var leftEntityType = GetEntityTypeByName(leftTableName);
            var rightEntityType = GetEntityTypeByName(rightTableName);

            foreach (NavigationProperty np in leftEntityType.NavigationProperties)
            {
                var association = (np.ToEndMember.DeclaringType as AssociationType);
                var constraint = association.ReferentialConstraints.FirstOrDefault();

                if (
                    constraint != null &&
                    constraint.ToRole.GetEntityType() == leftEntityType &&
                    constraint.FromRole.GetEntityType() == rightEntityType
                    )

                    return constraint.FromProperties.First().Name;
            }

            return null;
        }

        public EntityType GetEntityTypeByName(string tableName)
        {
            var workspace = ((IObjectContextAdapter)this.DbContext).ObjectContext.MetadataWorkspace;
            var itemCollection = (ObjectItemCollection)(workspace.GetItemCollection(DataSpace.OSpace));

            foreach (var e in itemCollection.OfType<EntityType>())
            {
                if (e.Name == tableName)
                {
                    return workspace.GetEdmSpaceType((StructuralType)workspace.GetItem<EntityType>(e.FullName, DataSpace.OSpace)) as EntityType;
                }
            }

            return null;
        }
    }
}