using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationServer
{
    [Serializable]
    public class DBMessage
    {
        public MessageType MessageType { get; set; }
        public IObject Entity { get; set; }
        public List<IObject> Entities { get; set; }
        public EntityType EntityType { get; set; }
        public String FormName { get; set; }
        public int ObjectID { get; set; }

        //public MessageType Type
        //{
        //    get { return type; }
        //    set { type = value; }
        //}

        //public IObject Entity
        //{
        //    get { return entity; }
        //    set { entity = value; }
        //}

        //public List<IObject> Entities
        //{
        //    get { return entities; }
        //    set { entities = value; }
        //}

        public DBMessage(MessageType type)
        {
            this.MessageType = type;
            //System.Object o = new Faculty();
        }
    }

    [Serializable]
    public enum MessageType
    {
        GetEntities,
        GetEntityInfo,
        InsertEntity,
        UpdateEntity,
        DeleteEntity,
        GetFormName
    }

    [Serializable]
    public enum EntityType
    {
        Object,
        Folder,
        Organization,
        EducationalOrganization,
        Faculty,
        Person,
        Learner,
        Student
    }

    public interface IObject
    {
        int ObjectID { get; }
        Nullable<int> ClassID { get; }
        Nullable<int> MajorID { get; }
        string Name { get; }
    }
}
