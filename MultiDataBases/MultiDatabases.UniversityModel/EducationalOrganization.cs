namespace MultiDatabases.UniversityModel
{
    public class EducationalOrganization
    {
        public int ObjectID { get; set; }
        public string Specialities { get; set; }

        public virtual Faculty Faculty { get; set; }
        public virtual Organization Organization { get; set; }
    }
}