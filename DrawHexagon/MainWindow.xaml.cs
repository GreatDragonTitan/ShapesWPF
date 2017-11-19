using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
namespace DrawHexagon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        bool activated = false;
        Point point;
        Point pointO;
        ViewModel vm = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            
           
            DataContext = vm;
        }
        private void MouseDownShape(object sender, MouseButtonEventArgs e)
        {
            if (vm.Mode == "Select" && !activated)
            { 
                activated = true;
                point = new Point(vm.XPos, vm.YPos);
                pointO = new Point(Canvas.GetLeft(sender as UIElement), Canvas.GetTop(sender as UIElement));
            }
        }

        private void MouseMoveShape(object sender, MouseEventArgs e)
        {
            if (activated && vm.Mode == "Select")
            {
                var pG = sender as UIElement;
                if (pG != null)
                {
                    //vm.Mode = Canvas.GetTop(pG).ToString();
                    Canvas.SetLeft(pG, pointO.X + vm.XPos - point.X);
                    Canvas.SetTop(pG, pointO.Y + vm.YPos - point.Y);
                }
            }
        }

        private void MouseUpShape(object sender, MouseButtonEventArgs e)
        {
            if(vm.Mode == "Select")
            {
                activated = false;
            }
            
        }

        private void CanvasArea_MouseMove(object sender, MouseEventArgs e)
        {
            vm.YPos = (int)Mouse.GetPosition(sender as UIElement).Y;
            vm.XPos = (int)Mouse.GetPosition(sender as UIElement).X;
            if (vm.Mode == "Draw")
            { 
                if (vm.doing)
                {
                    vm.Nodes.Last()[vm.Nodes.Last().Count - 1] = Mouse.GetPosition(sender as UIElement);
                }
            }
        }

        private void CanvasArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (vm.Mode == "Draw")
            {
                if (!vm.doing)
                {
                    vm.doing = true;
                    vm.Nodes.Add(new Shape(new Point(vm.XPos, vm.YPos)) { Number = vm.Nodes.Count });
                    vm.Nodes.Last().Add(new Point(vm.XPos, vm.YPos));
                }
                else
                {
                    vm.Nodes.Last().Add(new Point(vm.XPos, vm.YPos));
                    if (vm.Nodes.Last().Count == 6)
                    {
                        vm.doing = false;

                    }
                }
            }
        }

        private void SelectMode_Click(object sender, RoutedEventArgs e)
        {
            if(vm.doing)
            {
                vm.Nodes.RemoveAt(vm.Nodes.Count - 1);
                vm.doing = false;
                vm.Mode = "Select";
            }
            vm.Mode = "Select";
        }

        private void DrawMode_Click(object sender, RoutedEventArgs e)
        {
            vm.Mode = "Draw";
        }
    }
}
