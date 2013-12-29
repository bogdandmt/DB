using System;

namespace MultiDatabases.UniversityModel
{
    public class Person
    {
        public int ObjectID { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Surname { get; set; }

        public virtual Learner Learner { get; set; }
        public virtual BaseObject Object { get; set; }
    }
}