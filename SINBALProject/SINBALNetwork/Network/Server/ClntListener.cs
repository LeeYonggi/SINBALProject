using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SINBALNetwork.Network.Server
{
    public class ClntListener
    {
        // 비동기 Accept를 위한 eventArgs
        SocketAsyncEventArgs _acceptArgs   = null;

        // 클라이언트의 접속을 처리할 소켓
        TcpListener          _servSocket = null;

        // Accept처리의 순서를 제어하기 위한 이벤트 변수
        // 스레드를 컨트롤 할 때 쓰임.
        AutoResetEvent       _flowEvent    = null;

        // 새로운 클라이언트가 생성되었을 때 호출하는 콜백
        public delegate void NewClntHandler(TcpClient clientSocket, object token);
        private NewClntHandler _callbackClntHander = null;
        TcpClient tcpClient = null;

        // accept 받을 때 돌리는 스레드
        private Thread thread = null;
        public void ThreadClose() { if(thread != null) thread.Abort(); }

        public NewClntHandler CallbackClntHander { get => _callbackClntHander; set => _callbackClntHander += value; }

        private Interface.TexBoxForm _texbox;

        public ClntListener(Interface.TexBoxForm texbox)
        {
            _texbox = texbox;
        }

        public void Start(string host, int port, int backlog)
        {
            StartListen(host, port, backlog);
            StartAccept();
        }

        // Add Socket & bind, listen socket host 주소를 현재 주소로 하고 싶다면 0.0.0.0
        private void StartListen(string host, int port, int backlog) 
        {
            IPAddress address = null;
            if(host == "0.0.0.0")
            {
                address = IPAddress.Any;
            }
            else
            {
                address = IPAddress.Parse(host);
            }

            IPEndPoint ipEndPoint = new IPEndPoint(address, port);
            _servSocket = new TcpListener(ipEndPoint);

            try
            {
                _servSocket.Start();
            }
            catch(Exception e)
            {
                _texbox.ShowMessageBox(e.Message, e.Source);
            }
        }

        private void StartAccept()
        {
            try
            {
                _acceptArgs = new SocketAsyncEventArgs();
                // accept 완료시 호출되는 콜백
                // 콜백이라 인자를 따로 넣어줄 필요가 없음
                _acceptArgs.Completed += new EventHandler<SocketAsyncEventArgs>(AcceptCompleted);

                // 스레드 사용 1
                if (thread == null)
                {   
                    thread = new Thread(DoAcceptListenAsync);
                    thread.Start();
                }
            }
            catch(Exception e)
            {
                _texbox.ShowMessageBox("accept 에러가 발생했습니다.", e.Message);
            }
        }

        private async void DoAcceptListenAsync()
        {
            while(true)
            {
                // 재사용하기 위한 초기화
                _acceptArgs.AcceptSocket = null;
                try
                {
                    tcpClient = await _servSocket.AcceptTcpClientAsync().ConfigureAwait(false);
                    _callbackClntHander(tcpClient, _acceptArgs);
                }
                catch(Exception e)
                {
                    continue;
                }

                //// 즉시 완료되면 이벤트가 발생하지 않으므로 리턴값이 false일 경우 콜백 메소드를 직접 호출
                //// pending 상태라면 비동기 요청이 들어간 상태이므로 콜백 메소드를 기다림
                //if(pending == false) // 즉시 완료된 경우
                //{
                //    // 콜백 실행
                //    // 수동적 실행이라 인자를 넣어주어야 함
                //    AcceptCompleted(null, _acceptArgs);
                //}

                //// 클라이언트가 접속 처리되면 이벤트 객체 신호를 전달받아 다시 루프를 수행
                //_flowEvent.WaitOne();
            }
            if(_servSocket != null)
                _servSocket.Stop();
        }

        private void AcceptCompleted(object sender, SocketAsyncEventArgs e)
        {
            if(e.SocketError == SocketError.Success)
            {
                // 새로 생긴 소켓 보관
                TcpClient clientSocket = new TcpClient();
                clientSocket.Client = e.AcceptSocket;

                // 다음 연결을 받음
                // while 동작 가능
                _flowEvent.Set();

                //
                _callbackClntHander?.Invoke(clientSocket, e.UserToken);

                return;
            }
            else
            {
                // todo:Accept 실패 처리

            }

            //다음 연결을 받아들임
            _flowEvent.Set();
        }
    }
}
