using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using SINBALNetwork.Network.Interface;
using SINBALNetwork.Network;
using System.Threading;
using System.IO;

namespace SINBALNetwork.Network.Client
{
    public class ServerConnecter
    {
        // 서버 소켓
        private TcpListener _serverSocket = null;

        // 서버와 연결할 클라이언트 소켓
        private TcpClient   _clientSocket = null;
        NetworkStream _writeStream = null;

        private TexBoxForm _texBox = null;
        // 서버에서 들어온 메세지를 읽을 스트림
        Thread _thread = null;

        public ServerConnecter(TexBoxForm texBox)
        {
            _clientSocket = new TcpClient();
            _texBox = texBox;
        }

        public void Start(string address, int port, string myId)
        {
            StartConnect(address, port, myId);

            if (_thread == null)
            {
                _thread = new Thread(DoReadMessage);
                _thread.Start();
            }
        }

        public void StartConnect(string address, int port, string myId)
        {
            try
            {
                _clientSocket.Connect(address, port);
                WriteToServer(myId);
                _texBox.DrawTex("서버에 연결되었습니다.");
            }
            catch(ArgumentNullException e)
            {
                _texBox.ShowMessageBox("접속 실패", e.Message);
            }
            catch(SocketException e)
            {
                _texBox.ShowMessageBox("접속 실패", e.Message);
            }
            catch(ObjectDisposedException e)
            {
                _texBox.ShowMessageBox("접속 실패", e.Message);
            }
        }

        // 서버에 보냄
        public void WriteToServer(string message)
        {
            try
            {
                _writeStream = _clientSocket.GetStream();

                byte[] buff = Encoding.Unicode.GetBytes(message);
                _writeStream.Write(buff, 0, buff.Length);
                _writeStream.Flush();
            }
            catch(Exception e)
            {
                _texBox.ShowMessageBox("쓰기 에러", e.Message);
            }
        }

        // 스레드와 함께써야함 이곳으로 접근할 때에는 델리게이트로 비동기 접근 가능
        private void DoReadMessage()
        {
            try
            {
                while (true)
                {
                    if (_clientSocket.Connected == false)
                        break;
                    NetworkStream _readStream = _clientSocket.GetStream();

                    // 서버에서 읽어옴
                    byte[] buff     = new byte[Server.NetWorkService.BufferSize];
                    int nBuffSize   = _readStream.Read(buff, 0, buff.Length);

                    string message = Encoding.Unicode.GetString(buff, 0, nBuffSize);

                    _texBox.DrawTex(message);
                }
            }
            catch(IOException e)
            {
                _texBox.ShowMessageBox("연결이 끊어졌습니다.", e.Message);
            }
        }

        public void CloseClient()
        {
            if (_thread != null)
                _thread.Abort();
            if (_writeStream != null)
                _writeStream.Close();
            if (_clientSocket != null)
                _clientSocket.Close();
        }
    }
}
