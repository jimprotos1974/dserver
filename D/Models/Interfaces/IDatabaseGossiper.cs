using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Models.Interfaces
{
    public interface IDatabaseGossiper
    {
        string GetPrimaryKeyColumn(string tableName);

        string GetRelationshipType(string leftTableName, string rightTableName);

        string GetColumnToJoinOn(string leftTableName, string rightTableName);
    }
}
