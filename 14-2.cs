/*
using System.Numerics;

string polymer = Console.ReadLine();
Dictionary<string, char> pairs = new();
Dictionary<string, long> counts = new();
Dictionary<char, long> letterCounts = new();

Console.ReadLine();

while (true)
{
    string s = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(s)) break;

    string[] split = s.Split();

    pairs.Add(split[0], split[2][0]);
    counts.Add(split[0], 0);
    if (!letterCounts.ContainsKey(split[0][0])) letterCounts.Add(split[0][0], 0);
}

string copy = polymer;

for (int i = 1; i < copy.Length; i++)
{
    counts[$"{copy[i - 1]}{copy[i]}"]++;
}

for (int i = 0; i < 40; i++)
{
    Dictionary<string, long> dCounts = new();
    foreach (var kvp in counts) dCounts.Add(kvp.Key, 0);

    foreach (var kvp in counts)
    {
        string pair = kvp.Key;
        char c = pairs[kvp.Key];

        dCounts[$"{pair[0]}{c}"] += kvp.Value;
        dCounts[$"{c}{pair[1]}"] += kvp.Value;
    }

    counts = dCounts;
}

foreach (var pair in counts)
{
    letterCounts[pair.Key[0]] += pair.Value;
    letterCounts[pair.Key[1]] += pair.Value;
}

letterCounts[polymer[0]]++;
letterCounts[polymer[^1]]++;

var sorted = (from x in letterCounts.Values orderby x select x).ToArray();
Console.WriteLine((sorted[^1] - sorted[0]) / 2);
*/