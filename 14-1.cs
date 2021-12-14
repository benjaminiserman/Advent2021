/*
string polymer = Console.ReadLine();
Dictionary<string, char> pairs = new();

Console.ReadLine();

while (true)
{
    string s = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(s)) break;

    string[] split = s.Split();

    pairs.Add(split[0], split[2][0]);
}

string copy = polymer;

for (int x = 0; x < 10; x++)
{
    for (int i = 1; i < copy.Length; i++)
    {
        if (pairs.TryGetValue($"{copy[i - 1]}{copy[i]}", out char c))
        {
            copy = copy.Insert(i, c.ToString());
            i++;
        }
    }
}

Dictionary<char, int> counts = new();
foreach (char c in copy)
{
    if (counts.ContainsKey(c)) counts[c]++;
    else counts.Add(c, 1);
}

var sorted = (from x in counts.Values orderby x select x).ToArray();
Console.WriteLine(sorted[^1] - sorted[0]);
*/