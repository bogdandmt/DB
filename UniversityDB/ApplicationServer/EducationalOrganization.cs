//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ApplicationServer
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public partial class EducationalOrganization : IObject
    {
        public int ObjectID { get; set; }
        public string Specialities { get; set; }
    
        public virtual Organization Organization { get; set; }
        public virtual Faculty Faculty { get; set; }


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
