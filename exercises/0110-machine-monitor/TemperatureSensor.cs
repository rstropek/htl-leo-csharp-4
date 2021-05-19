using System;

namespace MachineMonitor
{
    /// <summary>
    /// Represents a temperature reading
    /// </summary>
    public record TemperatureReading(double Value, bool IsCritical);

    /// <summary>
    /// Simulates a temperature sensor
    /// </summary>
    /// <remarks>
    /// This sample class generates artificial temperature readings based on
    /// a sinus curve. The readings are between 0 and <see cref="Amplitude"/>.
    /// </remarks>
    public class TemperatureSensor
    {
        // Date and time when sensor was created. Used to generate artifical temperature values.
        private readonly DateTimeOffset creationTimestamp = DateTimeOffset.UtcNow;

        // Random shift of sinus curve. Makes sure that every sensor produces slightly different values.
        private readonly double shift;

        // Period in ms for sinus curve
        private const long period = 20_000L;

        // Amplitude for generated values
        public const double Amplitude = 300d;

        public TemperatureSensor() => shift = Math.PI * new Random().NextDouble();

        public TemperatureReading GetTemperature()
        {
            // Simulate reading temperatur
            var duration = DateTimeOffset.UtcNow.Subtract(creationTimestamp).TotalMilliseconds / period * period;
            var reading = (Amplitude / 2d) + Amplitude / 2d * Math.Sin(shift + Math.PI * 2 * duration / period);
            return new(reading, reading is > (Amplitude * 0.9d) or < (Amplitude * 0.1d));
        }
    }
}
