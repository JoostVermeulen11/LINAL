
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private string _point1X, _point1Y, _point1Z, _point2X, _point2Y, _point2Z, _degrees, _speed;

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

        public string Speed
        {
            get { return _speed; }
            set
            {
                if (value != _speed)
                {
                    // Replace dots with commas so the convert method works with decimal numbers written with dots
                    value = value.Replace('.', ',');
                    _speed = value;
                    OnPropertyChanged("Speed");
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

            Degrees = "3";
            Speed = "1";
        }

        public List<double> convertRotationParameters()
        {
            List<double> list = new List<double>();

            try
            {
                // Regex Matches to make sure only numbers are filled in
                list.Add(!String.IsNullOrWhiteSpace(Point1X) || Regex.IsMatch(Point1X, "\\w+") ? Convert.ToDouble(Point1X) : 0);
                list.Add(!String.IsNullOrWhiteSpace(Point1Y) || Regex.IsMatch(Point1Y, "\\w+") ? Convert.ToDouble(Point1Y) : 0);
                list.Add(!String.IsNullOrWhiteSpace(Point1Z) || Regex.IsMatch(Point1Z, "\\w+") ? Convert.ToDouble(Point1Z) : 0);
                list.Add(!String.IsNullOrWhiteSpace(Point2X) || Regex.IsMatch(Point2X, "\\w+") ? Convert.ToDouble(Point2X) : 0);
                list.Add(!String.IsNullOrWhiteSpace(Point2Y) || Regex.IsMatch(Point2Y, "\\w+") ? Convert.ToDouble(Point2Y) : 0);
                list.Add(!String.IsNullOrWhiteSpace(Point2Z) || Regex.IsMatch(Point2Z, "\\w+") ? Convert.ToDouble(Point2Z) : 0);
                                                              
                list.Add(!String.IsNullOrWhiteSpace(Degrees) || Regex.IsMatch(Degrees, "\\w+") ? Convert.ToDouble(Degrees) : 3);
            }
            catch (Exception fe)
            {
                Console.WriteLine(fe.Data);
            }

            return list;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Subtract)
            {
                _controller.zoomOut();
            }
            else if (e.Key == Key.Add)
            {
                _controller.zoomIn();
            }
            else if (e.Key == Key.Up)
            {
                _controller.LookatYUp();
            }
            else if (e.Key == Key.Down)
            {
                _controller.LookatYDown();
            }
            else if (e.Key == Key.X)
            {
                _controller.RotateX();
            }
            else if (e.Key == Key.Y)
            {
                _controller.RotateY();
            }
            else if (e.Key == Key.Z)
            {
                _controller.RotateZ();
            }
            
            else if (e.Key == Key.R)
            {
                // Fetch all the inputs from the rotate over fields
                var list = convertRotationParameters();
                if (list.Count >= 7)
                {
                    _controller.rotateOver(new Point3D { X = list[0], Y = list[1], Z = list[2] }, new Point3D { X = list[3], Y = list[4], Z = list[5] }, list[6]);
                }
            }
            else if (e.Key == Key.W)
            {
                double speed = !String.IsNullOrWhiteSpace(Speed) || !Regex.IsMatch(Speed, "\\w+") ? Convert.ToDouble(Speed) : 1;
            
                //translate up
                _controller.translate(0, speed, 0);
            }
            else if (e.Key == Key.S)
            {
                double speed = !String.IsNullOrWhiteSpace(Speed) || !Regex.IsMatch(Speed, "\\w+") ? Convert.ToDouble(Speed) : 1;
            
                //translate down            
                _controller.translate(0, -speed, 0);
            }
            else if (e.Key == Key.D)
            {
                double speed = !String.IsNullOrWhiteSpace(Speed) || !Regex.IsMatch(Speed, "\\w+") ? Convert.ToDouble(Speed) : 1;
            
                //translate right            
                _controller.translate(speed, 0, 0);
            }
            else if (e.Key == Key.A)
            {
                double speed = !String.IsNullOrWhiteSpace(Speed) || !Regex.IsMatch(Speed, "\\w+") ? Convert.ToDouble(Speed) : 1;
            
                //translate left            
                _controller.translate(-speed, 0, 0);
            }
            else if (e.Key == Key.Q)
            {
                double speed = !String.IsNullOrWhiteSpace(Speed) || !Regex.IsMatch(Speed, "\\w+") ? Convert.ToDouble(Speed) : 1;

                //translate front
                _controller.translate(0, 0, speed);
            }
            else if (e.Key == Key.E)
            {
                double speed = !String.IsNullOrWhiteSpace(Speed) || !Regex.IsMatch(Speed, "\\w+") ? Convert.ToDouble(Speed) : 1;

                //translate back
                _controller.translate(0, 0, -speed);
            }

        }
    }
}
