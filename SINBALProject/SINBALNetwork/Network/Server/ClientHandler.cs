using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SINBALNetwork.Network.Server
{
    public class ClientHandler
    {
        // client연결 관리
        private TcpClient clntSocket = null;
        private NetworkStream stream = null;

        // 델리게이트
        public delegate void DisconnectedHandler(ClientHandler client);
        private DisconnectedHandler onDisconnect = null;

        // 비동기로 클라이언트에서 읽어오기 위한 테스크
        private Task<int> readWaitTask = null;

        // 읽기 위한 버퍼
        private byte[] buff = null;

        // 클라이언트 정보
        private object token = null;
        private string clientId = "";

        // Property
        public string Id { get => clientId; set => clientId = value; }
        public TcpClient ClntSocket { get => clntSocket; set => clntSocket = value; }
        public object Token { get => token; set => token = value; }
        public Task<int> ReadWaitTask { get => readWaitTask; set => readWaitTask = value; }

        public ClientHandler(TcpClient socket, object token, string id, 
            DisconnectedHandler disconnected)
        {
            ClntSocket = socket;
            Token = token;
            clientId = id;
            onDisconnect = disconnected;
        }
        public void SendToClient(string text)
        {
            try
            {
                if(clntSocket.Connected == true)
                {
                    stream = clntSocket.GetStream();

                    byte[] buff = Encoding.Unicode.GetBytes(text);

                    // 비동기로 교체해야 함
                    stream.Write(buff, 0, buff.Length);
                    // 버퍼 비움
                    stream.Flush();
                }
            }
            catch(Exception)
            {
                return;
            }
        }
        public Task ReadFromClient()
        {
            try
            {
                if (clntSocket.Connected == false)
                {
                    ClientClose();
                    return null;
                }
                stream = clntSocket.GetStream();

                buff = new byte[NetWorkService.BufferSize];
                readWaitTask = stream.ReadAsync(buff, 0, buff.Length);
                return readWaitTask;
            }
            catch (SocketException)
            {
                ClientClose();
                return null;
            }
            catch (Exception)
            {
                ClientClose();
                return null;
            }
        }

        public string ReadClientBuffer()
        {
            if(readWaitTask.Result > 0)
            {
                string text = Encoding.Unicode.GetString(buff, 0, readWaitTask.Result);
                return text;
            }
            else
            {
                ClientClose();
                return string.Empty;
            }
        }

        private void ClientClose()
        {
            onDisconnect?.Invoke(this);
            clntSocket.Close();
            stream.Close();
        }
    }
}
