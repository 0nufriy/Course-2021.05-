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
    public partial class AddToList : Form
    {
        public AddToList()
        {
            InitializeComponent();
        }
        JsonSave AllMusic = new JsonSave();
        AllList Major = new AllList();
        private ListAllMusic Okno;
        public ListAllMusic FormActive
        {
            get { return Okno; }
            set { Okno = value; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Major.Load(ref AllMusic);

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Некоторые поля остались пустыми", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int Year = 0;
            try
            {
                Year = Int32.Parse(textBox4.Text);
            }
            catch
            {
                MessageBox.Show("Недопустимый формат года", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Year < 1000 || Year>9999)
            {
                MessageBox.Show("Недопустимый формат года", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for(int i = 0; i < textBox1.Text.Length;i++)
            {
                if(textBox1.Text[i] == '/' || textBox1.Text[i] == '\\' || textBox1.Text[i] == ':' || textBox1.Text[i] == '*' || textBox1.Text[i] == '?' || textBox1.Text[i] == '"' || textBox1.Text[i] == '<' || textBox1.Text[i] == '>' || textBox1.Text[i] == '|' || textBox1.Text[i] == '+')
                {
                    MessageBox.Show("Имя группы не должно содержать символы: /,\\,:,*,?,<,>,|,+ и т.п.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            for (int i = 0; i < textBox2.Text.Length; i++)
            {
                if (textBox2.Text[i] == '/' || textBox2.Text[i] == '\\' || textBox2.Text[i] == ':' || textBox2.Text[i] == '*' || textBox2.Text[i] == '?' || textBox2.Text[i] == '"' || textBox2.Text[i] == '<' || textBox2.Text[i] == '>' || textBox2.Text[i] == '|' || textBox2.Text[i] == '+')
                {
                    MessageBox.Show("Название альбома не должно содержать символы: /,\\,:,*,?,<,>,|,+ и т.п.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                    
            }
            
            Major.AddToList(AllMusic, textBox1.Text, textBox2.Text, textBox3.Text, Year);
            MessageBox.Show("Песня успешно добавлена в список","Уведомление",MessageBoxButtons.OK,MessageBoxIcon.Information);
            FormActive.RefreshMy();
            
        }

        

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists("Covers/"+textBox1.Text +'.'+textBox2.Text + ".png"))
            {
                pictureBox1.ImageLocation = "Covers/" + textBox1.Text + '.' + textBox2.Text + ".png";
            }
            else
            {
                pictureBox1.ImageLocation = "default.png";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" )
            {
                MessageBox.Show("Некоторые поля остались пустыми", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var result = MessageBox.Show("Вы точно хотите изменить обложку этого альлома?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                JsonSave AllMusic = new JsonSave();
                AllList Major = new AllList();
                Major.Load( ref AllMusic);

                openFileDialog1.ShowDialog();
                string file = openFileDialog1.FileName;
                if (file == "") return;
                if(AllList.ChangeCover(file, textBox1.Text, textBox2.Text)) pictureBox1.ImageLocation = "Covers/" + textBox1.Text + '.' + textBox2.Text + ".png";
                else MessageBox.Show("Имя группы или название альбома содержит запрещённые символы", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);



            }
        }

        private void AddToList_Load(object sender, EventArgs e)
        {

        }
    }
}
