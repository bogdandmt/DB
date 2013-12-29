using System.Collections.Generic;
using System.ServiceModel;
using MultiDatabases.UniversityModel;


namespace MultiDataBases.Core
{
    [ServiceContract(Namespace = "http://localhost:8080/MultiDatabase")]
    public interface IDatabaseService
    {
        [OperationContract]
        ICollection<Faculty> GetFaculties();

        [OperationContract]
        ICollection<Person> GetPersons();

        [OperationContract]
        ICollection<EducationalOrganization> GetEducationalOrganization();

        [OperationContract]
        ICollection<Learner> GetLearner();

        [OperationContract]
        ICollection<Organization> GetOrganization();

        [OperationContract]
        ICollection<Student> GetStudent();
    }
}