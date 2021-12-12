/*
List<(string, string)> connections = new();

while (true)
{
    string s = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(s)) break;
    string[] vs = s.Split('-');
    connections.Add((vs[0], vs[1]));
}

Console.WriteLine(Traverse(connections, "start", "start"));

int Traverse(List<(string, string)> connections, string path, string current)
{
    var found = from x in connections where x.Item1 == current || x.Item2 == current select x;
    int count = 0;

    foreach (var c in found)
    {
        string next = c.Item1 == current ? c.Item2 : c.Item1;

        if (next == "start") continue;

        if (next == "end")
        {
            //Console.WriteLine($"{path}-end");
            count++;
        }
        else if (!path.Contains($"{current}-{next}"))
        {
            if (!char.IsLower(next[0]) || !path.Contains($"-{next}-")) count += Traverse(connections, $"{path}-{next}", next);
        }
    }

    return count;
}
*/