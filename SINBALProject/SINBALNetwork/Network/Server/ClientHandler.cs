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
        // client를 관리하는 소켓입니다.
        private TcpClient clntSocket = null;

        private object token = null;
        public object Token { get => token; set => token = value; }

        private string clientId = "";
        public string Id { get => clientId; set => clientId = value; }
        public TcpClient ClntSocket { get => clntSocket; set => clntSocket = value; }

        public delegate void DisconnectedHandler(ClientHandler client);
        private DisconnectedHandler onDisconnect = null;
        private NetworkStream stream = null;

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
        public async Task<string> ReadFromClientAsync()
        {
            try
            {
                if (clntSocket.Connected == false)
                {
                    ClientClose();
                    return "";
                }
                stream = clntSocket.GetStream();

                byte[] buff = new byte[NetWorkService.BufferSize];
                int nBuffSize = await stream.ReadAsync(buff, 0, buff.Length).ConfigureAwait(false);
                
                if (nBuffSize > 0)
                {
                    string text = Encoding.Unicode.GetString(buff, 0, nBuffSize);
                    text        = text.Substring(0, text.Length);
                    return text;
                }
                else
                {
                    return "";
                }
                
            }
            catch(SocketException)
            {
                ClientClose();
                return "";
            }
            catch(Exception)
            {
                ClientClose();
                return "";
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
