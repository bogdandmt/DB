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
    public partial class Student : IObject
    {
        public int ObjectID { get; set; }
        public string StudentCardNumber { get; set; }
    
        public virtual Learner Learner { get; set; }


        public Nullable<int> ClassID
        {
            get { return Learner.Person.Object.ClassID; }
        }

        public Nullable<int> MajorID
        {
            get { return Learner.Person.Object.MajorID; }
        }

        public string Name
        {
            get { return Learner.Person.Object.Name; }
        }
    }
}
