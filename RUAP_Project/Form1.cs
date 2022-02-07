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

            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "0";
            textBox5.Text = "0";
            textBox6.Text = "0";
            textBox7.Text = "0";
            textBox8.Text = "0";
            textBox9.Text = "0";
            textBox10.Text = "0";
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
                "0",
                "0",
                "0",
                "0"
                }, { textBox1.Text,
                textBox2.Text,
                textBox3.Text,
                textBox4.Text,
                textBox5.Text,
                textBox6.Text,
                textBox7.Text,
                textBox8.Text,
                textBox9.Text,
                textBox10.Text,
                "0",
                "0",
                "0",
                "0" }, };



            PredictionModel.setInputValues(inputValues);
            PredictionModel.startPrediction();
            Thread.Sleep(3000);
            this.setPredictionText(PredictionModel.result);
        }
    }
}
