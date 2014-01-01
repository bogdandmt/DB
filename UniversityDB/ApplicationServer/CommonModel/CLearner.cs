using ApplicationServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonModel
{
    [Serializable]
    public partial class CLearner : IObject
    {
        public CLearner(ApplicationServer.Learner learner)
        {
            ObjectID = learner.ObjectID;
            AvgMark = learner.AvgMark;

            //Person = new CPerson(learner.Person);
            //Student = new CStudent(learner.Student);
        }

        public int ObjectID { get; set; }
        public Nullable<double> AvgMark { get; set; }

        public virtual CPerson Person { get; set; }
        public virtual CStudent Student { get; set; }


        public Nullable<int> ClassID
        {
            get { return Person.Object.ClassID; }
        }

        public Nullable<int> MajorID
        {
            get { return Person.Object.MajorID; }
        }

        public string Name
        {
            get { return Person.Object.Name; }
        }
    }
}
