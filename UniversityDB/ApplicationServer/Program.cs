using CommonModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace ApplicationServer
{
    class Program
    {
        public static TcpClient clientSocketForServer = default(TcpClient);
        private static UniversityDBEntities entities = new UniversityDBEntities();

        public static void HandleClient()
        {
            try
            {
                while (true)
                {
                    BinaryFormatter binFormatter = new BinaryFormatter();
                    DBMessage request = (DBMessage)binFormatter.Deserialize(clientSocketForServer.GetStream());

                    if (request == null)
                    {
                        throw new IOException();
                    }
                    DBMessage response = null;
                    int classId = 0;
                    switch (request.MessageType)
                    {
                        case MessageType.GetEntities:
                            response = new DBMessage(MessageType.GetEntities);
                            List<IObject> responseList = new List<IObject>();
                            foreach (Object o in entities.Objects.ToList())
                            {
                                CommonModel.CObject commonObj = new CommonModel.CObject(o);
                                responseList.Add(commonObj);
                            }
                            response.Entities = responseList;
                            break;

                        case MessageType.GetFormName:
                            response = new DBMessage(MessageType.GetEntityInfo);
                            classId = (int)entities.Objects.Where(e => e.ObjectID == request.ObjectID).ToList().ElementAt(0).ClassID;
                            response.FormName = entities.Classes.Where(e => e.ClassId == classId).ToList().ElementAt(0).Formname;
                            break;

                        case MessageType.GetEntityInfo:
                            response = getEntity(request.ObjectID);                   
                            break;

                    }
                    //DBMessage response = new DBMessage(commandFromClient.Text + " return from server");
                    binFormatter.Serialize(clientSocketForServer.GetStream(), response);
                }
            }
            catch (IOException)
            {
            }
        }

        private static DBMessage getEntity(int objectId)
        {
            DBMessage response = new DBMessage(MessageType.GetEntityInfo);
            int classId = (int)entities.Objects.Where(e => e.ObjectID == objectId).ToList().ElementAt(0).ClassID;
            IObject entity = null;
            switch (classId)
            {
                case 1:
                    entity = new CObject(entities.Objects.Where(e => e.ObjectID == objectId).ToList().ElementAt(0));
                    response.EntityType = EntityType.Object;
                    break;

                case 2:
                    entity = new CFolder(entities.Folders.Where(e => e.ObjectID == objectId).ToList().ElementAt(0))
                    {
                        Object = new CObject(entities.Objects.Where(e => e.ObjectID == objectId).ToList().ElementAt(0))
                    };
                    response.EntityType = EntityType.Folder;
                    break;

                case 3:
                    entity = new COrganization(entities.Organizations.Where(e => e.ObjectID == objectId).ToList().ElementAt(0))
                    {
                        Object = new CObject(entities.Objects.Where(e => e.ObjectID == objectId).ToList().ElementAt(0))
                    };
                    response.EntityType = EntityType.Organization;
                    break;

                case 4:
                    entity = new CEducationalOrganization(entities.EducationalOrganizations.Where(e => e.ObjectID == objectId).ToList().ElementAt(0))
                    {
                        Organization = new COrganization(entities.Organizations.Where(e => e.ObjectID == objectId).ToList().ElementAt(0))
                        {
                            Object = new CObject(entities.Objects.Where(e => e.ObjectID == objectId).ToList().ElementAt(0))
                        }
                    };
                    response.EntityType = EntityType.EducationalOrganization;
                    break;

                case 5:
                    entity = new CFaculty(entities.Faculties.Where(e => e.ObjectID == objectId).ToList().ElementAt(0))
                    {
                        EducationalOrganization = new CEducationalOrganization(entities.EducationalOrganizations.Where(e => e.ObjectID == objectId).ToList().ElementAt(0))
                        {
                            Organization = new COrganization(entities.Organizations.Where(e => e.ObjectID == objectId).ToList().ElementAt(0))
                            {
                                Object = new CObject(entities.Objects.Where(e => e.ObjectID == objectId).ToList().ElementAt(0))
                            }
                        }
                    };
                    response.EntityType = EntityType.Faculty;
                    break;

                case 6:
                    entity = new CPerson(entities.Persons.Where(e => e.ObjectID == objectId).ToList().ElementAt(0))
                    {
                        Object = new CObject(entities.Objects.Where(e => e.ObjectID == objectId).ToList().ElementAt(0))
                    };
                    response.EntityType = EntityType.Person;
                    break;

                case 7:
                    entity = new CLearner(entities.Learners.Where(e => e.ObjectID == objectId).ToList().ElementAt(0))
                    {
                        Person = new CPerson(entities.Persons.Where(e => e.ObjectID == objectId).ToList().ElementAt(0))
                        {
                            Object = new CObject(entities.Objects.Where(e => e.ObjectID == objectId).ToList().ElementAt(0))
                        }
                    };
                    response.EntityType = EntityType.Learner;
                    break;

                case 8:
                    entity = new CStudent(entities.Students.Where(e => e.ObjectID == objectId).ToList().ElementAt(0))
                    {
                        Learner = new CLearner(entities.Learners.Where(e => e.ObjectID == objectId).ToList().ElementAt(0))
                        {
                            Person = new CPerson(entities.Persons.Where(e => e.ObjectID == objectId).ToList().ElementAt(0))
                            {
                                Object = new CObject(entities.Objects.Where(e => e.ObjectID == objectId).ToList().ElementAt(0))
                            }
                        }
                    };
                    response.EntityType = EntityType.Student;
                    break;
            }
            response.Entity = entity;
            return response;
        }

        public static TcpClient WaitAndAcceptClient(CancellationToken token)
        {
            var tcpListener = new TcpListener(IPAddress.Any, 5000);
            TcpClient client = null;
            tcpListener.Start();
            while (token.IsCancellationRequested)
            {
                client = tcpListener.AcceptTcpClient();
                var stream = client.GetStream();
                break;
            }
            tcpListener.Stop();
            return client;
        }

        static void Main(string[] args)
        {
            //UniversityDBEntities e = new UniversityDBEntities();
            //Faculty faculty = new Faculty()
            //{
            //    Dean = "Vovk",
            //    EducationalOrganization = new EducationalOrganization()
            //    {
            //        Specialities = "specialities",
            //        Organization = new Organization()
            //        {
            //            Phone = "32456874",
            //            Website = "lnu.edu.ua",
            //            Object = new Object()
            //            {
            //                ObjectID = 2,
            //                MajorID = 1,
            //                Name = "AMI_CHILD"
            //            }
            //        }
            //    }
            //};
            //e.Faculties.Add(faculty);
            //e.SaveChanges();

            //UniversityDBEntities e = new UniversityDBEntities();
            //Student st = new Student()
            //{
            //    ObjectID = 3,
            //    Learner = new Learner()
            //    {
            //        Person = new Person()
            //        {
            //            Object = new Object()
            //            {
            //                MajorID = 2,
            //                ClassID = 8,
            //                Name = "Vasya"
            //            }
            //        },
            //        AvgMark = 5
            //    },
            //    StudentCardNumber = "bk23435"
            //};
            //e.Students.Add(st);
            //e.SaveChanges();

            //Faculty f = e.Faculties.ToList().ElementAt(0);
            //List<Faculty> fl = e.Faculties.ToList();
            //Console.WriteLine(f.Dean + ' ' + f.EducationalOrganization.Organization.Object.Name);
            //e.Objects.Remove(e.Objects.Find(f.ObjectID));
            //e.Faculties.Remove(f);
            //e.SaveChanges();

            Console.WriteLine("Server  start");
            clientSocketForServer = WaitAndAcceptClient(new CancellationToken(true));
            Console.WriteLine("Connect success");
            HandleClient();
        }
    }
}
