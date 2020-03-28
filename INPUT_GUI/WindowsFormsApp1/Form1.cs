using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        
        delegate void SetTextCallback(string text);
        public Form1()
        {
            InitializeComponent();
            Form.CheckForIllegalCrossThreadCalls = false;
            CreatePanel();
            timer1.Stop();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            progressBar1.Value = 10;
            double timeStamp = ((TimeSpan)DateTime.Now.Subtract(DateTime.MinValue)).TotalSeconds;
            string fileName = string.Format("{0}.txt", timeStamp);
            Process pyProc = new Process();
            string picdata = textBox1.Text;
            pyProc.EnableRaisingEvents = true;
            pyProc.StartInfo.UseShellExecute = false;
            pyProc.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            pyProc.StartInfo.FileName = "python";
            pyProc.StartInfo.CreateNoWindow = true;

            pyProc.StartInfo.Arguments = string.Format("predict.py --content \"{0}\" --timestamp \"{1}\"", picdata, timeStamp);

            pyProc.Exited += (obj, args) =>
            {
                if (pyProc.ExitCode == 0)
                {
                    progressBar1.Value = 100;
                    //this.SetText(System.IO.File.ReadAllText(fileName));
                    label1.Text =  System.IO.File.ReadAllText(fileName);
                    String org=label1.Text;
                    org=org.Replace("[","");
                    org=org.Replace("]","");
                    string[] words = org.Split(' ');
                    int datacnt=0;
                    String all="";
                    float[] datas = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    float[] datap = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    int big = 0;
                        foreach (var word in words)
                        {
                            String a = word.Trim();
                            
                            if (a != "")
                            {
                                all += a + "\n";
                                float dataorg = Convert.ToSingle(a);
                                datap[datacnt] = dataorg;
                                datacnt++;
                            }
                            
                        }
                        for (int z = 1; z < 10; z++)
                        {
                            if (datap[big] <= datap[z])
                            {
                                big = z;
                            }
                        }
                            label1.Text = all;
                            label3.Text = Convert.ToString(big);
                            label4.Text = Convert.ToString(Math.Round((datap[big] * 100), 2, MidpointRounding.AwayFromZero)) + "%";
                    File.Delete(fileName);
                }
                else
                {
                    progressBar1.Value = 100;
                    label1.Text = "我不知道這是啥";
                    //# MessageBox.Show(string.Format("TimeStamp: {0} 執行錯誤", timeStamp));
                    //this.SetText(string.Format("TimeStamp: {0} 執行錯誤", timeStamp));
                }
            };

            bool result = pyProc.Start();
        }
        private void SetText(string text)
        {
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text = text;
            }
        }
        int adot = 1;
        string kt = "1";
        Label[] buttons = new Label[785];
        bool status = false;
        private void CreatePanel()
        {
            


            for (int x = 0; x < 28; x++)
            {
                for (int y = 0; y < 28; y++)
                {
                    buttons[28*x+ y] = new Label();
              
                    buttons[28 * x + y].Height = 10;
                    buttons[28 * x + y].FlatStyle = FlatStyle.Flat;
                    buttons[28 * x + y].Width = 10;
                    buttons[28 * x + y].Name = "Bt" + 28 * x + y;
                    buttons[28 * x + y].Text = "0";
                    buttons[28 * x + y].BackColor = Color.White;
                    buttons[28 * x + y].Location = new Point(10 * x, 10 * y);
                    buttons[28 * x + y].MouseEnter += new EventHandler(button_Click);
                    panel1.Controls.Add(buttons[28 * x + y]);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_Click(object sender, EventArgs e)
        {
            if (status==true)
            {
                ((Label)(sender)).Text = kt;
                if (kt == "1")
                {
                    ((Label)(sender)).BackColor = Color.Black;
                }
                else
                {
                    ((Label)(sender)).BackColor = Color.White;
                }

              
            }
 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            adot = 1;
            timer1.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            for (int x = 0; x < 28; x++)
            {
                for (int y = 0; y < 28; y++)
                {
                    textBox1.Text += buttons[28 * y + x].Text;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
          status = true;
            if (e.KeyCode == Keys.Z)
            {
                kt = "1";
            }
            if (e.KeyCode == Keys.X)
            {
                kt = "0";
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            status = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
      
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            buttons[adot].Text = "0";
            buttons[adot].BackColor = Color.White;
            adot++;
            if (adot >= 784)
            {
                timer1.Stop();
            }

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
