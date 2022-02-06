using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RUAP_Project
{
    public partial class Form1 : Form
    {
        string[,] inputValues;

        public void setPredictionText(String txt)
        {
            label12.Text = txt;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            inputValues = new string[,] { {
                textBox1.Text,
                textBox2.Text,
                textBox3.Text,
                textBox4.Text,
                textBox5.Text,
                textBox6.Text,
                textBox7.Text,
                textBox8.Text,
                textBox9.Text,
                textBox10.Text,
                "" } };
            PredictionModel.setInputValues(inputValues);
            PredictionModel.startPrediction();
            //Thread.Sleep(3000);
        }
    }
}
