using System;
using System.Collections.Generic;
using System.Linq;

var lines = ReadAllLinesFromConsole()
    .Select(line => line.Split('\t'))
    .ToList();

var groupingColumnName = args[0];
var numericColumnName = args[1];
var maxNumberOfResultRow = Int32.Parse(args[2]);

var groupingColumnIndex = Array.IndexOf(lines.First(), groupingColumnName);
var numericColumnIndex = Array.IndexOf(lines.First(), numericColumnName);

var result = lines.Skip(1)
    .GroupBy(line => line[groupingColumnIndex])
    .Select(group => new
    {
        GroupKey = group.Key,
        Sum = group.Sum(item => Int32.Parse(item[numericColumnIndex]))
    })
    .OrderByDescending(line => line.Sum)
    .Take(maxNumberOfResultRow)
    .ToList();

var maximumNumericValue = result.Max(item => item.Sum);

foreach (var item in result)
{
    Console.WriteLine($"{item.GroupKey}: {item.Sum / maximumNumericValue * 100}");
}


static IEnumerable<string> ReadAllLinesFromConsole()
{
    while (true)
    {
        var line = Console.ReadLine();
        if (string.IsNullOrEmpty(line)) break;
        yield return line.EndsWith("\r\n") ? line[..^2] : line;
    }
}


