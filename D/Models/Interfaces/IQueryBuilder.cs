using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Models.Interfaces
{
    interface IQueryBuilder
    {
        AnalyticField ConvertToRichField(string name);
        string BuildBasicQuery(IEnumerable<AnalyticField> fields);
    }
}
