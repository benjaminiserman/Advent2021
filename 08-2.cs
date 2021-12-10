/*
int count = 0;

while (true)
{
    string s = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(s)) break;
    string[] split = s.Split('|', StringSplitOptions.RemoveEmptyEntries);
    string[] one = split[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    string[] two = split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

    List<string>[] lengths = new List<string>[10];
    for (int i = 0; i < 10; i++) lengths[i] = new();
    foreach (string s2 in one)
    {
        lengths[s2.Length].Add(s2);
    }

    string[] map = new string[10];
    map[1] = lengths[2][0];
    map[4] = lengths[4][0];
    map[7] = lengths[3][0];
    map[8] = lengths[7][0];

    map[6] = lengths[6].First(s => s.Count(x => map[1].Contains(x)) == 1);
    map[5] = lengths[5].First(s => s.Count(x => map[6].Contains(x)) == 5);
    map[3] = lengths[5].First(s => s.Count(x => map[1].Contains(x)) == 2);
    map[2] = lengths[5].First(s => s != map[5] && s != map[3]);
    map[9] = lengths[6].First(s => s.Count(x => map[4].Contains(x)) == 4);
    map[0] = lengths[6].First(s => s != map[9] && s != map[6]);

    string final = string.Empty;

    foreach (string s2 in two)
    {
        for (int i = 0; i < 10; i++)
        {
            if (map[i].Length == s2.Length && s2.Count(x => map[i].Contains(x)) == s2.Length)
            {
                final += i.ToString();
                break;
            }
        }
    }

    count += int.Parse(final);
    Console.WriteLine(final);
}

Console.WriteLine(count);
*/