/*
using System.Numerics;

List<int> fish = (from x in Console.ReadLine().Split(',') select int.Parse(x)).ToList();
Dictionary<int, BigInteger> dict = new(); // editor's note: this should definitely be an array

for (int i = 0; i <= 8; i++) dict.Add(i, 0);

foreach (int f in fish) dict[f]++;

for (int i = 0; i < 256; i++)
{
    BigInteger carry = dict[0];

    for (int j = 0; j < 8; j++)
    {
        dict[j] = dict[j + 1];
    }

    dict[8] = 0;

    dict[6] += carry;
    dict[8] += carry;
}

BigInteger sum = 0;
foreach (BigInteger value in dict.Values)
{
    sum += value;
}

Console.WriteLine(sum);
*/