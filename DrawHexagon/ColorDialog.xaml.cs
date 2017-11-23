namespace DrawHexagon
{
    using System.Windows;

    public partial class ColorDialog : Window
    {
        public ColorDialog()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
