using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using Newtonsoft.Json;

namespace Course
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Program.Context.MainForm = new ListAllMusic();
            this.Close();
            Program.Context.MainForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.Context.MainForm = new FavoriteList();
            this.Close();
            Program.Context.MainForm.Show();
        }

        
       
    }
    
   
    
    
    
    
    

    
}
