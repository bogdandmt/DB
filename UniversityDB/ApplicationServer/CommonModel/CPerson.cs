using ApplicationServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonModel
{
    [Serializable]
    public partial class CPerson : IObject
    {
        public CPerson(ApplicationServer.Person person)
        {
            ObjectID = person.ObjectID;
            DateOfBirth = person.DateOfBirth;
            Surname = person.Surname;

            //Learner = new CLearner(person.Learner);
            //Object = new CObject(person.Object);
        }

        public int ObjectID { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Surname { get; set; }

        public virtual CLearner Learner { get; set; }
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
