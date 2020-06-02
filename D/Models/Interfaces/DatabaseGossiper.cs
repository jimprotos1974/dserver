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
    public class DatabaseGossiper: IDatabaseGossiper
    {
        public NavigableDbContext NavigableDbContext;

        public DatabaseGossiper(NavigableDbContext NavigableDbContext)
        {
            this.NavigableDbContext = NavigableDbContext;
        }

        public string GetPrimaryKeyColumn(string tableName)
        {
            throw new NotImplementedException();
        }

        public string GetRelationshipType(string leftTableName, string rightTableName)
        {
            throw new NotImplementedException();
        }

        public string GetColumnToJoinOn(string leftTableName, string rightTableName)
        {
            var leftEntityType = this.NavigableDbContext.GetEntityTypeByName(leftTableName);
            var rightEntityType = this.NavigableDbContext.GetEntityTypeByName(rightTableName);

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
    }
}