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
    public partial class SelectPlayList : Form
    {
        public SelectPlayList()
        {
            InitializeComponent();
        }
        private MusicEl Okno;
        public MusicEl MusicElementActive
        {
            get { return Okno; }
            set { Okno = value; }
        }
        private void SelectPlayList_Load(object sender, EventArgs e)
        {
            string[] l = Directory.GetFiles("PlayList/");
            for (int i = 0; i < l.Length; i++)
            {
                l[i] = l[i].Substring(9, l[i].Length - 14);
                comboBox1.Items.Add(l[i]);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(File.Exists("Covers/" + comboBox1.Text + ".png"))
            {
                pictureBox1.ImageLocation = "Covers/" + comboBox1.Text + ".png";
            }
            else
            {
                pictureBox1.ImageLocation = "default.png";
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "") return;
            for (int i = 0; i < comboBox1.Text.Length; i++)
            {
                if (comboBox1.Text[i] == '/' || comboBox1.Text[i] == '\\' || comboBox1.Text[i] == ':' || comboBox1.Text[i] == '*' || comboBox1.Text[i] == '?' || comboBox1.Text[i] == '"' || comboBox1.Text[i] == '<' || comboBox1.Text[i] == '>' || comboBox1.Text[i] == '|' || comboBox1.Text[i] == '+')
                {
                    MessageBox.Show("Имя группы не должно содержать символы: /,\\,:,*,?,<,>,|,+ и т.п.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            JsonSave AllMusic = new JsonSave();
            AllList Major = new AllList();
            JsonSave FavoriteMusic = new JsonSave();
            MyFavoriteList MajoreFav = new MyFavoriteList();
            Major.Load(ref AllMusic);
            MajoreFav.Load(ref FavoriteMusic, comboBox1.Text, Major);
            string albna = MusicElementActive.AlbumName.Substring(0, MusicElementActive.AlbumName.Length - 6);
            int year = Int32.Parse(MusicElementActive.AlbumName.Substring(MusicElementActive.AlbumName.Length - 5, 4));
            int shet = FavoriteMusic.Count;
            MajoreFav.AddToMyFavorite(comboBox1.Text, FavoriteMusic, Major, MusicElementActive.ArtistName, albna, MusicElementActive.MusicName, year);
            if (shet == FavoriteMusic.Count)
            {
                MessageBox.Show("Данная песня уже была в этом плейлисте", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Песня успешно добавлена в плейлист", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            
        }
    }
}
