using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace MachineMonitor
{
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer timer = new();
        private const int numberOfSensors = 10;

        public List<SensorWithValues> Sensors { get; } = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            // Generate sensors
            for (var i = 0; i < numberOfSensors; i++)
            {
                Sensors.Add(new SensorWithValues($"Sensor {i + 1}", new(), new()));
            }

            // Setup UI refresh interval
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += (_, _) =>
            {
                // Iterate over all sensors
                foreach (var (_, sensor, temperatures) in Sensors)
                {
                    // Get current temperature and store it in value history
                    temperatures.Insert(0, sensor.GetTemperature());

                    // Remove old values
                    while (temperatures.Count > 20) temperatures.RemoveAt(temperatures.Count - 1);
                }
            };
            timer.Start();
        }

        protected override void OnContentRendered(EventArgs _)
        {
            SensorSelection.SelectAll();
        }
    }
}
