using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;


namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Private Test server_translate = new Test;
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.ToString();
            //Test.translator(textBox1.Text.ToString());
            //MessageBox.Show(textBox1.Text.ToString());
            //public Test test = new Test();

        }
    }
}
