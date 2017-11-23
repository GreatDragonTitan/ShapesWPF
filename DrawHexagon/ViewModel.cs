namespace DrawHexagon
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Shapes;

    public class ViewModel: INotifyPropertyChanged
    {
        private int xPos;
        public int XPos
        {
            get
            {
                return xPos;
            }
            set
            {
                xPos = value;
                OnPropertyChanged("XPos");
            }
        }

        private int yPos;
        public int YPos
        {
            get
            {
                return yPos;
            }
            set
            {
                yPos = value;
                OnPropertyChanged("YPos");
            }
        }

        private string mode;
        public string Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
                OnPropertyChanged("Mode");
            }
        }
        

        private ObservableCollection<Polygon> polygones;
        public ObservableCollection<Polygon> Polygones
        {
            get
            {
                return polygones;
            }
            set
            {
                polygones = value;
                OnPropertyChanged("Polygones");
            }
        }
       
        public ViewModel()
        {
            Polygones = new ObservableCollection<Polygon>();
            Mode = "Малювання";
            XPos = 0;
            YPos = 0;
        }

        public int Count
        {
            get
            {
                return Polygones.Count;
            }
        }

        public int Count_Last
        {
            get
            {
                if(Count>0)
                {
                    return Polygones.Last().Points.Count();
                }
                else
                {
                    throw new Exception("Empty collection of polygones.");
                }
            }
        }

        public void Add(Polygon _polygon)
        {
            Polygones.Add(_polygon);
        }

        public void Add_Last(Point _point)
        {
            Polygones.Last().Points.Add(_point);
        }
        public void Change_Last(Point _point)
        {
            Polygones.Last().Points[Count_Last - 1] = _point;
        }

        public void Serialize(string _path)
        {
            var s = new HexagonesModel();
            int j = 0;
            
            foreach(var i in Polygones)
            {
                s.Shapes.Add(new HexagonModel() { Points = i.Points, Fill = i.Fill, Stroke = i.Stroke, Number = j,Left = Canvas.GetLeft(i), Top = Canvas.GetTop(i)});
                ++j;
            }
            s.Serialize(_path);
        }

        public void Deserialize(string _path)
        {
            var s = new HexagonesModel();
            Polygones = new ObservableCollection<Polygon>();
            s.Deserialize(_path);
            foreach(var i in s.Shapes)
            {
                Add(new Polygon() { Points = i.Points, Fill = i.Fill, Stroke = i.Stroke });
                Canvas.SetLeft(Polygones.Last(), i.Left);
                Canvas.SetTop(Polygones.Last(), i.Top);
            }
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
