using ApplicationServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonModel
{
    [Serializable]
    public partial class CStudent : IObject
    {
        public CStudent(ApplicationServer.Student student)
        {
            ObjectID = student.ObjectID;
            StudentCardNumber = student.StudentCardNumber;

            //Learner = new CLearner(student.Learner);
        }

        public int ObjectID { get; set; }
        public string StudentCardNumber { get; set; }

        public virtual CLearner Learner { get; set; }


        public Nullable<int> ClassID
        {
            get { return Learner.Person.Object.ClassID; }
        }

        public Nullable<int> MajorID
        {
            get { return Learner.Person.Object.MajorID; }
        }

        public string Name
        {
            get { return Learner.Person.Object.Name; }
        }
    }
}
