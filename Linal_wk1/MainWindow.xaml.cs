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

                Vector ClickedVector = vectorList.Where(i => i.x1 == ClickedArrow.X1 && i.x2 == ClickedArrow.X2 && i.y1 == ClickedArrow.Y1 && i.y2 == ClickedArrow.Y2).FirstOrDefault();

                SelectedVector = ClickedVector;

                SelectedX1 = (ClickedVector.x1 / 50).ToString();
                SelectedX2 = (ClickedVector.x2 / 50).ToString();
                SelectedY1 = (ClickedVector.y1 / 50).ToString();
                SelectedY2 = (ClickedVector.y2 / 50).ToString();
            }
        }

        private void scaleVector_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedVector == null)
                return;
            else
                SelectedVector.Scale(int.Parse(textScale.Text));

            drawObjects();
        }
    }
}
