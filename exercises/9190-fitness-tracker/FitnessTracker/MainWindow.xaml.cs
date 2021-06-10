using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace FitnessTracker
{
    public record Activity(string Sport, DateTime StartDateTime, TimeSpan Duration, double Kcal);

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            SelectedSport = Sports[0];

            Activities.Add(new(Sports[0], DateTime.Now.AddDays(-3), TimeSpan.FromMinutes(32d), 350d));
            Activities.Add(new(Sports[0], DateTime.Now.AddDays(-2), TimeSpan.FromMinutes(45d), 500d));
            Activities.Add(new(Sports[1], DateTime.Now.AddDays(-1), TimeSpan.FromMinutes(110d), 900d));
        }

        public string[] Sports { get; set; } = new[] { "Laufen", "Radfahren" };

        public string SelectedSport { get; set; }

        public ObservableCollection<Activity> Activities { get; set; } = new();

        public string StartStopText { get; set; } = "Start";

        public Activity? SelectedActivity { get; set; }

        public DateTime Start { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnClick(object sender, EventArgs ea)
        {
            if (StartStopText == "Start")
            {
                StartStopText = "Stop";
                Start = DateTime.Now;
            }
            else
            {
                StartStopText = "Start";
                var duration = TimeSpan.FromSeconds(Math.Round((DateTime.Now - Start).TotalSeconds, 0));
                var effortFactor = 30 * 60 * duration.TotalSeconds;
                Activities.Add(new(SelectedSport, Start, duration, Math.Round(SelectedSport switch
                {
                    "Laufen" => 400d,
                    "Radfahren" => 350d,
                    _ => throw new NotImplementedException()
                } / effortFactor, 2)));
            }

            PropertyChanged?.Invoke(this, new(nameof(StartStopText)));
        }

        public void OnDelete(object sender, EventArgs ea)
        {
            if (SelectedActivity != null)
            {
                Activities.Remove(SelectedActivity);
            }
        }
    }
}
