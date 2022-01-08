int[] positions = (from string s in Console.ReadLine().Split(',') select int.Parse(s)).ToArray();

long minFuel = long.MaxValue;

int max = positions.Max();
for (int i = 0; i < max; i++)
{
    long fuel = 0;
    for (int j = 0; j < positions.Length; j++)
    {
        int x = Math.Abs(i - positions[j]);

        int f = 0; // editors note: this variable was added for debugging purposes but not removed from the end-program.
        for (int k = 1; k <= x; k++)
        {
            f += k;
        }

        fuel += f;
    }

    if (fuel < minFuel) minFuel = fuel;
}

Console.WriteLine(minFuel);