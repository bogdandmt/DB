using ApplicationServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonModel
{
    [Serializable]
    public partial class CEducationalOrganization : IObject
    {
        public CEducationalOrganization(ApplicationServer.EducationalOrganization eduOrg)
        {
            ObjectID = eduOrg.ObjectID;
            Specialities = eduOrg.Specialities;

            //Organization = new COrganization(eduOrg.Organization);
            //Faculty = new CFaculty(eduOrg.Faculty);
        }

        public int ObjectID { get; set; }
        public string Specialities { get; set; }

        public virtual COrganization Organization { get; set; }
        public virtual CFaculty Faculty { get; set; }


        public Nullable<int> ClassID
        {
            get { return Organization.Object.ClassID; }
        }

        public Nullable<int> MajorID
        {
            get { return Organization.Object.MajorID; }
        }

        public string Name
        {
            get { return Organization.Object.Name; }
        }
    }
}
