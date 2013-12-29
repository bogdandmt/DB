namespace MultiDatabases.UniversityModel
{
    public class Learner
    {
        public int ObjectID { get; set; }
        public double? AvgMark { get; set; }

        public virtual Student Student { get; set; }
        public virtual Person Person { get; set; }
    }
}