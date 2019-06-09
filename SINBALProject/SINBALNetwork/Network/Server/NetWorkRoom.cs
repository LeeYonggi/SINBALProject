using System;
using System.Collections;
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
        // 클라이언트 정보들
        private List<ClientHandler> _clientList = null;
        List<Task> allTasks = new List<Task>();

        // 방 정보
        private string roomName = "";

        // UI관련
        private Interface.TexBoxForm drawText;

        // 쓰레드
        private Thread thread = null;

        // Property
        public void ThreadClose() { if(thread != null) thread.Abort();  }

        public NetWorkRoom(string name, Interface.TexBoxForm form)
        {
            _clientList = new List<ClientHandler>();
            roomName = name;
            drawText = form;

            // 스레드 생성 2
            if (thread == null) 
            {
                thread = new Thread(ReadClientAllAsync);
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

            AddTesk(client);
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
        public async void ReadClientAllAsync()
        {
            while (true)
            {
                if (!allTasks.Any())
                    continue;
                Task finished = await Task.WhenAny(allTasks);
                
                IEnumerator<ClientHandler> e = _clientList.GetEnumerator();
                while(e.MoveNext())
                {
                    if (finished != e.Current.ReadWaitTask)
                        continue;

                    string text = e.Current.ReadClientBuffer();

                    if (text != string.Empty)
                    {
                        // 전체에게 메세지 전달
                        SendClientAll(text);
                        drawText.DrawTex(text);
                        // 새로운 테스크 생성
                        allTasks.Remove(finished);
                        AddTesk(e.Current);
                    }
                    else
                        allTasks.Remove(finished);
                }
            }
        }

        private void AddTesk(ClientHandler client)
        {
            Task tempTask = client.ReadFromClient();
            if (tempTask != null)
                allTasks.Add(tempTask);
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
