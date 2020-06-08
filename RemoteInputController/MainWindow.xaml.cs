using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using WindowsInput;
using WindowsInput.Native;

namespace RemoteInputController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private InputSimulator inputSimulator = null;
        private Thread MouseThread = null;
        public MainWindow()
        {
            InitializeComponent();
            inputSimulator = new InputSimulator();
            this.KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Left)
            {
                inputSimulator.Mouse.MoveMouseBy(-5, 0);
            }
            else if(e.Key == Key.Right)
            {
                inputSimulator.Mouse.MoveMouseBy(5, 0);
            }
            else if(e.Key == Key.Up)
            {
                inputSimulator.Mouse.MoveMouseBy(0, -5);
            }
            else if (e.Key == Key.Down)
            {
                inputSimulator.Mouse.MoveMouseBy(0, 5);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            MouseThread = new Thread(MouseThreadMethod);
            MouseThread.Start();
        }
        private void MouseThreadMethod()
        {
            DateTime startTime = DateTime.Now;
            int MouseX = 0, MouseY = 0;
            inputSimulator.Mouse.MoveMouseTo(MouseX, MouseY);
            int MouseDeltaX = 10,MouseDeltaY=10;
            while ((DateTime.Now - startTime).TotalMinutes <= 2)
            {
                inputSimulator.Mouse.MoveMouseBy(MouseDeltaX, MouseDeltaY);
                Thread.Sleep(1000);
            }
        }
    }
}
