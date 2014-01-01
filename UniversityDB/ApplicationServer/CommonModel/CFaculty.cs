using ApplicationServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonModel
{
    [Serializable]
    public partial class CFaculty : IObject
    {
        public CFaculty(ApplicationServer.Faculty faculty)
        {
            ObjectID = faculty.ObjectID;
            Dean = faculty.Dean;

            //EducationalOrganization = new CEducationalOrganization(faculty.EducationalOrganization);
        }

        public int ObjectID { get; set; }
        public string Dean { get; set; }

        public virtual CEducationalOrganization EducationalOrganization { get; set; }


        public Nullable<int> ClassID
        {
            get { return EducationalOrganization.Organization.Object.ClassID; }
        }

        public Nullable<int> MajorID
        {
            get { return EducationalOrganization.Organization.Object.MajorID; }
        }

        public string Name
        {
            get { return EducationalOrganization.Organization.Object.Name; }
        }
    }
}
