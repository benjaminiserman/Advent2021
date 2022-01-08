List<(int, int)> points = new();

int max = 0;
while (true)
{
    string s = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(s)) break;
    string[] split = s.Split(',');
    (int x, int y) t = (int.Parse(split[0]), int.Parse(split[1]));
    points.Add(t);

    if (t.x > max) max = t.x;
    if (t.y > max) max = t.y;
}

while (true)
{
    string s = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(s)) break;

    char dir = s["fold along ".Length];
    int p = int.Parse(s["fold along ?=".Length..]);

    if (dir == 'x')
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (points[i].Item1 > p) points[i] = (p - (points[i].Item1 - p), points[i].Item2);
        }
    }
    else
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (points[i].Item2 > p) points[i] = (points[i].Item1, p - (points[i].Item2 - p));
        }
    }

    break;
}

int count = 0;
for (int i = 0; i < max; i++)
{
    for (int j = 0; j < max; j++)
    {
        if (points.Contains((j, i)))
        {
            count++;
        }
    }
}

Console.Write(count);