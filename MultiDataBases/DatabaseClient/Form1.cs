using System.Windows.Forms;
using DatabaseClient.Core;

namespace DatabaseClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var manager = new DatabaseManager();
        }
    }
}