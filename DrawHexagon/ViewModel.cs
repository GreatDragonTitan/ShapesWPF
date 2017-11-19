using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawHexagon
{
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

        public bool doing { get; set; } = false;
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
        private ObservableCollection<Shape> nodes;
        public ObservableCollection<Shape> Nodes
        {
            get
            {
                return nodes;
            }
            set
            {
                nodes = value;
                OnPropertyChanged("Nodes");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            Nodes = new ObservableCollection<Shape>();
            Mode = "Draw";
        }

        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
