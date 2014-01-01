using ApplicationServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonModel
{
    [Serializable]
    public partial class CFolder : IObject
    {
        public CFolder(ApplicationServer.Folder folder)
        {
            ObjectID = folder.ObjectID;
            ConnectionString = folder.ConnectionString;

            //Object = new CObject(folder.Object);
        }

        public int ObjectID { get; set; }
        public string ConnectionString { get; set; }

        public virtual CObject Object { get; set; }


        public Nullable<int> ClassID
        {
            get { return Object.ClassID; }
        }

        public Nullable<int> MajorID
        {
            get { return Object.MajorID; }
        }

        public string Name
        {
            get { return Object.Name; }
        }
    }
}
