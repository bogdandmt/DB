using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.ServiceModel.Channels;
using MultiDatabases.UniversityModel;

namespace MultiDataBases.Core
{
    public class DatabaseService : IDatabaseService
    {
        private readonly MSUniversityDBEntities m_entities = new MSUniversityDBEntities();

        public ICollection<Faculty> GetFaculties()
        {
            return m_entities.Faculties.ToList();
        }

        public ICollection<Person> GetPersons()
        {
            return m_entities.Persons.ToList();
        }

        public ICollection<EducationalOrganization> GetEducationalOrganization()
        {
            return m_entities.EducationalOrganizations.ToList();
        }

        public ICollection<Learner> GetLearner()
        {
            return m_entities.Learners.ToList();
        }

        public ICollection<Organization> GetOrganization()
        {
            return m_entities.Organizations.ToList();
        }

        public ICollection<Student> GetStudent()
        {
            return m_entities.Students.ToList();
        }
    }
}