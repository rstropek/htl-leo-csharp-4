using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace DataBinding
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private const string BaseUrl = "https://cddataexchange.blob.core.windows.net/data-exchange/Cars/";
        public List<Car> Cars { get; set; } = new()
        {
            new(
                $"{BaseUrl}Car_1_01.png",
                "Ferrari",
                "What a great car from Italy. Only buy it in red, never buy a Ferrari in any other color."
            ),
            new(
                $"{BaseUrl}Car_2_01.png",
                "Police",
                "Epic police car. Looks like taken right out of a classic hollywood move."
            ),
            new(
                $"{BaseUrl}Car_3_01.png",
                "Lambo",
                "Lamborghini sports car. A MUST HAVE for every rapper on earth that wants to be cool."
            )
        };

        public MainWindow()
        {
            CurrentCar = Cars[0];

            InitializeComponent();
            DataContext = this;
        }

        private Car CurrentCarValue;
        public Car CurrentCar
        { 
            get { return CurrentCarValue; }
            set
            {
                CurrentCarValue = value;
                PropertyChanged?.Invoke(this, new(nameof(CurrentCar)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private enum NavigationDirection
        {
            Previous = -1,
            Next = 1
        }

        private void Navigate(NavigationDirection direction)
        {
            var newIndex = Cars.IndexOf(CurrentCar) + (int)direction;
            if (newIndex >= 0 && newIndex < Cars.Count)
            {
                CurrentCar = Cars[newIndex];
            }
        }

        private void OnNext(object sender, RoutedEventArgs e)
            => Navigate(NavigationDirection.Next);

        private void OnPrevious(object sender, RoutedEventArgs e)
            => Navigate(NavigationDirection.Previous);

        private void Edit(object sender, RoutedEventArgs e)
        {
            var dlg = new EditCarWindow
            {
                DataContext = CurrentCar
            };
            dlg.ShowDialog();
        }
    }
}
