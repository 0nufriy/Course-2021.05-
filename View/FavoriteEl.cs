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
    public partial class FavoriteEl : UserControl
    {
        public FavoriteEl()
        {
            InitializeComponent();
        }
        private string ArtN;
        private string AlbN;
        private string MusN;
        private string Cov;
        private FavoriteList Okno;
        public FavoriteList FormActive
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
            set { Cov = value; pictureBox1.ImageLocation = value; }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            var result = MessageBox.Show("Вы точно хотите удалить эту песню из плейлиста?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                JsonSave AllMusic = new JsonSave();
                AllList Major = new AllList();
                JsonSave FavoriteMusic = new JsonSave();
                MyFavoriteList MajoreFav = new MyFavoriteList();
                Major.Load(ref AllMusic);
                MajoreFav.Load(ref FavoriteMusic, FormActive.Text, Major);

                string albna = AlbumName.Substring(0, AlbumName.Length - 6);
                int year = Int32.Parse(AlbumName.Substring(AlbumName.Length - 5, 4));
                MajoreFav.RemoveAtMyFavorite(FormActive.Text,FavoriteMusic, ArtistName, albna, MusicName, year);
                
                FormActive.LoadAllMusicList(FormActive.Text);
                MessageBox.Show("Песня успешно удалена из плейлиста", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
