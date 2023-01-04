using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace XMLFormatter
{
    public partial class Form1 : Form
    {
        string file;
        int size = -1;
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                file = openFileDialog1.FileName;
                textBox1.Text = Path.GetFileName(file);
                textBox2.Text = Path.GetFileNameWithoutExtension(file) + "_1" + Path.GetExtension(file);
                textBox3.Text = Path.GetDirectoryName(file);
                try
                {
                    string text = File.ReadAllText(file);
                    size = text.Length;
                }
                catch (IOException)
                {
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToString("h:mm:ss tt");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "") || (textBox2.Text == ""))
            {
                MessageBox.Show("PLease select From File and To File");
            }
            else
            {
                String s1 = Path.GetDirectoryName(file);
                s1 = s1 + "\\" + textBox2.Text;

                if (File.Exists(s1))
                {
                    const string message = "The Target File exists. \n Do you still confinue ? (Y/N)";
                    const string caption = "File exists ?";
                    DialogResult result = MessageBox.Show(message, caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                    {
                        return;
                    }

                }
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;

                XmlDocument xmldoc = new XmlDocument();

                XmlWriter xw = XmlWriter.Create(s1, settings);
                try
                {
                    xmldoc.Load(openFileDialog1.FileName);
                    xmldoc.Save(xw);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invaliad XML FIle. Select XML File! \n" + ex);
                    return;
                }
                xw.Flush();
                xw.Close();
                MessageBox.Show("Completed!");
            }
        }
    }
}
