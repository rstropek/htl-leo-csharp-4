using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

var fileContent = await File.ReadAllTextAsync("data.json");
var cars = JsonSerializer.Deserialize<CarData[]>(fileContent);

// Print all cars with at least 4 doors
//var carsWithAtLeastFourDoors = cars.Where(car => car.NumberOfDoors >= 4);
//foreach(var car in carsWithAtLeastFourDoors)
//{
//    Console.WriteLine($"The car {car.Model} has {car.NumberOfDoors} doors");
//}

// Print all Mazda cars with at least 4 doors
//var mazdasWithAtLeastFourDoors = cars.Where(car => car.Make == "Mazda" && car.NumberOfDoors >= 4);
//mazdasWithAtLeastFourDoors = cars.Where(car => car.Make == "Mazda").Where(car => car.NumberOfDoors >= 4);
//foreach (var car in mazdasWithAtLeastFourDoors)
//{
//    Console.WriteLine($"The Mazda car {car.Model} has {car.NumberOfDoors} doors");
//}

// Print Make + Model for all Makes that start with "M"
//cars.Where(car => car.Make.StartsWith("M"))
//    .Select(car => $"{car.Make} {car.Model}")
//    .ToList()
//    .ForEach(car => Console.WriteLine(car));

// Display a list of the 10 most powerful cars (in terms of hp)
//cars.OrderByDescending(car => car.HP)
//    .Take(10)
//    .Select(car => $"{car.Make} {car.Model}")
//    .ToList()
//    .ForEach(car => Console.WriteLine(car));

// Display the number of models per make that appeared after 2008.
// Makes should be displayed with a number of zero if there are no models after 2008.
//cars.GroupBy(car => car.Make)
//    .Select(c => new { c.Key, 
//        NumberOfModels = c.Count(car => car.Year >= 2008) })
//    .ToList()
//    .ForEach(item => Console.WriteLine($"{item.Key}: {item.NumberOfModels}"));

// Display a list of makes that have at least 2 models with >= 400hp
//cars.Where(car => car.HP >= 400)
//    .GroupBy(car => car.Make)
//    .Select(car => new { Make = car.Key, NumberOfPowerfulCars = car.Count() })
//    .Where(make => make.NumberOfPowerfulCars >= 2)
//    .ToList()
//    .ForEach(make => Console.WriteLine(make.Make));

// Display there average hp per make
//cars.GroupBy(car => car.Make)
//    .Select(car => new { Make = car.Key, AverageHP = car.Average(c => c.HP) })
//    .ToList()
//    .ForEach(make => Console.WriteLine($"{make.Make}: {make.AverageHP}"));

// How many makes build cars with hp between 0..100, 101..200, 201..300, 301..400, 401..500
cars.GroupBy(car => car.HP switch
    {
        <= 100 => "0..100",
        <= 200 => "101..200",
        <= 300 => "201..300",
        <= 400 => "301..400",
        _ => "401..500"
    })
    .Select(car => new { HPCategory = car.Key, 
        NumberOfMake = car.Select(c => c.Make).Distinct().Count() })
    .ToList()
    .ForEach(item => Console.WriteLine($"{item.HPCategory}: {item.NumberOfMake}"));

class CarData
{
    [JsonPropertyName("id")]
    public int ID { get; set; }

    [JsonPropertyName("car_make")]
    public string Make { get; set; }
    
    [JsonPropertyName("car_models")]
    public string Model { get; set; }

    [JsonPropertyName("car_year")]
    public int Year { get; set; }

    [JsonPropertyName("number_of_doors")]
    public int NumberOfDoors { get; set; }

    [JsonPropertyName("hp")]
    public int HP { get; set; }

}

