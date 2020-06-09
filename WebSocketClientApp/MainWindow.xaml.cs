using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebSocketSharp;

namespace WebSocketClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private string Url = "wss://echo.websocket.org";
        private string Url = "ws://10.16.25.160";
        private WebSocket webSocket = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                webSocket = new WebSocket(Url);
                webSocket.OnMessage += WebSocket_OnMessage;
                webSocket.ConnectAsync();
            }
            catch (Exception ex)
            {

            }
        }

        private void WebSocket_OnMessage(object sender, MessageEventArgs e)
        {
            Debug.WriteLine("Received:" + e.Data);
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                webSocket.Send("Hello Server!");

            }
            catch (Exception ex)
            {

            }
        }
    }
}
