using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace LogAnalysis.Logic
{
    public record LogItem(string Url, DateTime DownloadTimestamp)
    {
        public LogItem(string url, string downloadDateString, string downloadTime)
            : this(url, DateTime.Parse(downloadDateString) + TimeSpan.Parse(downloadTime)) { }
    }

    public record MonthlyStatisticItem(string YearMonth, int Downloads);

    public record HourlyStatisticItem(string Hour, float Percentage);

    public record UrlData<T>(string Url, IEnumerable<T> Data);

    public class Photographer
    {
        [JsonPropertyName("pic")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("takenBy")]
        public string TakenBy { get; set; } = string.Empty;
    }

    public static class Analyzer
    {
        public static IEnumerable<LogItem> ToLogItems(this IEnumerable<string> lines, string? urlFilter = null) =>
            lines.Skip(1)
                .Where(l => !string.IsNullOrEmpty(l))
                .Select(l => l.Split('\t'))
                .Where(l => urlFilter == null || l[0] == urlFilter)
                .Select(l => new LogItem(l[0], l[1], l[2]));

        public static IEnumerable<UrlData<MonthlyStatisticItem>> CalculateMonthlyStatistics(this IEnumerable<LogItem> log) =>
            log.GroupBy(l => l.Url)
            .OrderBy(l => l.Key)
            .Select(u => new UrlData<MonthlyStatisticItem>(u.Key,
                u.GroupBy(m => m.DownloadTimestamp.ToString("yyyy-MM"))
                    .OrderBy(m => m.Key)
                    .Select(m => new MonthlyStatisticItem(m.Key, m.Count()))));

        public static IEnumerable<UrlData<HourlyStatisticItem>> CalculateHourlyStatistics(this IEnumerable<LogItem> log) =>
            log.GroupBy(l => l.Url)
            .OrderBy(l => l.Key)
            .Select(u => new UrlData<HourlyStatisticItem>(u.Key,
                u.GroupBy(m => m.DownloadTimestamp.ToString("HH:00"))
                    .OrderBy(m => m.Key)
                    .Select(m => new HourlyStatisticItem(m.Key, ((float)m.Count()) / u.Count()))));

        public static IEnumerable<(string Photographer, int Downloads)> CalculatePhotographerStatistics(
            this IEnumerable<LogItem> log, IEnumerable<Photographer> photographers) =>
            log.GroupBy(l => l.Url)
            .Select(l => (Photographer: photographers.First(p => p.Url == l.Key).TakenBy, Downloads: l.Count()))
            .OrderByDescending(l => l.Downloads);
    }
}
