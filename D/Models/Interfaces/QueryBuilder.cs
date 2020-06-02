using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace D.Models.Interfaces
{
    public class QueryBuilder : IQueryBuilder
    {
        public IDatabaseGossiper DatabaseGossiper;

        public QueryBuilder(IDatabaseGossiper databaseGossiper)
        {
            this.DatabaseGossiper = databaseGossiper;
        }

        public AnalyticField ConvertToRichField(string description)
        {
            var descriptionSplitOnUnderscore = description.Split(new[]{ '_' }, 2);
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

        public string BuildBasicQuery(IEnumerable<string> descriptions)
        {
            List<AnalyticField> analyticFields = new List<AnalyticField>();

            foreach(string description in descriptions)
            {
                analyticFields.Add(ConvertToRichField(description));
            }

            return BuildBasicQuery(analyticFields);
        }

        public string BuildBasicQuery(IEnumerable<AnalyticField> fields)
        {
            StringBuilder sbSelectClause = new StringBuilder();
            StringBuilder sbFromClause = new StringBuilder();

            sbSelectClause.AppendLine(" select ");
            sbFromClause.AppendLine(" from rooms ");

            var descriptionsHistory = new List<string>();
            var namesHistory = new List<string>();
            var jointsHistory = new List<string>();

            foreach (AnalyticField field in fields)
            {
                var joinRequired = !string.IsNullOrEmpty(field.JointTableName);

                if (descriptionsHistory.Contains(field.Description))
                {
                    break;
                }

                descriptionsHistory.Add(field.Description);

                if (!namesHistory.Contains(field.FullName))
                {
                    namesHistory.Add(field.FullName);
                    sbSelectClause.AppendLine(field.FullName + " as [" + field.Description + "], ");
                }

                if (joinRequired)
                {
                    var jointAlias = field.JointTableName + "_ON_" + field.Name;

                    if (!jointsHistory.Contains(jointAlias))
                    {
                        jointsHistory.Add(jointAlias);

                        sbFromClause.AppendLine(" left outer join " + field.JointTableName + " " + jointAlias + " on rooms." + field.Name + " = " + jointAlias + "." + this.DatabaseGossiper.GetColumnToJoinOn("Room", "Building"));
                    }                   

                    sbSelectClause.AppendLine(jointAlias + "." + field.JointName + " as [" + field.Description + "], ");                    
                }
            }

            sbSelectClause.AppendLine(" 1 ");

            return sbSelectClause.ToString() + sbFromClause.ToString();
        }
    }
}