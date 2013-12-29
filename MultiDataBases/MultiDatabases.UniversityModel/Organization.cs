namespace MultiDatabases.UniversityModel
{
    public class Organization
    {
        public int ObjectID { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }

        public virtual EducationalOrganization EducationalOrganization { get; set; }
        public virtual BaseObject Object { get; set; }
    }
}