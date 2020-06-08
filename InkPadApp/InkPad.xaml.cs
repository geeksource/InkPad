using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InkPadApp
{
    //https://bitsbobsetc.wordpress.com/2015/10/25/a-multitouch-inkcanvas-control/
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
            e.Stroke.AddPropertyData(Guid.NewGuid(), StrokeTimeStamp);
        }
        private List<Stroke> StrokesTobePlayed = new List<Stroke>();
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {

            //Lets clear the inkcanvas
            StrokesTobePlayed.AddRange(inkCanvas.Strokes.Clone());
            inkCanvas.Strokes.Clear();



        }
        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            DrawRandomDrawing();
            /*
            for (int i = 0; i < StrokesTobePlayed.Count; i++)
            {
                try
                {
                    Guid StrokGUID = StrokesTobePlayed[i].GetPropertyDataIds()[0];
                    DateTime startTime = (DateTime)StrokesTobePlayed[i].GetPropertyData(StrokGUID);

                    Guid NextStrokGUID = StrokesTobePlayed[i + 1].GetPropertyDataIds()[0];
                    DateTime NextStrokeTime = (DateTime)StrokesTobePlayed[i + 1].GetPropertyData(NextStrokGUID);

                    //Draw the Stroke

                    inkCanvas.Strokes.Add(StrokesTobePlayed[i]);
                    //Wait for next stroke to draw
                    Thread.Sleep((int)(NextStrokeTime - startTime).TotalMilliseconds);
                }
                catch (Exception ex)
                {

                }

            }*/
        }
        private void PlayStroke()
        {

        }
        private DateTime? StrokeTimeStamp = null;
        private void InkCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void InkCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && StrokeTimeStamp == null)
            {
                StrokeTimeStamp = DateTime.Now;
            }
        }


        #region Drawing Strokes
        private void DrawRandomDrawing()
        {
            StylusPointCollection stylusPoints = new StylusPointCollection();
            int x = 100, y = 100;
            for(int i=0;i<10;i++)
            {
                stylusPoints.Add(new StylusPoint() { X = x, Y = y, PressureFactor = 0.1f });
                x++;
                y++;
            }
            for (int i = 0; i < 10; i++)
            {
                stylusPoints.Add(new StylusPoint() { X = x, Y = y, PressureFactor = 0.5f });
                x++;
                y++;
            }
            Stroke stroke = new Stroke(stylusPoints, inkCanvas.DefaultDrawingAttributes);
            DrawStroke(stroke, "/mohsin/abbas");
        }


        private Guid purposeGuid = new Guid("12345678-9012-3456-7890-123456789012");
        private delegate void Draw_Stroke_Delegate(Stroke Stroke, string StrokeKey);
        public void DrawStroke(Stroke stroke, string strokKey)
        {
            inkCanvas.Dispatcher.BeginInvoke(new Draw_Stroke_Delegate(Draw_Stroke), new object[] { stroke, strokKey });
        }
        //private void Draw_Stroke(byte[] _stroke, string _strokeKey)
        private void Draw_Stroke(Stroke _stroke, string _strokeKey)
        {
            try
            {
                _stroke.AddPropertyData(purposeGuid, _strokeKey);
                inkCanvas.Strokes.Add(_stroke);
                /*
                using (MemoryStream ms = new MemoryStream(_stroke))
                {
                    //DrawingAnimation(ms);
                    // return;
                    InkCanvas iTemp2 = new InkCanvas();
                    iTemp2.Strokes = new System.Windows.Ink.StrokeCollection(ms);
                    ms.Close();
                    foreach (System.Windows.Ink.Stroke stroke in iTemp2.Strokes)
                    {
                        try
                        {
                            stroke.AddPropertyData(purposeGuid, _strokeKey);
                        }
                        catch (Exception ex)
                        {

                        }
                        inkCanvas.Strokes.Add(stroke);
                        //inkCanvas.Strokes.Add(ScaleStrokes(stroke));
                    }
                    */
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

    }
}
