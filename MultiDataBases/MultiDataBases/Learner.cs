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
    
    public partial class Learner
    {
        public int ObjectID { get; set; }
        public Nullable<double> AvgMark { get; set; }
    
        public virtual Student Student { get; set; }
        public virtual Person Person { get; set; }
    }
}