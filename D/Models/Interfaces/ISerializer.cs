using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Models.Interfaces
{
    interface ISerializer
    {
        string Serialize(object value);
        object Deserialize(string value);
    }
}
