namespace DrawHexagon
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Media;
    using System.Xml.Serialization;

    [XmlInclude(typeof(Brushes))]
    [XmlInclude(typeof(SolidColorBrush))]
    [XmlInclude(typeof(MatrixTransform))]
    [Serializable]
    public class HexagonModel
    {
        private PointCollection points;
        private Brush stroke;
        private Brush fill;
        private int number;
        private double left;
        private double top;

        public Brush Stroke
        {
            get
            {
                return stroke;
            }
            set
            {
                stroke = value;
                OnPropertyChanged("Stroke");
            }
        }
        public Brush Fill
        {
            get
            {
                return fill;
            }
            set
            {
                fill = value;
                OnPropertyChanged("Fill");
            }
        }
        public PointCollection Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
                OnPropertyChanged("Ponts");
            }
        }
        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
                OnPropertyChanged("Number");
            }
        }
        public double Left
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
                OnPropertyChanged("Left");
            }
        }
        public double Top
        {
            get
            {
                return top;
            }
            set
            {
                top = value;
                OnPropertyChanged("Top");
            }
        }

        public HexagonModel()
        {
            Points = new PointCollection();
            Stroke = Brushes.Black;
            Fill = Brushes.Red;
            Number = 0;
            Left = 0;
            Top = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
