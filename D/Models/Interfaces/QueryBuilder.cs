using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace D.Models.Interfaces
{
    public class QueryBuilder : IQueryBuilder
    {
        public AnalyticField ConvertToRichField(string description)
        {
            var descriptionSplitOnUnderscore = description.Split('_');
            var fullName = descriptionSplitOnUnderscore[0];
            var fullNameSplitOnDot = fullName.Split('.');
            var tableName = fullNameSplitOnDot[0];
            var name = fullNameSplitOnDot[1];
            var jointDescription = (descriptionSplitOnUnderscore.Length == 2) ? descriptionSplitOnUnderscore[1] : null;
            var hasJointDescription = !String.IsNullOrEmpty(jointDescription);
            var jointDescriptionSplitOnUnderscore = hasJointDescription ? jointDescription.Split('_') : null;
            var jointTableName = hasJointDescription ? jointDescriptionSplitOnUnderscore[0] : null;
            var jointName = hasJointDescription ? jointDescriptionSplitOnUnderscore[1] : null;
            var jointFullName = hasJointDescription ? (jointTableName + "." + jointName) : null;

            return new AnalyticField()
            {
                Description = description,
                FullName = fullName,
                TableName = tableName,
                Name = name,
                JointFullName = jointFullName,
                JointTableName = jointTableName,
                JointName = jointName
            };
        }

        public string BuildBasicQuery(IEnumerable<AnalyticField> fields)
        {
            throw new NotImplementedException();
        }
    }
}