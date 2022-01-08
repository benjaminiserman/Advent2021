long test = 111111111111;
int digits = test.ToString().Length;
List<string> oxygen = new(), co2 = new();

int oxygenMeter = 0, co2Meter = 0;

while (true)
{
    string s = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(s)) break;

    oxygen.Add(s);
    co2.Add(s);
}

for (int i = 0; i < digits; i++)
{
    int oneCount = oxygen.Count(x => x[i] == '1');
    int useOne = oneCount - (oxygen.Count - oneCount);

    for (int j = 0; j < oxygen.Count; j++)
    {
        if ((oxygen[j][i] == '1') != (useOne >= 0))
        {
            oxygen.RemoveAt(j);
            j--;
        }
    }

    if (oxygen.Count == 1)
    {
        int b = 0;
        for (int j = 0; j < digits; j++)
        {
            b += oxygen[0][digits - j - 1] == '1' ? (1 << j) : 0;
        }

        oxygenMeter = b;
        Console.WriteLine($"Oxygen: {b} with {oxygen[0]}");
        break;
    }
}

for (int i = 0; i < digits; i++)
{
    int oneCount = co2.Count(x => x[i] == '1');
    int useOne = oneCount - (co2.Count - oneCount);

    for (int j = 0; j < co2.Count; j++)
    {
        if ((co2[j][i] == '1') != (useOne < 0))
        {
            co2.RemoveAt(j);
            j--;
        }
    }

    if (co2.Count == 1)
    {
        int b = 0;
        for (int j = 0; j < digits; j++)
        {
            b += co2[0][digits - j - 1] == '1' ? (1 << j) : 0;
        }

        co2Meter = b;
        Console.WriteLine($"CO2: {b} with {co2[0]}");
        break;
    }
}

Console.WriteLine($"Key: {oxygenMeter * co2Meter}");