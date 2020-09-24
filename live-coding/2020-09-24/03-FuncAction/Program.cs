using System;
using System.Diagnostics;

var watch = Stopwatch.StartNew();
CountToNearlyInfinity(); // <<<< Method to benchmark
watch.Stop();
Console.WriteLine(watch.Elapsed);

MeasureTime(() => CountToNearlyInfinity());

Console.WriteLine($"The result is {MeasureTimeFunc(() => CalculateSomeResult())}");

static void MeasureTime(Action a)
{
    var watch = Stopwatch.StartNew();
    a();
    watch.Stop();
    Console.WriteLine(watch.Elapsed);
}

static int MeasureTimeFunc(Func<int> f)
{
    var watch = Stopwatch.StartNew();
    var result = f();
    watch.Stop();
    Console.WriteLine(watch.Elapsed);
    return result;
}

static void CountToNearlyInfinity()
{
    for (var i = 0; i < 1000000000; i++) ;
}

static int CalculateSomeResult()
{
    // Simulate some interesting calculation
    for (var i = 0; i < 1000000000; i++) ;

    // Return the result
    return 42;
}
