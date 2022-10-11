using System.Windows.Forms;

namespace МорскойБой
{
    public partial class VictroryGame : Form
    {
        public VictroryGame(string str)
        {
            InitializeComponent();
            label1.Text = str;
        }

        private void VictroryGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void VictroryGame_Load(object sender, System.EventArgs e)
        {

        }
    }
}
