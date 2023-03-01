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
    public partial class NewPlaylist : Form
    {
        public NewPlaylist()
        {
            InitializeComponent();
        }
        private FavoriteList Okno;
        public FavoriteList FormActive
        {
            get { return Okno; }
            set { Okno = value; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(File.Exists("PlayList/" + textBox1.Text + ".json"))
            {
                MessageBox.Show("Плейлист с таким названием уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            JsonSave playlist = new JsonSave();
            try
            {
                File.WriteAllText("PlayList/" + textBox1.Text + ".json", JsonConvert.SerializeObject(playlist));

            }
            catch
            {
                MessageBox.Show("Имя плейлиста содержит запрещённые символы", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(pictureBox1.ImageLocation != "default.png")
            {
                if (!Directory.Exists("Covers/")) Directory.CreateDirectory("Covers/");
                File.Copy(pictureBox1.ImageLocation, "Covers/" + textBox1.Text + ".png");
            }
            
            FormActive.LoadPlayList();
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Поля осталось пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var result = MessageBox.Show("Вы точно хотите изменить обложку этого плейлиста?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                JsonSave AllMusic = new JsonSave();
                AllList Major = new AllList();
                Major.Load(ref AllMusic);



                openFileDialog1.ShowDialog();
                string file = openFileDialog1.FileName;
                if (file == "") return;
                pictureBox1.ImageLocation = file;



            }
        }
    }
}
