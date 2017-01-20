
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

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            _controller = new ObjectController(this);                     
        }               

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Subtract)
            {
                _controller.zoomOut();
            }
            if(e.Key == Key.Add)
            {
                _controller.zoomIn();
            }
            if(e.Key == Key.Up)
            {
                _controller.LookatYUp();
            }
            if (e.Key == Key.Down)
            {
                _controller.LookatYDown();
            }
            if(e.Key == Key.X)
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
                _controller.rotateOver(new Point3D { X = 20, Y = 0, Z = 0 }, new Point3D { X = 25, Y = 1, Z = 0 });
            }
            if(e.Key == Key.Up)
            {
                _controller.translate(1,1,1);                
                //translate up
            }
            if(e.Key == Key.Down)
            {
                //translate down
            }
            if(e.Key == Key.Right)
            {
                //translate right
            }
            if(e.Key == Key.Left)
            {
                //translate left
            }

        }
    }
}
