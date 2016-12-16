using Petzold.Media2D;
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
        private Vector _selectedVector;
        public Vector SelectedVector
        {
            get { return _selectedVector; }
            set
            {
                if (value != _selectedVector)
                {
                    _selectedVector = value;
                    OnPropertyChanged("SelectedVector");
                }
            }
        }
        private Vector _selectedVector2;
        public Vector SelectedVector2
        {
            get { return _selectedVector2; }
            set
            {
                if (value != _selectedVector2)
                {
                    _selectedVector2 = value;
                    OnPropertyChanged("SelectedVector2");
                }
            }
        }
        private Matrix _selectedMatrix;
        public Matrix SelectedMatrix
        {
            get { return _selectedMatrix; }
            set
            {
                if (value != _selectedMatrix)
                {
                    _selectedMatrix = value;
                    OnPropertyChanged("SelectedMatrix");
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            _controller = new ObjectController(this);
            AddVector.IsEnabled = false;

            for (int i = 1; i <= 9; i++)
            { 
                labelXLine.Content += i.ToString() + "             ";
            }

            for (int i = 10; i <= 16; i++)
            {
                labelXLine2.Content += i.ToString() + "           ";
            }

            for (int i = 14; i > 0; i--)
            {
                labelYLine.Content += i.ToString() + "\n\n\n";
            }
        }

        private void AddVector_Click(object sender, RoutedEventArgs e)
        {
            _controller.ADDVector(SelectedVector, SelectedVector2);      
            AddVector.IsEnabled = false;
            SelectedVector.getVector().Opacity = 1;
            SelectedVector = null;
            SelectedVector2.getVector().Opacity = 1;
            SelectedVector2 = null;
        }

        private void ScaleMatrix_Click(object sender, RoutedEventArgs e)
        {
            _controller.ScaleMatrix(double.Parse(ScaleMatrixX.Text), double.Parse(ScaleMatrixY.Text), SelectedMatrix);
            
            SelectedMatrix.getLine().Opacity = 1;
            SelectedMatrix = null;
        }

        private void TranslateMatrix_Click(object sender, RoutedEventArgs e)
        {
            _controller.TranslateMatrix(double.Parse(translateMatrixX.Text), double.Parse(TranslateMatrixY.Text), SelectedMatrix);

            SelectedMatrix.getLine().Opacity = 1;
            SelectedMatrix = null;
        }

        private void Assenstelsel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {       
            if (e.OriginalSource is ArrowLine)
            {
                ArrowLine ClickedArrow = (ArrowLine)e.OriginalSource;

                Vector ClickedVector = _controller.getVectors().Where(i => i.xPos == ClickedArrow.X1 / 50 && i.yPos == ClickedArrow.Y1 / 50 && (i.xPos + i.deltaX) == ClickedArrow.X2 / 50 && (i.yPos + i.deltaY) == ClickedArrow.Y2 / 50).FirstOrDefault();

                if (SelectedVector == null)
                {
                    SelectedVector = ClickedVector;
                    SelectedVector.getVector().Opacity = 0.5;
                }                   
                else
                {
                    SelectedVector2 = ClickedVector;
                    SelectedVector2.getVector().Opacity = 0.5;
                    AddVector.IsEnabled = true;
                }
            }
            else if (e.OriginalSource is Polyline)
            {
                Polyline ClickedRectangle = (Polyline)e.OriginalSource;

                Matrix ClickedMatrix = _controller.getMatrixes().Where(i => i.getColor() == ClickedRectangle.Stroke).FirstOrDefault();

                if (SelectedMatrix == null)
                    SelectedMatrix = ClickedMatrix;

                SelectedMatrix.getLine().Opacity = 0.5;
            }
        }

        private void scaleVector_Click(object sender, RoutedEventArgs e)
        {
            _controller.ScaleVector(double.Parse(txtScaleX.Text), double.Parse(txtScaleY.Text), SelectedVector);
            SelectedVector.getVector().Opacity = 1;
            SelectedVector = null;
        }

        private void RotateMatrix_Click(object sender, RoutedEventArgs e)
        {
            _controller.RotateMatrix(double.Parse(RotateText.Text), SelectedMatrix);

            SelectedMatrix.getLine().Opacity = 1;
            SelectedMatrix = null;
        }

        private void RotateSpecificPoint_Click(object sender, RoutedEventArgs e)
        {
            _controller.RotateSpecificPoint(SelectedMatrix, double.Parse(RotateText.Text), 
                double.Parse(RotateSpecificX.Text), double.Parse(RotateSpecificY.Text));
            
            SelectedMatrix.getLine().Opacity = 1;
            SelectedMatrix = null;
        }

        private void Animate_Click(object sender, RoutedEventArgs e)
        {
            _controller.getTimer().Start();
        }
    }
}
