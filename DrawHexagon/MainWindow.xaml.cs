using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
namespace DrawHexagon
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public partial class MainWindow : Window
    {
        Point point;
        Point pointO;
        ViewModel vm = new ViewModel();
        bool moving = false;
        bool drawing = false;
        public MainWindow()
        {
            InitializeComponent();
            vm.Add(new Polygon() { Points = new PointCollection(){ new Point(0, 0), new Point(0, 100), new Point(100, 0) }, Fill = Brushes.Red, Stroke = Brushes.Black });
            vm.Add(new Polygon() { Points = new PointCollection() { new Point(200, 50), new Point(90, 300), new Point(10, 100) }, Fill = Brushes.Red, Stroke = Brushes.Black });
            vm.Add(new Polygon() { Points = new PointCollection() { new Point(60, 100), new Point(70, 80), new Point(90, 120), new Point(200, 120) }, Fill = Brushes.Red, Stroke = Brushes.Black });

            DataContext = vm;
        }

        

        private void MouseDownShape(object sender, MouseButtonEventArgs e)
        {
            if (vm.Mode == "Пересування" && !moving)
            {
                point = new Point(vm.XPos, vm.YPos);
                pointO = new Point(Canvas.GetLeft(sender as UIElement), Canvas.GetTop(sender as UIElement));
                moving = true;
                var element = (UIElement)sender;
                element.CaptureMouse();
                Panel.SetZIndex(element, 1);
            }
        }

        private void MouseMoveShape(object sender, MouseEventArgs e)
        {
            if (vm.Mode == "Пересування" && moving)
            {
                var element = (UIElement)sender;
                Canvas.SetLeft(element, pointO.X + vm.XPos - point.X);
                Canvas.SetTop(element, pointO.Y + vm.YPos - point.Y);
            }
        }

        private void MouseUpShape(object sender, MouseButtonEventArgs e)
        {
            if (moving && vm.Mode == "Пересування")
            {
                var element = (UIElement)sender;

                element.ReleaseMouseCapture();
                Canvas.SetZIndex(element, 0);
                moving = false;
            }
        }

        private void CanvasArea_MouseMove(object sender, MouseEventArgs e)
        {
            vm.YPos = (int)Mouse.GetPosition(sender as UIElement).Y;
            vm.XPos = (int)Mouse.GetPosition(sender as UIElement).X;

            if (vm.Mode == "Малювання" && drawing)
            { 
                vm.Change_Last(new Point(vm.XPos, vm.YPos));
            }
        }

        private void CanvasArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (vm.Mode == "Малювання" )
            {
                if (!drawing)
                {
                    drawing = true;
                    vm.Add(new Polygon() { Points = new PointCollection() { new Point(vm.XPos, vm.YPos), new Point(vm.XPos, vm.YPos) }, Fill = Brushes.Black, Stroke = Brushes.Red });

                }
                else
                {
                    vm.Add_Last(new Point(vm.XPos, vm.YPos));
                    if (vm.Count_Last >= 7)
                    {
                        ColorDialog dlg = new ColorDialog();
                        
                        dlg.Owner = this;

                        dlg._fillColorPicker.SelectedColor = (vm.Polygones.Last().Fill as SolidColorBrush).Color;

                        dlg._strokeColorPicker.SelectedColor = (vm.Polygones.Last().Stroke as SolidColorBrush).Color;

                        dlg.ShowDialog();

                        if (dlg.DialogResult == true)
                        {
                            vm.Polygones.Last().Fill = new SolidColorBrush((Color)dlg._fillColorPicker.SelectedColor);
                            vm.Polygones.Last().Stroke = new SolidColorBrush((Color)dlg._strokeColorPicker.SelectedColor);
                        }
                        
                        drawing = false;
                    }
                }
            }

        }

        private void SelectMode_Click(object sender, RoutedEventArgs e)
        {
            
            if(drawing)
            {
                vm.Polygones.RemoveAt(vm.Count - 1);
                drawing = false;
            }
            vm.Mode = "Пересування";
        }

        private void DrawMode_Click(object sender, RoutedEventArgs e)
        {
            
            vm.Mode = "Малювання";
            
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string _path = "1.xml";
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = ".xml";
            dialog.Filter = "Xml document (.xml)|*.xml";
            if (dialog.ShowDialog() == true)
            {
                _path = dialog.FileName;
            }
            vm.Serialize(_path);
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            string _path = "1.xml";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".xml";
            dialog.Filter = "Xml document (.xml)|*.xml";
            if (dialog.ShowDialog() == true)
            {
                _path = dialog.FileName;
            }
            vm.Deserialize(_path);
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            vm.Polygones = new ObservableCollection<Polygon>();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
