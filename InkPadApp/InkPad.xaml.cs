using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace InkPadApp
{
    /// <summary>
    /// Interaction logic for InkPad.xaml
    /// </summary>
    public partial class InkPad : Window, INotifyPropertyChanged
    {
        private Brush _SelectedColor = Brushes.Black;
        public Brush SelectedColor
        {
            get
            {
                return _SelectedColor;
            }
            set
            {
                _SelectedColor = value;
                inkCanvas.DefaultDrawingAttributes.Color = ((SolidColorBrush)value).Color;
                OnPropertyChanged("SelectedColor");
            }
        }

        private int _sliderValue = 0;
        public int SiderValue
        {
            get
            {
                return _sliderValue;
            }
            set
            {
                _sliderValue = value;
                OnPropertyChanged("SiderValue");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public InkPad()
        {
            DataContext = this;
            InitializeComponent();
        }
        private void ColorGridRectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle source = sender as Rectangle;
            if (source != null)
                SelectedColor = source.Fill;
        }
        
        private void InkCanvas_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            e.Stroke.AddPropertyData(new Guid(StrokeTimeStamp.Value.Ticks.ToString()), StrokeTimeStamp);
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {

        }
        private DateTime? StrokeTimeStamp = null;
        private void InkCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed && StrokeTimeStamp==null)
            {
                StrokeTimeStamp = DateTime.Now;
            }
        }

        private void InkCanvas_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
