using LogAnalysis.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

var logContent = await File.ReadAllLinesAsync("access-log.txt");
var log = logContent.ToLogItems(args.Length > 1 ? args[1] : null);

var photographersText = await File.ReadAllTextAsync("photographers.json");
var photographers = JsonSerializer.Deserialize<IEnumerable<Photographer>>(photographersText);
if (photographers == null)
{
    throw new InvalidOperationException("Couldn't parse photographers.json");
}

switch (args[0])
{
    case "monthly":
        var monthlyStats = log.CalculateMonthlyStatistics();
        foreach (var url in monthlyStats)
        {
            Console.WriteLine($"{url.Url}:");
            foreach (var month in url.Data)
            {
                Console.WriteLine($"\t{month.YearMonth}: {month.Downloads}");
            }

            Console.WriteLine($"\tTOTAL: {url.Data.Sum(m => m.Downloads)}");
        }
        break;

    case "hourly":
        var hourlyStats = log.CalculateHourlyStatistics();
        foreach (var url in hourlyStats)
        {
            Console.WriteLine($"{url.Url}:");
            foreach (var hour in url.Data)
            {
                Console.WriteLine($"\t{hour.Hour}: {hour.Percentage:P}");
            }
        }
        break;

    case "photographers":
        var photographerStats = log.CalculatePhotographerStatistics(photographers);
        foreach (var photographer in photographerStats)
        {
            Console.WriteLine($"{photographer.Photographer}: {photographer.Downloads}");
        }
        break;

    default:
        throw new ArgumentException("Invalid parameter");
}

