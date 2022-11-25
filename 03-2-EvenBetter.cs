using InputHandler; // wow I am am immensely better programmer than this time last year - me, 25NOV2022

Console.WriteLine("Input:");
var readings = Input.ListUntilWhiteSpace();
var digits = readings.First().Length;

int FilterReadings(IEnumerable<string> readings, Predicate<int> condition, string message)
{
    for (var i = 0; i < digits; i++)
    {
        var oneCount = readings.Count(x => x[i] == '1');
        var useOne = oneCount - (readings.Count() - oneCount);

        readings = readings.Where(line => (line[i] == '1') == condition(useOne)).ToList();
        Console.WriteLine(readings.Count());

        if (readings.Count() == 1)
        {
            var b = Convert.ToInt32(readings.First(), 2);

            Console.WriteLine($"{message}: {b} with {readings.First()}");
            return b;
        }
    }

    return 0;
}

var oxygenMeter = FilterReadings(readings, useOne => useOne >= 0, "Oxygen");
var co2Meter = FilterReadings(readings, useOne => useOne < 0, "CO2");

Console.WriteLine($"Key: {oxygenMeter * co2Meter}");