using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SINBALNetwork.Network.Interface;
using System.Net;
using System.Net.Sockets;
using SINBALNetwork.Network.Client;

namespace SINBALClntGUI
{
    public partial class Form1 : Form, TexBoxForm
    {
        ClientService _clientService = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        void TexBoxForm.ClearTex()
        {
            textBox1.Text = "";
        }

        void TexBoxForm.DrawColorTex(string text, int r, int g, int b)
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

        void TexBoxForm.DrawTex(string text)
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

        void TexBoxForm.ShowMessageBox(string text, string caption)
        {
            MessageBox.Show(text, caption);
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

        private void Button1_Click(object sender, EventArgs e)
        {
            _clientService = new ClientService(this);

            _clientService.Connect(serverIP.Text, Int32.Parse(portNum.Text), myID.Text);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("프로그램을 종료합니다.", "프로그램 종료", MessageBoxButtons.YesNo)
                   == DialogResult.Yes)
            {
                if (_clientService != null) 
                    _clientService.Close();
            }
            else
            {
                e.Cancel = true;
                return;
            }
        }

        private void Send_Click(object sender, EventArgs e)
        {
            if(writeBox.Text != "" && _clientService != null)
            {
                _clientService.Write(writeBox.Text);
                writeBox.Text = "";
            }
            else if(_clientService == null)
            {
                MessageBox.Show("서버에 연결해 주세요.");
            }
            else if(writeBox.Text == "")
            {
                MessageBox.Show("메세지를 입력해 주세요.");
            }
        }
    }
}
