using ApplicationServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using Client;
using System.Reflection;
using CommonModel;

namespace Client
{
    public partial class MainForm : Form
    {
        private TcpClient client = default(TcpClient);
        private BinaryFormatter formatter = new BinaryFormatter();

        public MainForm()
        {
            InitializeComponent();
        }

        public TcpClient ConnectToServer(IPAddress ip, int port)
        {
            var client = new TcpClient();
            client.Connect(ip, port);
            return client;
        }

        private void connectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            client = ConnectToServer(IPAddress.Parse("127.0.0.1"), 5000);
            loadRootNodes();
        }

        private void loadRootNodes()
        {
            treeView.Nodes.Clear();

            foreach (IObject o in getObjectsWithMajor(null))
            {
                TreeNode node = new TreeNode();
                node.Tag = o.ObjectID;
                node.Text = o.Name;
                TreeNode dummyNode = new TreeNode("Loading. Please wait...");
                dummyNode.Tag = "dummy";
                node.Nodes.Add(dummyNode);
                treeView.Nodes.Add(node);
            }
            treeView.ShowPlusMinus = true;
        }

        private List<IObject> getObjectsWithMajor(Nullable<int> majorID)
        {
            //Assembly.LoadFrom("ApplicationServer.exe");
            List<IObject> result;
            DBMessage message = new DBMessage(MessageType.GetEntities);
            formatter.Serialize(client.GetStream(), message);
            DBMessage messageFromServer = (DBMessage)formatter.Deserialize(client.GetStream());
            List<IObject> allObjects = messageFromServer.Entities;
            result = allObjects.Where(o => o.MajorID == majorID).ToList();
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //DBMessage message = new DBMessage("qw");
            //formatter.Serialize(client.GetStream(), message);

            //DBMessage messageFromServer = (DBMessage)formatter.Deserialize(client.GetStream());
            //MessageBox.Show(messageFromServer.Text);

            UniversityDBEntities entities = new UniversityDBEntities();
            //Faculty f = entities.Faculties.ToList().ElementAt(0);
            //MessageBox.Show(f.Dean);

            ApplicationServer.Object o = new ApplicationServer.Object()
            {
                ObjectID = 3,
                MajorID = 1
            };

            //AObject ao = (AObject)o;
            IObject io = new Faculty();

            MessageBox.Show(o.GetType().ToString());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode parentNode = e.Node;
            parentNode.Nodes.RemoveAt(0);
            List<IObject> children = getObjectsWithMajor((int)parentNode.Tag);
            foreach (IObject child in children)
            {
                TreeNode childNode = new TreeNode();
                childNode.Tag = child.ObjectID;
                childNode.Text = child.Name;
                TreeNode dummyNode = new TreeNode("Loading. Please wait...");
                dummyNode.Tag = "dummy";
                childNode.Nodes.Add(dummyNode);
                parentNode.Nodes.Add(childNode);
            }
        }

        private void treeView_MouseClick(object sender, MouseEventArgs e)
        {
            if (treeView.Nodes.Count > 0)
            {
                treeView.SelectedNode = treeView.Nodes[0];
            }
            if (e.Button == MouseButtons.Right && treeView.SelectedNode != null)
            {
                treeView.SelectedNode = treeView.GetNodeAt(e.Location);
                contextMenuStrip.Show(treeView, e.Location);
                //MessageBox.Show(treeView.SelectedNode.Tag.ToString());
            }
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowEditForm();
        }

        private void ShowEditForm()
        {
            DBMessage requestFormName = new DBMessage(MessageType.GetFormName);
            int objectID = (int)treeView.SelectedNode.Tag;
            requestFormName.ObjectID = objectID;
            formatter.Serialize(client.GetStream(), requestFormName);
            DBMessage responseFormName = (DBMessage)formatter.Deserialize(client.GetStream());

            Form f;
            Type t = Type.GetType("Client." + responseFormName.FormName);
            f = (Form)Activator.CreateInstance(t);

            DBMessage requestEntInfo = new DBMessage(MessageType.GetEntityInfo);
            requestEntInfo.ObjectID = objectID;
            formatter.Serialize(client.GetStream(), requestEntInfo);
            DBMessage responseEntInfo = (DBMessage)formatter.Deserialize(client.GetStream());
            fillForm(responseEntInfo, f);
            //f.ShowDialog();
        }

        private void fillForm(DBMessage response, Form form)
        {
            IObject entity = response.Entity;
            switch (response.EntityType)
            {
                case EntityType.Object:
                    ObjectForm objForm = (ObjectForm)form;
                    fillObjectForm(entity, objForm);
                    break;

                case EntityType.Folder:
                    FolderForm folderForm = (FolderForm)form;
                    fillFolderForm(entity, folderForm);
                    break;

                case EntityType.Organization:
                    OrganizationForm orgForm = (OrganizationForm)form;
                    fillOrganizationForm(entity, orgForm);
                    break;

                case EntityType.EducationalOrganization:
                    EducationalOrganizationForm edOrgForm = (EducationalOrganizationForm)form;
                    fillEduOrganizationForm(entity, edOrgForm);
                    break;

                case EntityType.Faculty:
                    FacultyForm facultyForm = (FacultyForm)form;
                    fillFacultyForm(entity, facultyForm);
                    break;

                case EntityType.Person:
                    PersonForm personForm = (PersonForm)form;
                    fillPersonForm(entity, personForm);
                    break;

                case EntityType.Learner:
                    LearnerForm learnerForm = (LearnerForm)form;
                    fillLearnerForm(entity, learnerForm);
                    break;

                case EntityType.Student:
                    StudentForm studentForm = (StudentForm)form;
                    fillStudentForm(entity, studentForm);
                    break;
            }
            form.ShowDialog();
        }

        private void fillObjectForm(IObject entity, ObjectForm form)
        {
            form.nameTextBox.Text = entity.Name;
        }

        private void fillFolderForm(IObject entity, FolderForm form)
        {
            fillObjectForm(entity, form);
            form.connStrTextBox.Text = ((CFolder)entity).ConnectionString;
        }

        private void fillOrganizationForm(IObject entity, OrganizationForm form)
        {
            fillObjectForm(entity, form);
            form.phoneTextBox.Text = ((COrganization)entity).Phone;
            form.websiteTextBox.Text = ((COrganization)entity).Website;
        }

        private void fillEduOrganizationForm(IObject entity, EducationalOrganizationForm form)
        {
            fillOrganizationForm(((CEducationalOrganization)entity).Organization, form);
            form.specialitiesTextBox.Text = ((CEducationalOrganization)entity).Specialities;
        }

        private void fillFacultyForm(IObject entity, FacultyForm form)
        {
            fillEduOrganizationForm(((CFaculty)entity).EducationalOrganization, form);
            form.deanTextBox.Text = ((CFaculty)entity).Dean;
        }

        private void fillPersonForm(IObject entity, PersonForm form)
        {
            fillObjectForm(entity, form);
            form.surnameTextBox.Text = ((CPerson)entity).Surname;
            form.dateTimePicker.Text = ((CPerson)entity).DateOfBirth.ToString();
        }

        private void fillLearnerForm(IObject entity, LearnerForm form)
        {
            fillPersonForm(((CLearner)entity).Person, form);
            form.avgMarkTextBox.Text = ((CLearner)entity).AvgMark.ToString();
        }

        private void fillStudentForm(IObject entity, StudentForm form)
        {
            fillLearnerForm(((CStudent)entity).Learner, form);
            form.studCardNumbtextBox.Text = ((CStudent)entity).StudentCardNumber;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
