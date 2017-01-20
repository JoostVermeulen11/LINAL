
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Linal_wk1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private ObjectController _controller;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _point1X, _point1Y, _point1Z, _point2X, _point2Y, _point2Z, _degrees, _translationSpeed;

        public string Point1X
        {
            get { return _point1X; }
            set
            {
                if (value != _point1X)
                {
                    // Replace dots with commas so the convert method works with decimal numbers written with dots
                    value = value.Replace('.', ',');
                    _point1X = value;
                    OnPropertyChanged("Point1X");
                }
            }
        }

        public string Point1Y
        {
            get { return _point1Y; }
            set
            {
                if (value != _point1Y)
                {
                    // Replace dots with commas so the convert method works with decimal numbers written with dots
                    value = value.Replace('.', ',');
                    _point1Y = value;
                    OnPropertyChanged("Point1Y");
                }
            }
        }

        public string Point1Z
        {
            get { return _point1Z; }
            set
            {
                if (value != _point1Z)
                {
                    // Replace dots with commas so the convert method works with decimal numbers written with dots
                    value = value.Replace('.', ',');
                    _point1Z = value;
                    OnPropertyChanged("Point1Z");
                }
            }
        }

        public string Point2X
        {
            get { return _point2X; }
            set
            {
                if (value != _point2X)
                {
                    // Replace dots with commas so the convert method works with decimal numbers written with dots
                    value = value.Replace('.', ',');
                    _point2X = value;
                    OnPropertyChanged("Point2X");
                }
            }
        }

        public string Point2Y
        {
            get { return _point2Y; }
            set
            {
                if (value != _point2Y)
                {
                    // Replace dots with commas so the convert method works with decimal numbers written with dots
                    value = value.Replace('.', ',');
                    _point2Y = value;
                    OnPropertyChanged("Point2Y");
                }
            }
        }

        public string Point2Z
        {
            get { return _point2Z; }
            set
            {
                if (value != _point2Z)
                {
                    // Replace dots with commas so the convert method works with decimal numbers written with dots
                    value = value.Replace('.', ',');
                    _point2Z = value;
                    OnPropertyChanged("Point2Z");
                }
            }
        }

        public string Degrees
        {
            get { return _degrees; }
            set
            {
                if (value != _degrees)
                {
                    // Replace dots with commas so the convert method works with decimal numbers written with dots
                    value = value.Replace('.', ',');
                    _degrees = value;
                    OnPropertyChanged("Degrees");
                }
            }
        }

        public string TranslationSpeed
        {
            get { return _translationSpeed; }
            set
            {
                if (value != _translationSpeed)
                {
                    // Replace dots with commas so the convert method works with decimal numbers written with dots
                    value = value.Replace('.', ',');
                    _translationSpeed = value;
                    OnPropertyChanged("TranslationSpeed");
                }
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            _controller = new ObjectController(this);

            // Set the default values of the textboxes.
            Point1X = "0";
            Point1Y = "0";
            Point1Z = "0";

            Point2X = "0";
            Point2Y = "0";
            Point2Z = "0";

            Degrees = "5";

            TranslationSpeed = "1";
        }

        public List<double> convertAll()
        {
            List<double> list = new List<double>();

            try
            {
                list.Add(!String.IsNullOrWhiteSpace(Point1X) ? Convert.ToDouble(Point1X) : 0);
                list.Add(!String.IsNullOrWhiteSpace(Point1Y) ? Convert.ToDouble(Point1Y) : 0);
                list.Add(!String.IsNullOrWhiteSpace(Point1Z) ? Convert.ToDouble(Point1Z) : 0);
                list.Add(!String.IsNullOrWhiteSpace(Point2X) ? Convert.ToDouble(Point2X) : 0);
                list.Add(!String.IsNullOrWhiteSpace(Point2Y) ? Convert.ToDouble(Point2Y) : 0);
                list.Add(!String.IsNullOrWhiteSpace(Point2Z) ? Convert.ToDouble(Point2Z) : 0);

                list.Add(!String.IsNullOrWhiteSpace(Degrees) ? Convert.ToDouble(Degrees) : 5);
                list.Add(!String.IsNullOrWhiteSpace(TranslationSpeed) ? Convert.ToDouble(TranslationSpeed) : 1);
            }
            catch (Exception fe)
            {
                Console.WriteLine(fe.Data);
            }

            return list;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var list = convertAll();
            if (e.Key == Key.Subtract)
            {
                _controller.zoomOut();
            }
            if (e.Key == Key.Add)
            {
                _controller.zoomIn();
            }
            if (e.Key == Key.X)
            {
                _controller.RotateX();
            }
            if (e.Key == Key.Y)
            {
                _controller.RotateY();
            }
            if (e.Key == Key.Z)
            {
                _controller.RotateZ();
            }
            if (e.Key == Key.S)
            {        
                _controller.rotateOver(new Point3D { X = list[0], Y = list[1], Z = list[2] }, new Point3D { X = list[3], Y = list[4], Z = list[5] }, list[6]);
            }
            if (e.Key == Key.NumPad8)
            {
                //translate up
                _controller.translate(0, list[7], 0);
            }
            if (e.Key == Key.NumPad5)
            {
                //translate down
                _controller.translate(0, (list[7] * -1), 0);
            }
            if (e.Key == Key.NumPad6)
            {
                //translate right
                _controller.translate(list[7], 0, 0);
            }
            if (e.Key == Key.NumPad4)
            {
                //translate left
                _controller.translate((list[7] * -1), 0, 0);
            }
            if(e.Key == Key.V)
            {
                //translate front
                _controller.translate(0, 0, list[7]);
            }
            if (e.Key == Key.B)
            {
                //translate front
                _controller.translate(0, 0, (list[7] * -1));
            }

        }
    }
}
