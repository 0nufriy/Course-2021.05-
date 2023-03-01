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
    public partial class ListAllMusic : Form
    {
        public ListAllMusic()
        {
            InitializeComponent();
        }
        JsonSave AllMusic = new JsonSave();
        AllList Major = new AllList();
        private void button1_Click(object sender, EventArgs e)
        {
            Program.Context.MainForm = new Form1();
            this.Close();
            Program.Context.MainForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddToList formtoadd = new AddToList();
            formtoadd.FormActive = this;
            formtoadd.Show();
        }

        int section = 0;

        

        public void LoadAllMusicList(int start=0, int length=-1)
        {

            textBox1.Text = "";
            flowLayoutPanel1.Controls.Clear();
            int k = 0;
            if (length <= 0) length = Major.CountMusic;
            MusicEl[] Cataloge = new MusicEl[Major.CountMusic];
            if (Major.CountMusic <= 10) button4.Enabled = false;
            for (int i = 0; i < Major.Count; i++)
            {
                for (int j = 0; j < Major.artistlist[i].Count; j++)
                {
                    for (int g = 0; g < Major.artistlist[i][j].Count; g++)
                    {
                        if (k >= start && k<start+length)
                        {
                            Cataloge[k] = new MusicEl();
                            Cataloge[k].ArtistName = Major.artistlist[i].name;
                            Cataloge[k].AlbumName = Major.artistlist[i][j].name + '(' + Major.artistlist[i][j].years.ToString() + ')';
                            Cataloge[k].MusicName = Major.artistlist[i][j][g].name;
                            Cataloge[k].FormActive = this;
                            if (File.Exists(Major.artistlist[i][j].Cover))
                            {
                                Cataloge[k].Cover = Major.artistlist[i][j].Cover;
                            }
                            else
                            {
                                Cataloge[k].Cover = "default.png";
                            }
                            flowLayoutPanel1.Controls.Add(Cataloge[k]);
                            
                        }
                        k++;
                        if (k ==  start + length) return;
                        
                    }
                }
            }
        }

        private void ListAllMusic_Load(object sender, EventArgs e)
        {
            Major.Load(ref AllMusic);
            LoadAllMusicList(0,10);
        }
        List<Music> resultSN = new List<Music>();
        List<Album> resultSAL = new List<Album>();
        List<Artist> resultSAR = new List<Artist>();
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            

            if (textBox1.Text.Length < 3)
            {
                if (textBox1.Text == "")
                {

                    section = 0;
                    button3.Enabled = false;
                    button4.Enabled = true;
                    button5.Enabled = true;
                    button6.Enabled = true;
                    button7.Enabled = false;

                    LoadAllMusicList(0, 10);
                }
                return;
            }
            flowLayoutPanel1.Controls.Clear();
            button7.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            if (radioButton1.Checked)
            {
                List<Music> result = Major.SearchByMusicName(textBox1.Text);
                resultSN = result;
                MusicEl[] Cataloge = new MusicEl[result.Count];
                if(result.Count == 0)
                {
                    Label notfound = new Label();
                    notfound.Text = "Ничего не найдено";
                    notfound.AutoSize = true;
                    flowLayoutPanel1.Controls.Add(notfound);
                }
                for(int i = 0; i < Cataloge.Length; i++)
                {
                    Cataloge[i] = new MusicEl();
                    Cataloge[i].ArtistName = result[i].artist.name;
                    Cataloge[i].AlbumName = result[i].album.name + '(' + result[i].years.ToString() + ')';
                    Cataloge[i].MusicName = result[i].name;
                    Cataloge[i].FormActive = this;
                    if (File.Exists(result[i].album.Cover))
                    {
                        Cataloge[i].Cover = result[i].album.Cover;
                    }
                    else
                    {
                        Cataloge[i].Cover = "default.png";
                    }
                    flowLayoutPanel1.Controls.Add(Cataloge[i]);
                }
            }
            else if (radioButton2.Checked)
            {
                List<Album> result=Major.SearchByAlbumName(textBox1.Text);
                resultSAL = result;
                if (result.Count == 0)
                {
                    Label notfound = new Label();
                    notfound.Text = "Ничего не найдено";
                    notfound.AutoSize = true;
                    flowLayoutPanel1.Controls.Add(notfound);
                }
                int sum = 0;
                for(int i = 0; i < result.Count; i++)
                {
                    sum += result[i].Count;
                }
                MusicEl[] Cataloge = new MusicEl[sum];
                int k = 0;
                for(int i = 0; i < result.Count; i++)
                {
                    for (int j = 0; j < result[i].Count; j++)
                    {
                        Cataloge[k] = new MusicEl();
                        Cataloge[k].ArtistName = result[i][j].artist.name;
                        Cataloge[k].AlbumName = result[i][j].album.name + '(' + result[i].years.ToString() + ')';
                        Cataloge[k].MusicName = result[i][j].name;
                        Cataloge[k].FormActive = this;
                        if (File.Exists(result[i].Cover))
                        {
                            Cataloge[k].Cover = result[i].Cover;
                        }
                        else
                        {
                            Cataloge[k].Cover = "default.png";
                        }
                        flowLayoutPanel1.Controls.Add(Cataloge[k]);
                        k++;
                    }
                }
            }
            else if (radioButton3.Checked)
            {
                List<Artist> result= Major.SearchByArtistName(textBox1.Text);
                resultSAR = result;
                if (result.Count == 0)
                {
                    Label notfound = new Label();
                    notfound.Text = "Ничего не найдено";
                    notfound.AutoSize = true;
                    flowLayoutPanel1.Controls.Add(notfound);
                }
                int sum = 0;
                for (int i = 0; i < result.Count; i++)
                {
                    for (int j = 0; j < result[i].Count; j++)
                    {
                        sum += result[i][j].Count;
                    }
                }
                MusicEl[] Cataloge = new MusicEl[sum];
                int k = 0;
                for (int i = 0; i < result.Count; i++)
                {
                    for (int j = 0; j < result[i].Count; j++)
                    {
                        for (int g = 0; g < result[i][j].Count; g++)
                        {
                            Cataloge[k] = new MusicEl();
                            Cataloge[k].ArtistName = result[i].name;
                            Cataloge[k].AlbumName = result[i][j].name + '(' + result[i][j].years.ToString() + ')';
                            Cataloge[k].MusicName = result[i][j][g].name;
                            Cataloge[k].FormActive = this;
                            if (File.Exists(result[i][j].Cover))
                            {
                                Cataloge[k].Cover = result[i][j].Cover;
                            }
                            else
                            {
                                Cataloge[k].Cover = "default.png";
                            }
                            flowLayoutPanel1.Controls.Add(Cataloge[k]);
                            k++;
                        }
                    }
                }
            }
        }

        
        public void RefreshMy()
        {
            AllMusic = JsonConvert.DeserializeObject<JsonSave>(File.ReadAllText("AllList.json"));
            Major = new AllList();
            for (int i = 0; i < AllMusic.Count; i++)
            {
                Major.AddToList(AllMusic, AllMusic[i].ArtistName, AllMusic[i].AlbumName, AllMusic[i].MusicName, AllMusic[i].Year,  true);
            }
            section = 0;
            button3.Enabled = false;
            button4.Enabled = true;
            LoadAllMusicList(0, 10);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (section - 20 < 0) button3.Enabled = false;
            int lastsection = 0;
            if (Major.CountMusic % 10 == 0) lastsection = Major.CountMusic - 10;
            else lastsection = Major.CountMusic - Major.CountMusic % 10;
            if (section == lastsection) button4.Enabled = true;
            section -= 10;
            LoadAllMusicList(section, 10);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (section + 20 >= Major.CountMusic) button4.Enabled = false;
            if(section==0) button3.Enabled = true;
            section +=10;
            LoadAllMusicList(section, 10);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            button3.Enabled = false;
            button4.Enabled = true;
            section = 0;
            LoadAllMusicList(section, 10);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int lastsection = 0;
            if (Major.CountMusic % 10 == 0) lastsection = Major.CountMusic - 10;
            else lastsection = Major.CountMusic - Major.CountMusic % 10;

            button3.Enabled = true;
            button4.Enabled = false;
            section = lastsection;
            LoadAllMusicList(section, 10);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName == "") return;
            
                JsonSave res = new JsonSave();
            if (radioButton1.Checked)
            {
                for(int i = 0; i < resultSN.Count; i++)
                {
                    res.list.Add(new JSONElement(resultSN[i].artist.name, resultSN[i].album.name, resultSN[i].name, resultSN[i].album.years));
                }
                
            } else
            if (radioButton2.Checked)
            {
                for(int i = 0; i < resultSAL.Count; i++)
                {
                    for (int j = 0; j < resultSAL[i].musiclist.Count; j++)
                    {
                        res.list.Add(new JSONElement(resultSAL[i].artist.name, resultSAL[i].name, resultSAL[i].name, resultSAL[i].years));
                    }
                }
                
            } else
            if (radioButton3.Checked)
            {
                for(int i = 0; i< resultSAR.Count; i++)
                {
                    for (int j = 0; j < resultSAR[i].Count; j++)
                    {
                        for (int g = 0; g < resultSAR[i][j].Count; g++)
                        {
                            res.list.Add(new JSONElement(resultSAR[i].name, resultSAR[i][j].name, resultSAR[i][j][g].name, resultSAR[i][j].years));
                        }
                    }
                }
                
            }
                File.WriteAllText(saveFileDialog1.FileName, JsonConvert.SerializeObject(res));
                MessageBox.Show("Файл успешно сохранён", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
           

        }

    }

}
