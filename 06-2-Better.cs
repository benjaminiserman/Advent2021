List<int> fish = (from x in Console.ReadLine().Split(',') select int.Parse(x)).ToList();
long[] array = new long[9];

foreach (int f in fish) array[f]++;

for (int i = 0; i < 256; i++)
{
    long carry = array[0];

    for (int j = 0; j < 8; j++)
    {
        array[j] = array[j + 1];
    }

    array[8] = 0;

    array[6] += carry;
    array[8] += carry;
}

long sum = 0;
foreach (long value in array)
{
    sum += value;
}

Console.WriteLine(sum);