using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiDatabases.UniversityModel
{
    public class BaseObject
    {
        public int BaseObjectID { get; set; }
        public int? ClassID { get; set; }
        public int? MajorID { get; set; }
        public string Name { get; set; }

        public virtual Person Person { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
