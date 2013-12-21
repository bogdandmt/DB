using System.Collections.Generic;
using System.ServiceModel;


namespace MultiDataBases.Core
{
    [ServiceContract(Namespace = "http://localhost:8080/MultiDatabase")]
    public interface IDatabaseService
    {
        [OperationContract]
        IEnumerable<Faculty> GetObjects();
    }
}