namespace SINBALClntGUI
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Chatting = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.serverIP = new System.Windows.Forms.TextBox();
            this.ConnectServer = new System.Windows.Forms.Button();
            this.send = new System.Windows.Forms.Button();
            this.writeBox = new System.Windows.Forms.TextBox();
            this.portNum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.myID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 132);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(310, 384);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // Chatting
            // 
            this.Chatting.AutoSize = true;
            this.Chatting.Font = new System.Drawing.Font("맑은 고딕", 14F);
            this.Chatting.Location = new System.Drawing.Point(7, 104);
            this.Chatting.Name = "Chatting";
            this.Chatting.Size = new System.Drawing.Size(86, 25);
            this.Chatting.TabIndex = 1;
            this.Chatting.Text = "Chatting";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.label1.Location = new System.Drawing.Point(12, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "MyID";
            // 
            // serverIP
            // 
            this.serverIP.Location = new System.Drawing.Point(101, 19);
            this.serverIP.Name = "serverIP";
            this.serverIP.Size = new System.Drawing.Size(96, 21);
            this.serverIP.TabIndex = 3;
            // 
            // ConnectServer
            // 
            this.ConnectServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ConnectServer.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.ConnectServer.Location = new System.Drawing.Point(213, 30);
            this.ConnectServer.Name = "ConnectServer";
            this.ConnectServer.Size = new System.Drawing.Size(109, 56);
            this.ConnectServer.TabIndex = 4;
            this.ConnectServer.Text = "Connect Server";
            this.ConnectServer.UseVisualStyleBackColor = true;
            this.ConnectServer.Click += new System.EventHandler(this.Button1_Click);
            // 
            // send
            // 
            this.send.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.send.Location = new System.Drawing.Point(259, 522);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(62, 27);
            this.send.TabIndex = 6;
            this.send.Text = "Send";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.Send_Click);
            // 
            // writeBox
            // 
            this.writeBox.Location = new System.Drawing.Point(17, 527);
            this.writeBox.Name = "writeBox";
            this.writeBox.Size = new System.Drawing.Size(233, 21);
            this.writeBox.TabIndex = 7;
            // 
            // portNum
            // 
            this.portNum.Location = new System.Drawing.Point(101, 46);
            this.portNum.Name = "portNum";
            this.portNum.Size = new System.Drawing.Size(96, 21);
            this.portNum.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.label2.Location = new System.Drawing.Point(8, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 21);
            this.label2.TabIndex = 9;
            this.label2.Text = "ServerIP";
            // 
            // myID
            // 
            this.myID.Location = new System.Drawing.Point(101, 73);
            this.myID.Name = "myID";
            this.myID.Size = new System.Drawing.Size(96, 21);
            this.myID.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.label3.Location = new System.Drawing.Point(13, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 21);
            this.label3.TabIndex = 11;
            this.label3.Text = "Port";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 561);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.myID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.portNum);
            this.Controls.Add(this.writeBox);
            this.Controls.Add(this.send);
            this.Controls.Add(this.ConnectServer);
            this.Controls.Add(this.serverIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Chatting);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label Chatting;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox serverIP;
        private System.Windows.Forms.Button ConnectServer;
        private System.Windows.Forms.Button send;
        private System.Windows.Forms.TextBox writeBox;
        private System.Windows.Forms.TextBox portNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox myID;
        private System.Windows.Forms.Label label3;
    }
}

