using SuperWebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RemoteInputController
{
    /// <summary>
    /// Interaction logic for InputControllerUI.xaml
    /// </summary>
    public partial class InputControllerUI : Window
    {
        //https://www.youtube.com/watch?v=JbXurSfeceY
        private WebSocketServer webSocketServer = null;
        public InputControllerUI()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            webSocketServer = new WebSocketServer();
            int port = 80;
            webSocketServer.Setup(port);
            webSocketServer.NewSessionConnected += WebSocketServer_NewSessionConnected;
            webSocketServer.NewDataReceived += WebSocketServer_NewDataReceived;
            webSocketServer.NewMessageReceived += WebSocketServer_NewMessageReceived;
            webSocketServer.SessionClosed += WebSocketServer_SessionClosed;
            webSocketServer.Start();
        }
        private void WebSocketServer_NewSessionConnected(WebSocketSession session)
        {

        }
        private void WebSocketServer_NewDataReceived(WebSocketSession session, byte[] value)
        {

        }        
        private void WebSocketServer_NewMessageReceived(WebSocketSession session, string value)
        {
            session.Send("No Rocking Shocking!");
        }
        private void WebSocketServer_SessionClosed(WebSocketSession session, SuperSocket.SocketBase.CloseReason value)
        {

        }
    }
}
