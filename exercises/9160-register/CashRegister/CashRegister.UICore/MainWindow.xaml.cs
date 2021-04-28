using CashRegister.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CashRegister.UICore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // Add a HttpClient instance that we can use to access our backend Web API.
        // Note that this field is `static` because we only need a single instance
        // of the HTTP client.
        private static readonly HttpClient HttpClient = new()
        {
            BaseAddress = new Uri("https://localhost:5001"),
            Timeout = TimeSpan.FromSeconds(5)
        };

        public MainWindow()
        {
            InitializeComponent();

            // Set the data context for data binding
            DataContext = this;

            async Task LoadProducts()
            {
                var products = await HttpClient.GetFromJsonAsync<List<Product>>("api/products");
                if (products == null || products.Count == 0) return;
                foreach (var product in products) Products.Add(product);
            }
            Loaded += async (_, __) => await LoadProducts();
        }

        public ObservableCollection<Product> Products { get; } = new();

        public ObservableCollection<ReceiptLineViewModel> Basket { get; } = new();

        public decimal TotalSum => Basket.Sum(rl => rl.TotalPrice);

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnAddProduct(object sender, RoutedEventArgs e)
        {
            // Note that the buttons were generated from our products through data binding.
            // Therefore, we can acccess the bound product through the sender button's
            // `DataContext` property. We just need to do some type casting.
            if (((Button)sender).DataContext is not Product selectedProduct) return;

            // Lookup the product based on the ID
            var product = Products.First(p => p.ID == selectedProduct.ID);

            // New product -> add item to basket
            Basket.Add(new ReceiptLineViewModel
            {
                ProductID = product.ID,
                Amount = 1,
                ProductName = product.ProductName,
                TotalPrice = product.UnitPrice
            });

            // Inform UI that total sum has changed
            PropertyChanged?.Invoke(this, new(nameof(TotalSum)));
        }

        private async void OnCheckout(object sender, RoutedEventArgs e)
        {
            // Turn all items in the basket into DTO objects
            var dto = Basket.Select(b => new ReceiptLineDto
            {
                ProductID = b.ProductID,
                Amount = b.Amount
            }).ToList();

            // Send the receipt to the backend
            var response = await HttpClient.PostAsJsonAsync("/api/receipts", dto);

            // Throw exception if something went wrong
            response.EnsureSuccessStatusCode();

            // Clear basket so shopping can start from scratch
            Basket.Clear();

            // Inform UI that total sum has changed
            PropertyChanged?.Invoke(this, new(nameof(TotalSum)));
        }
    }
}
