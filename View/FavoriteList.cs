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
    public partial class FavoriteList : Form
    {
        public FavoriteList()
        {
            InitializeComponent();
        }
        
        private void FavoriteList_Load(object sender, EventArgs e)
        {
            
            LoadPlayList();

        }
        public void LoadPlayList()
        {
            Text = "FavoriteList";
            button2.Enabled = true;
            button2.Visible = true;
            button3.Enabled = false;
            button3.Visible = false;
            button4.Enabled = false;
            button4.Visible = false;
            button5.Enabled = false;
            button5.Visible = false;
            button6.Enabled = false;
            button6.Visible = false;
            button7.Enabled = false;
            button7.Visible = false;
            flowLayoutPanel1.Controls.Clear();
            if (!Directory.Exists("PlayList/")) Directory.CreateDirectory("PlayList/");
            string[] playlists = Directory.GetFiles("PlayList/");
            if (playlists.Length == 0)
            {
                Label notfound = new Label();
                notfound.Text = "Плейлисты не найдены";
                notfound.AutoSize = true;
                flowLayoutPanel1.Controls.Add(notfound);
                return;
            }
            PlayListEl[] Cataloge = new PlayListEl[playlists.Length];
            for (int i = 0; i < playlists.Length; i++)
            {

                string n = playlists[i].Substring(9, playlists[i].Length - 14);
                string cover = "Covers/" + n + ".png";
                Cataloge[i] = new PlayListEl();
                Cataloge[i].PlayListName = n;
                if (File.Exists(cover))
                {
                    Cataloge[i].Cover = cover;
                }
                else
                {
                    Cataloge[i].Cover = "default.png";
                }
                Cataloge[i].FormActive = this;
                flowLayoutPanel1.Controls.Add(Cataloge[i]);

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Program.Context.MainForm = new Form1();
            this.Close();
            Program.Context.MainForm.Show();
        }
        public void LoadAllMusicList(string name)
        {
            button2.Enabled = false;
            button2.Visible = false;
            button3.Enabled = true;
            button3.Visible = true;
            button4.Enabled = true;
            button4.Visible = true;
            button5.Enabled = true;
            button5.Visible = true;
            button6.Enabled = true;
            button6.Visible = true;
            button7.Enabled = true;
            button7.Visible = true;
            JsonSave AllMusic = new JsonSave();
            AllList Major = new AllList();
            JsonSave FavoriteMusic = new JsonSave();
            MyFavoriteList MajoreFav = new MyFavoriteList();
            Text = name;
            Major.Load(ref AllMusic);
            MajoreFav.Load(ref FavoriteMusic, name, Major);
            flowLayoutPanel1.Controls.Clear();
            if (MajoreFav.Count == 0)
            {
                Label notfound = new Label();
                notfound.Text = "Плейлист пуст";
                notfound.AutoSize = true;
                flowLayoutPanel1.Controls.Add(notfound);
                return;
            }
            FavoriteEl[] Cataloge = new FavoriteEl[Major.CountMusic];
            for (int i = 0; i < MajoreFav.Count; i++)
            {


                Cataloge[i] = new FavoriteEl();
                Cataloge[i].ArtistName = MajoreFav[i].artist.name;
                Cataloge[i].AlbumName = MajoreFav[i].album.name + '(' + MajoreFav[i].years.ToString() + ')';
                Cataloge[i].MusicName =  MajoreFav[i].name;
                if (File.Exists(MajoreFav[i].album.Cover))
                {
                    Cataloge[i].Cover = MajoreFav[i].album.Cover;
                }
                else
                {
                    Cataloge[i].Cover = "default.png";
                }

                Cataloge[i].FormActive = this;
                flowLayoutPanel1.Controls.Add(Cataloge[i]);


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewPlaylist ok = new NewPlaylist();
            ok.FormActive = this;
            ok.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadPlayList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Вы точно хоитет удалить этот плейлист?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(res == DialogResult.Yes)
            {
                File.Delete("PlayList/" + Text + ".json");
                File.Delete("Covers/" + Text + ".png");
                LoadPlayList();
            }
            
        }

      

        private void button4_Click(object sender, EventArgs e)
        {
            EditPlayList ok = new EditPlayList();
            ok.FormActive = this;
            ok.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string file = openFileDialog1.FileName;
            if (file == "") return;
            if(File.Exists("Covers/" + Text + ".png"))
            {
                File.Delete("Covers/" + Text + ".png");
            }
            if (!Directory.Exists("Covers/")) Directory.CreateDirectory("Covers/");
            File.Copy(file, "Covers/" + Text + ".png");
            LoadPlayList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string file = openFileDialog1.FileName;
            JsonSave res = null;
            if (file == "") return;
            try
            {
                res = JsonConvert.DeserializeObject<JsonSave>(File.ReadAllText(file));
            }
            catch
            {
                MessageBox.Show("Неверновыбранный файл", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            JsonSave AllMusic = new JsonSave();
            AllList Major = new AllList();
            JsonSave FavoriteMusic = new JsonSave();
            MyFavoriteList MajoreFav = new MyFavoriteList();
            Major.Load(ref AllMusic);
            MajoreFav.Load(ref FavoriteMusic, Text, Major);

            for (int i = 0; i<res.Count; i++)
            {
                MajoreFav.AddToMyFavorite(Text, FavoriteMusic, Major, res[i].ArtistName, res[i].AlbumName, res[i].MusicName, res[i].Year);
            }
            LoadAllMusicList(Text);

        }
    }
}
