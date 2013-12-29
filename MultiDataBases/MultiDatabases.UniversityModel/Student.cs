namespace MultiDatabases.UniversityModel
{
    public class Student
    {
        public int ObjectID { get; set; }
        public string StudentCardNumber { get; set; }

        public virtual Learner Learner { get; set; }
    }
}