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
    public partial class MusicEl : UserControl
    {
        public MusicEl()
        {
            InitializeComponent();
        }

        
        private string ArtN;
        private string AlbN;
        private string MusN;
        private string Cov;
        private ListAllMusic Okno;
        public ListAllMusic FormActive
        {
            get { return Okno; }
            set { Okno = value; }
        }

        public string ArtistName
        {
            get { return ArtN; }
            set { ArtN = value; label3.Text = value; }
        }
        
        public string AlbumName
        {
            get { return AlbN; }
            set { AlbN = value; label2.Text = value; }
        }
        
        public string MusicName
        {
            get { return MusN; }
            set { MusN = value; label1.Text = value; }
        }
        
        public string Cover
        {
            get { return Cov; }
            set
            {
                Cov = value;
                pictureBox1.ImageLocation = value;
            }
        }
        

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы точно хотите удалить эту песню?","Внимание!",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes  )
            {
                JsonSave AllMusic = new JsonSave();
                AllList Major = new AllList();
                Major.Load( ref AllMusic);
                string albna = AlbumName.Substring(0, AlbumName.Length - 6);
                int year = Int32.Parse(AlbumName.Substring(AlbumName.Length - 5, 4));
                Major.RomoveAtList(AllMusic, ArtistName, albna, MusicName, year);

                FormActive.RefreshMy();
                MessageBox.Show("Песня успешно удалена", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if(!Directory.Exists("PlayList/")) Directory.CreateDirectory("PlayList/");
            SelectPlayList ok = new SelectPlayList();
            ok.MusicElementActive = this;
            ok.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы точно хотите изменить обложку этого альлома?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                JsonSave AllMusic = new JsonSave();
                AllList Major = new AllList();
                Major.Load(ref AllMusic);

                string albna = AlbumName.Substring(0, AlbumName.Length - 6);
                int year = Int32.Parse(AlbumName.Substring(AlbumName.Length - 5, 4));

                openFileDialog1.ShowDialog();
                string file = openFileDialog1.FileName;
                if (file == "") return;
                AllList.ChangeCover(file, ArtistName, albna);
                
                FormActive.RefreshMy();
                
            }
        }
    }
}

