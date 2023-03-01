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
    public partial class EditPlayList : Form
    {
        public EditPlayList()
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
            if(textBox1.Text == FormActive.Text)
            {
                Close();
                return;
            }
            if (File.Exists("PlayList/" + textBox1.Text + ".json"))
            {
                MessageBox.Show("Плейлист с таким названием уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                File.Copy("PlayList/" + FormActive.Text + ".json", "PlayList/" + textBox1.Text + ".json");

            }
            catch
            {
                MessageBox.Show("Имя плейлиста содержит запрещённые символы", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            File.Delete("PlayList/" + FormActive.Text + ".json");
            if(File.Exists("Covers/" + FormActive.Text + ".png"))
            {
                File.Copy("Covers/" + FormActive.Text + ".png", "Covers/" + textBox1.Text + ".png");
                File.Delete("Covers/" + FormActive.Text + ".png");
            }
            
            FormActive.LoadPlayList();
            Close();
        }

        private void EditPlayList_Load(object sender, EventArgs e)
        {
            textBox1.Text = FormActive.Text;
          

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
