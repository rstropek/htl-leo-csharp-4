using System.Collections.ObjectModel;

namespace MachineMonitor
{
    public record SensorWithValues(
        string Name,
        TemperatureSensor Sensor,
        ObservableCollection<TemperatureReading> Temperatures);
}
