using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SINBALNetwork.Network.Interface;

namespace SINBALNetwork.Network.Client
{
    public class ClientService
    {
        private TexBoxForm _texBox = null;
        private ServerConnecter servConnecter = null;

        public ClientService(TexBoxForm texBox)
        {
            _texBox = texBox;
            servConnecter = new ServerConnecter(_texBox);
        }

        public void Connect(string host, int port, string myId)
        {
            if (port < 0 || port > 65535)
                throw new Exception("port 번호가 초과되었습니다.");

            servConnecter.Start(host, port, myId);
        }

        public void Write(string message)
        {
            servConnecter.WriteToServer(message);
        }

        public void Close()
        {
            servConnecter.CloseClient();
        }
    }
}
