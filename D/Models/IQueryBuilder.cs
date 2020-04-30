using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Models
{
    public class AnalyticField
    {
        public string Description;
        public string FullName;
        public string TableName;
        public string Name;
        public string JointFullName;
        public string JointTableName;
        public string JointName;
    }

    interface IQueryBuilder
    {
        AnalyticField GetAnalyticField(string description);

        List<AnalyticField> GetAnalyticFields(string[] descriptions);

        string BuildSelectClause(IEnumerable<string> descriptions);

        string BuildSelectClause(IEnumerable<AnalyticField> fields);
    }

    public class QueryBuilder : IQueryBuilder
    {
        public AnalyticField GetAnalyticField(string description)
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

            var analyticField = new AnalyticField
            {
                Description = description,
                FullName = fullName,
                TableName = tableName,
                Name = name,
                JointFullName = jointFullName,
                JointTableName = jointTableName,
                JointName = jointName
            };

            return analyticField;

            /*
                            System.Text.StringBuilder sbSelect = new System.Text.StringBuilder();
            System.Text.StringBuilder sbFrom = new System.Text.StringBuilder();

            sbSelect.Append(" SELECT ");
            sbFrom.Append(" FROM ");

            fields = new List<string>(){
                "ROOM.ID",
                "ROOM.NAME",
                "ROOM.BUILDINGID_BUILDING_NAME"
            };

            foreach (string description in fields)
            {
                var aDescriptionSplitOnUnderscore = description.Split('_');
                var aFullName = aDescriptionSplitOnUnderscore[0];
                var aFullNameSplitOnDot = aFullName.Split('.');
                var aTableName = aFullNameSplitOnDot[0];    
                var aFieldName = aFullNameSplitOnDot[1];
                var aJointDescription = aDescriptionSplitOnUnderscore[1];
                var hasJointDescription = !String.IsNullOrEmpty(aJointDescription);
                var aJointDescriptionSplitOnUnderscore = hasJointDescription ? aJointDescription.Split('_') : null;
                var aJointTable = hasJointDescription ? aJointDescriptionSplitOnUnderscore[0] : null;
                var aJointField = hasJointDescription ? aJointDescriptionSplitOnUnderscore[1] : null;

                sbSelect.Append(" ," + aFullName + " ");

                if (true)
                {
                    var foreignKeys = db.GetForeignKeyProperties<Room>();
                }
            }
            */
        }

        public string BuildSelectClause(IEnumerable<AnalyticField> fields)
        {
            StringBuilder SB
            foreach(AnalyticField analyticField in fields)
            {

            }

            return 
        }

        public List<AnalyticField> GetAnalyticFields(string[] descriptions)
        {
            var list = new List<AnalyticField>();

            foreach (string description in descriptions)
            {
                list.Add(GetAnalyticField(description));
            }

            return list;
        }

        public string BuildSelectClause(IEnumerable<string> descriptions)
        {
            throw new NotImplementedException();
        }
    }
}

/*
 "ROOM.ID","ROOM.NAME","ROOM.BUILDINGID_BUILDING_NAME"

RichField:
string Description,
string FullName,
string TableName,
string Name,
string JointFullName,
string JointTableName,
string JointName
 
IQueryBuilder:
public RichField ConvertToRichField(string name)
public RichField ConvertToRichField(string[] names)
public SelectAndFrom BuildSelectAndFrom(IEnumerable<RichField> richFields)
public string BuildQuery(int id, IEnumerable<RichField> richField)
*/
