using System.Windows.Forms;
using DatabaseClient.Core;

namespace DatabaseClient
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var manager = new DatabaseManager();
        }
    }
}