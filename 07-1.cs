int[] positions = (from string s in Console.ReadLine().Split(',') select int.Parse(s)).ToArray();

int minFuel = int.MaxValue;

for (int i = 0; i < positions.Length; i++)
{
    int fuel = 0;
    for (int j = 0; j < positions.Length; j++)
    {
        fuel += Math.Abs(i - positions[j]);
    }

    if (fuel < minFuel) minFuel = fuel;
}

Console.WriteLine(minFuel);