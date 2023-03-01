using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace Course
{
    public partial class PlayListEl : UserControl
    {
        public PlayListEl()
        {
            InitializeComponent();
        }
        private FavoriteList Okno;
        public FavoriteList FormActive
        {
            get { return Okno; }
            set { Okno = value; }
        }
        private string N;
        private string C;
        public string PlayListName
        {
            get { return N; }
            set { N = value; label1.Text = value; }
        }

        public string Cover
        {
            get { return C; }
            set { C = value; pictureBox1.ImageLocation = value; }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormActive.LoadAllMusicList(PlayListName);
        }
    }
}
