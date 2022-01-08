int count = 0;
long test = 111111111111;// 111111111111;
int length = test.ToString().Length;
List<string> oxygen = new(), co2 = new();

int fO = 0, fC = 0;

while (true)
{
    string s = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(s)) break;

    oxygen.Add(s);
    co2.Add(s);

    count++;
}

for (int i = 0; i < length; i++)
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
        for (int j = 0; j < length; j++)
        {
            b += oxygen[0][length - j - 1] == '1' ? (1 << j) : 0;
        }
        fO = b;
        Console.WriteLine($"Oxygen: {b} with {oxygen[0]}");
        break;
    }
}

for (int i = 0; i < length; i++)
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
        for (int j = 0; j < length; j++)
        {
            b += co2[0][length - j - 1] == '1' ? (1 << j) : 0;
        }
        fC = b;
        Console.WriteLine($"CO2: {b} with {co2[0]}");
        break;
    }
}

Console.WriteLine($"Key: {fO * fC}");