/*
List<(int, int)> points = new();

while (true)
{
    string s = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(s)) break;
    string[] split = s.Split(',');
    (int x, int y) t = (int.Parse(split[0]), int.Parse(split[1]));
    points.Add(t);
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
}

int xMax = 0;
int yMax = 0;
foreach ((int x, int y) in points)
{
    if (x > xMax) xMax = x;
    if (y > yMax) yMax = y;
}

for (int i = 0; i <= yMax; i++)
{
    for (int j = 0; j <= xMax; j++)
    {
        if (points.Contains((j, i)))
        {
            Console.Write('#');
        }
        else Console.Write(' ');
    }

    Console.WriteLine();
}
*/