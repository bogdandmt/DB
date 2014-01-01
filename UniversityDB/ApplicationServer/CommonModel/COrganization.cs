using ApplicationServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonModel
{
    [Serializable]
    public partial class COrganization : IObject
    {
        public COrganization(ApplicationServer.Organization org)
        {
            ObjectID = org.ObjectID;
            Phone = org.Phone;
            Website = org.Website;

            //EducationalOrganization = new CEducationalOrganization(org.EducationalOrganization);
            //Object = new CObject(org.Object);
        }

        public int ObjectID { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }

        public virtual CEducationalOrganization EducationalOrganization { get; set; }
        public virtual CObject Object { get; set; }


        public Nullable<int> ClassID
        {
            get { return Object.ClassID; }
        }

        public Nullable<int> MajorID
        {
            get { return Object.MajorID; }
        }

        public string Name
        {
            get { return Object.Name; }
        }
    }
}
