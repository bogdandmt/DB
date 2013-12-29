namespace MultiDatabases.UniversityModel
{
    public class Faculty
    {
        public int ObjectID { get; set; }
        public string Dean { get; set; }

        public virtual EducationalOrganization EducationalOrganization { get; set; }
    }
}