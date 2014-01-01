using ApplicationServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonModel
{
    [Serializable]
    public partial class CObject : IObject
    {
        public CObject(ApplicationServer.Object obj)
        {
            ObjectID = obj.ObjectID;
            ClassID = obj.ClassID;
            MajorID = obj.MajorID;
            Name = obj.Name;

            //Folder = new CFolder(obj.Folder);
            //Organization = new COrganization(obj.Organization);
            //Person = new CPerson(obj.Person);
        }

        public int ObjectID { get; set; }
        public Nullable<int> ClassID { get; set; }
        public Nullable<int> MajorID { get; set; }
        public string Name { get; set; }

        public virtual CFolder Folder { get; set; }
        public virtual COrganization Organization { get; set; }
        public virtual CPerson Person { get; set; }
    }
}
