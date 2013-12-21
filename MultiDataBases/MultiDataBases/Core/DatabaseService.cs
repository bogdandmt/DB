using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.ServiceModel.Channels;

namespace MultiDataBases.Core
{
    public class DatabaseService : IDatabaseService
    {
        private readonly UniversityDBEntities m_entityEntities = new UniversityDBEntities();

        public IEnumerable<Faculty> GetObjects()
        {
            return m_entityEntities.Faculties;
        }
    }
}