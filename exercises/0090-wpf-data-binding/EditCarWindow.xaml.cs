using System.Windows;

namespace DataBinding
{
    public partial class EditCarWindow : Window
    {
        public EditCarWindow()
        {
            InitializeComponent();
        }

        private void OnClose(object sender, RoutedEventArgs e) => Close();
    }
}
