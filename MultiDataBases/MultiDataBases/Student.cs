//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MultiDataBases
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public int ObjectID { get; set; }
        public string StudentCardNumber { get; set; }
    
        public virtual Learner Learner { get; set; }
    }
}