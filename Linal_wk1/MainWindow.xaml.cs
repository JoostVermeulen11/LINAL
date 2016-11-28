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
        private List<Vector> vectorList;
        private Random rnd;
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
        private string _selectedX1, _selectedX2, _selectedY1, _selectedY2;
        public string SelectedX1
        {
            get { return _selectedX1; }
            set
            {
                if (value != _selectedX1)
                {
                    _selectedX1 = value;
                    OnPropertyChanged("SelectedX1");
                }
            }
        }
        public string SelectedX2
        {
            get { return _selectedX2; }
            set
            {
                if (value != _selectedX2)
                {
                    _selectedX2 = value;
                    OnPropertyChanged("SelectedX2");
                }
            }
        }
        public string SelectedY1
        {
            get { return _selectedY1; }
            set
            {
                if (value != _selectedY1)
                {
                    _selectedY1 = value;
                    OnPropertyChanged("SelectedY1");
                }
            }
        }
        public string SelectedY2
        {
            get { return _selectedY2; }
            set
            {
                if (value != _selectedY2)
                {
                    _selectedY2 = value;
                    OnPropertyChanged("SelectedY2");
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            RandomColor r = new RandomColor();
            vectorList = new List<Vector>();
            rnd = new Random();
            DataContext = this;
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

        private void CreateVectors_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedX1 == "" || SelectedX2 == "" || SelectedY1 == "" || SelectedY2 == "")
                return;

            try
            {
                vectorList.Add(new Vector(Convert.ToDouble(SelectedX1), Convert.ToDouble(SelectedX2), Convert.ToDouble(SelectedY1), Convert.ToDouble(SelectedY2)));

                clearValues();

                drawObjects();
            }
            catch(Exception ex)
            {
                ex.ToString();
            }            
        }

        private void drawObjects()
        {
            Assenstelsel.Children.Clear();
            foreach (var vector in vectorList)
            {                
                Assenstelsel.Children.Add(vector.getVector());                
            }
        }

        private void clearValues()
        {
            SelectedX1 = string.Empty;
            SelectedX2 = string.Empty;
            SelectedY1 = string.Empty;
            SelectedY2 = string.Empty;
            LabelLength.Content = string.Empty;
        }

        private void DeleteAllVectors_Click(object sender, RoutedEventArgs e)
        {
            vectorList.Clear();
            Assenstelsel.Children.Clear();
        }

        private void AddVector_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedVector == null || SelectedVector2 == null)
                return;

            vectorList.Add(Vector.ADD(SelectedVector, SelectedVector2));
            AddVector.IsEnabled = false;
            clearValues();
            drawObjects();
        }

        private void DistractVector_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedVector == null || SelectedVector2 == null)
                return;

            vectorList.Add(Vector.DISTRACT(SelectedVector, SelectedVector2));
            AddVector.IsEnabled = false;
            clearValues();
            drawObjects();
        }

        private void DeleteVectors_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedVector == null)
                return;
           
            vectorList.Remove(SelectedVector);

            clearValues();

            drawObjects();
        }

        private void Assenstelsel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {       
            if (e.OriginalSource is ArrowLine)
            {
                ArrowLine ClickedArrow = (ArrowLine)e.OriginalSource;

                Vector ClickedVector = vectorList.Where(i => i.xPos == ClickedArrow.X1 / 50 && i.yPos == ClickedArrow.Y1 / 50 && (i.xPos + i.deltaX) == ClickedArrow.X2 / 50 && (i.yPos + i.deltaY) == ClickedArrow.Y2 / 50).FirstOrDefault();

                if(SelectedVector == null)
                    SelectedVector = ClickedVector;
                else
                {
                    SelectedVector2 = ClickedVector;
                    AddVector.IsEnabled = true;
                }

                SelectedX1 = (ClickedVector.xPos).ToString();
                SelectedX2 = (ClickedVector.yPos).ToString();
                SelectedY1 = (ClickedVector.deltaX).ToString();
                SelectedY2 = (ClickedVector.deltaY).ToString();
            }
        }

        private void scaleVector_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedVector == null)
                return;
            else
                SelectedVector.Scale(double.Parse(txtScaleX.Text), double.Parse(txtScaleY.Text));

            drawObjects();
        }
    }
}
