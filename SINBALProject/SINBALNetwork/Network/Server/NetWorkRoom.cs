using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SINBALNetwork.Network.Server
{
    public class NetWorkRoom
    {
        private List<ClientHandler> _clientList = null;

        private string roomName = "";

        private Interface.TexBoxForm drawText;

        private Thread thread = null;
        public void ThreadClose() { if(thread != null) thread.Abort();  }

        public NetWorkRoom(string name, Interface.TexBoxForm form)
        {
            _clientList = new List<ClientHandler>();
            roomName = name;
            drawText = form;

            // 스레드 생성 2
            if (thread == null) 
            {
                thread = new Thread(ReadClientAll);
                thread.Start();
            }
        }
        public void AddClient(TcpClient socket, object token, string userName)
        {
            ClientHandler client = new ClientHandler(socket, token, userName, DisconnectClient);
            _clientList.Add(client);

            drawText.DrawTex(string.Format("[System] {0} join the room {1}", client.Id, roomName));
            SendClientAll(string.Format("[System] {0} 님이 {1} 방에 입장하였습니다.",
                client.Id, roomName));
        }

        // 방 안의 모든 클라이언트들에게 메세지를 보냅니다.
        public void SendClientAll(string text)
        {
            for(int i = 0; i < _clientList.Count; i++)
            {
                _clientList[i].SendToClient(text);
            }
        }

        // 방 안의 모든 클라이언트들의 메세지를 받습니다.
        public void ReadClientAll()
        {
            while (true)
            {
                for (int i = 0; i < _clientList.Count; i++)
                {
                    string text = _clientList[i].ReadFromClientAsync().Result;
                    if (text != "")
                    {
                        SendClientAll(text);
                        drawText.DrawTex(text);
                    }
                }
            }

        }

        public void DisconnectClient(ClientHandler client)
        {
            drawText.DrawTex(string.Format("[System] {0} exit the room {1}", client.Id, roomName));
            SendClientAll(string.Format("[System] {0} 님이 {1} 방에서 퇴장하였습니다.",
            client.Id, roomName));
            _clientList.Remove(client);
        }
    }
}
