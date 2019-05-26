using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using SINBALNetwork.Network.Interface;
using SINBALNetwork.Network.Server;
using System.Net.Sockets;

namespace SINBALServGUI
{
    public partial class Form1 : Form, TexBoxForm
    {
        NetWorkService serverNetwork = null;
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "";
            textBox2.Text = "0";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void ClearTex()
        {
            textBox1.Text = "";
        }

        public void DrawColorTex(string text, int r, int g, int b)
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.BeginInvoke(new MethodInvoker(delegate
                {
                    textBox1.ForeColor = Color.FromArgb(r, g, b);
                    textBox1.AppendText(text + Environment.NewLine);
                }));
            }
            else
            {
                textBox1.ForeColor = Color.FromArgb(r, g, b);
                textBox1.AppendText(text + Environment.NewLine);
            }
        }

        public void DrawTex(string text)
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.BeginInvoke(new MethodInvoker(delegate
                {
                    textBox1.AppendText(text + Environment.NewLine);
                }));
            }
            else
                textBox1.AppendText(text + Environment.NewLine);
        }

        public void ShowMessageBox(string text, string caption)
        {
            MessageBox.Show(text, caption);
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (serverNetwork == null)
            {
                try
                {
                    serverNetwork = new NetWorkService(this);

                    serverNetwork.Listen("0.0.0.0", Int32.Parse(textBox2.Text), 100);

                    string myIp = GetMyIP(Dns.GetHostName());

                    DrawTex(string.Format("서버가 생성되었습니다. port: {0} Name: {1}", 
                        Int32.Parse(textBox2.Text), myIp));
                }
                catch (FormatException)
                {
                    MessageBox.Show("문자열 패턴에 맞지 않습니다.");
                }
                catch (OverflowException)
                {
                    MessageBox.Show("입력 값을 벗어났습니다.");
                }
                catch (Exception)
                {
                    Application.Exit();
                }
                textBox2.Text = "";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("서버를 종료합니다.", "서버 종료", MessageBoxButtons.YesNo) 
                == DialogResult.Yes)
            {
                if(serverNetwork != null) // 서버가 있으면
                  serverNetwork.Close();
            }
            else
            {
                e.Cancel = true;
                return;
            }
        }

        private string GetMyIP(string name)
        {
            IPHostEntry host = Dns.GetHostEntry(name);
            string ClientIP = string.Empty;
            for (int i = 0; i < host.AddressList.Length; i++)
            {
                if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    ClientIP = host.AddressList[i].ToString();
                }
            }
            return ClientIP;
        }
    }
}
