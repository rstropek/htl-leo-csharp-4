using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Screws
{
    /// <summary>
    /// Represents a single item in the order
    /// </summary>
    public record OrderItem(string ScrewType, decimal Amount, string UnitOfMeasure, decimal Price);

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// Represents a single item in the price list
        /// </summary>
        private record PriceListItem(decimal KgPer100Screws, decimal PricePer100Screws);

        /// <summary>
        /// Stores the price information for a screw. Screw type is dictionary key.
        /// </summary>
        private static readonly Dictionary<string, PriceListItem> PriceList = new()
        {
            { "M4, 6mm", new(0.133m, 1.35m) },
            { "M4, 8mm", new(0.149m, 1.40m) },
            { "M5, 6mm", new(0.218m, 1.65m) },
            { "M5, 8mm", new(0.238m, 1.80m) },
        };

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            // Set default values for comboboxes
            ScrewType = ScrewTypes.First();
            UnitOfMeasure = UnitsOfMeasure.First();
        }

        public ObservableCollection<OrderItem> Order { get; } = new();

        public decimal TotalPrice { get; private set; }

        public IEnumerable<string> ScrewTypes { get; } = new[]
        {
            "M4, 6mm",
            "M4, 8mm",
            "M5, 6mm",
            "M5, 8mm",
        };

        public string ScrewType { get; set; }

        public decimal Amount { get; set; } = 1;

        public IEnumerable<string> UnitsOfMeasure { get; } = new[]
        {
            "100er Packung",
            "kg",
        };

        public string UnitOfMeasure { get; set; }

        private void OnAddToCart(object sender, RoutedEventArgs e)
        {
            decimal numberOfPackages;
            if (UnitOfMeasure == "kg")
            {
                // Calculate number of packages for weight
                numberOfPackages = Math.Round(Amount / PriceList[ScrewType].KgPer100Screws);
            }
            else
            {
                numberOfPackages = Amount;
            }

            // Calculate price
            var price = numberOfPackages * PriceList[ScrewType].PricePer100Screws;

            // Add order
            Order.Add(new(ScrewType, Amount, UnitOfMeasure, price));

            // Refresh price
            TotalPrice += price;
            PropertyChanged?.Invoke(this, new(nameof(TotalPrice)));
        }
    }
}
