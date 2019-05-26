using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SINBALNetwork.Network.Server
{
    public class NetWorkService
    {
        ClntListener cListener = null;

        Dictionary<string, NetWorkRoom> _roomDictionary = new Dictionary<string, NetWorkRoom>();

        // windowform에서 드로우할 박스
        Interface.TexBoxForm _texBox = null;

        public const int BufferSize = 1024;

        public NetWorkService(Interface.TexBoxForm texBox)
        {
            _texBox = texBox;
            _roomDictionary.Add("Lobby", new NetWorkRoom("Lobby", _texBox));
        }


        public void Listen(string host, int port, int backlog)
        {
            if (port < 0 || port > 65535)
                throw new Exception("port 번호가 초과되었습니다.");
            cListener = new ClntListener(_texBox);
            // 콜백 함수
            cListener.CallbackClntHander = NewCreateClient;
            // 서버 실행
            cListener.Start(host, port, backlog);
        }

        public void NewCreateClient(TcpClient socket, object token)
        {
            try
            {
                // 클라이언트에서 버퍼를 읽어옴
                NetworkStream stream = socket.GetStream();
                byte[] buff          = new byte[BufferSize];
                int nBuffSize        = stream.Read(buff, 0, buff.Length);

                // 유니코드 변환 & 문자열 자르기
                string userName = Encoding.Unicode.GetString(buff, 0, nBuffSize);
                userName        = userName.Substring(0, userName.Length);


                NetWorkRoom room;
                _roomDictionary.TryGetValue("Lobby", out room);
                room.AddClient(socket, token, userName);
            }
            catch(Exception e)
            {
                _texBox.DrawTex(e.Message);
            }
        }

        public void Close()
        {
            cListener.ThreadClose();
            foreach (var value in _roomDictionary)
                value.Value.ThreadClose();
        }
    }
}
