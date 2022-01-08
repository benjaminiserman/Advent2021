using InputHandler; // hehe I brought in my input library (check it out on my github!)
using System.Numerics;

string s = Console.ReadLine();

List<int> binary = new();

foreach (char c in s)
{
    binary.AddRange(c switch
    {
        '0' => new List<int>() { 0, 0, 0, 0 },
        '1' => new List<int>() { 0, 0, 0, 1 },
        '2' => new List<int>() { 0, 0, 1, 0 },
        '3' => new List<int>() { 0, 0, 1, 1 },
        '4' => new List<int>() { 0, 1, 0, 0 },
        '5' => new List<int>() { 0, 1, 0, 1 },
        '6' => new List<int>() { 0, 1, 1, 0 },
        '7' => new List<int>() { 0, 1, 1, 1 },
        '8' => new List<int>() { 1, 0, 0, 0 },
        '9' => new List<int>() { 1, 0, 0, 1 },
        'A' => new List<int>() { 1, 0, 1, 0 },
        'B' => new List<int>() { 1, 0, 1, 1 },
        'C' => new List<int>() { 1, 1, 0, 0 },
        'D' => new List<int>() { 1, 1, 0, 1 },
        'E' => new List<int>() { 1, 1, 1, 0 },
        'F' => new List<int>() { 1, 1, 1, 1 },
        _ => throw new InvalidOperationException()
    });
}

foreach (int x in binary) Console.Write($"{x} ");
Console.WriteLine();

int sum = 0;

for (int i = 0; i < binary.Count;)
{
    try
    {
        sum += GrabX((i += 3) - 3, 3);
        int type = GrabX((i += 3) - 3, 3);

        switch (type)
        {
            case 4:
                int val = 0;
                while (true)
                {
                    int grab = GrabX((i += 5) - 5, 5);
                    val <<= 4;
                    val += grab & 0b1111;
                    if ((grab & 0b10000) == 0) break;
                }

                break;
            default:
                if (binary[i++] == 1)
                {
                    int subCount = GrabX((i += 11) - 11, 11);
                }
                else
                {
                    int bitLength = GrabX((i += 15) - 15, 15);
                }

                break;
        }
    }
    catch
    {
        break;
    }
}

Console.WriteLine(sum);

int GrabX(int i, int x)
{
    int v = 0;
    for (int j = 0; j < x; j++)
    {
        v += binary[i + j] << x - j - 1;
    }

    return v;
}