using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DrawHexagon
{
    public class Shape: INotifyPropertyChanged
    {
        private Geometry geometry;
        private Brush stroke;
        private Brush fill;
        private int number;

        public Geometry Geometry
        {
            get
            {
                return geometry;
            }
            set
            {
                geometry = value;
                OnPropertyChanged("Geometry");
            }
        }
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

        public Shape()
        {
            Geometry = new PathGeometry {
                Figures = new PathFigureCollection(new List<PathFigure>()
                {
                    new PathFigure(new Point(0, 0), new List<PathSegment>(), true)
                })
            };
            Number = 0;
        }

        public Shape(params Point[] _points)
        {
            Geometry = new PathGeometry
            {
                Figures = new PathFigureCollection(new List<PathFigure>()
                {
                    new PathFigure(_points[0], new List<PathSegment>(), true)
                })
            };
            var pathGeometry = Geometry as PathGeometry;
            for (int i=1;i<_points.Length;++i)
            {
                if(pathGeometry != null)
                {
                    pathGeometry.Figures.Last().Segments.Add(new LineSegment(_points[i], true));
                }
            }
            Number = 0;
        }

        public void Add(Point _point)
        {
            var pathGeometry = Geometry as PathGeometry;
            if (pathGeometry != null)
            {
                pathGeometry.Figures.Last().Segments.Add(new LineSegment(_point, true));
            }
        }

        public int Count
        {
            get
            {
                var pathGeometry = Geometry as PathGeometry;
                return pathGeometry.Figures.Last().Segments.Count;
            }
        }

        public Point this[int _index]
        {
            get
            {
                var pathGeometry = Geometry as PathGeometry;
                return (pathGeometry.Figures.Last().Segments[_index] as LineSegment).Point;
            }
            set
            {
                var pathGeometry = Geometry as PathGeometry;
                (pathGeometry.Figures.Last().Segments[_index] as LineSegment).Point = value;
            }
        }

        public void Move(int _x, int _y)
        {
            PathGeometry pathGeometry = Geometry as PathGeometry;
            if(pathGeometry!=null)
            {
                for(int i=0;i<Count;++i)
                {
                    this[i] = new Point(this[i].X + _x, this[i].Y + _y);
                }
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
