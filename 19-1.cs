using InputHandler;

List<Scanner> scanners = new();

while (true)
{
    string s = Console.ReadLine();
    if (s.Length >= 3 && s[..3] == "---")
    {
        scanners.Add(new(scanners.Count, Input.ListUntilEmpty(Console.ReadLine, x =>
        {
            int[] split = (from y in x.Split(',') select int.Parse(y)).ToArray();
            return new Vector3(split[0], split[1], split[2]);
        })));
    }
    else break;
}

List<Scanner> solved = new() { scanners[0] };
HashSet<(int, int)> check = new();

while (scanners.Count != solved.Count)
{
    int solvedCount = solved.Count;
    for (int i = 0; i < solvedCount; i++)
    {
        for (int j = 0; j < scanners.Count; j++)
        {
            if (solved[i].id != scanners[j].id && !solved.Contains(scanners[j]) && !check.Contains((solved[i].id, scanners[j].id)))
            {
                check.Add((solved[i].id, scanners[j].id));

                int bestRot = -1, max = 0;

                HashSet<Vector3> bestPoints;

                for (int r = 0; r < 24; r++)
                {
                    HashSet<Vector3> points = new();

                    var overlap = from x in scanners[j].diffs where solved[i].diffs.ContainsValue(x.Value.Rot(r)) select x.Key;

                    foreach (var x in overlap)
                    {
                        points.Add(x.Item1);
                        points.Add(x.Item2);
                    }

                    if (points.Count > max)
                    {
                        max = points.Count;
                        bestRot = r;
                        bestPoints = points;
                    }
                }

                if (max >= 12)
                {
                    HashSet<(Vector3, Vector3)> set = new();

                    foreach (var kvp1 in scanners[j].diffs)
                    {
                        foreach (var kvp2 in solved[i].diffs)
                        {
                            if (kvp1.Value.Rot(bestRot) == kvp2.Value)
                            {
                                set.Add((kvp1.Key.Item1, kvp2.Key.Item1));
                                set.Add((kvp1.Key.Item2, kvp2.Key.Item2));
                            }    
                        }
                    }

                    Dictionary<Vector3, int> dict = new();
                    foreach (var v in set)
                    {
                        Vector3 diff = v.Item2 - v.Item1.Rot(bestRot);

                        if (dict.ContainsKey(diff)) dict[diff]++;
                        else dict.Add(diff, 1);
                    }

                    var sorted = from x in dict orderby x.Value descending select x.Key;
                    Vector3 finalDiff = sorted.First();

                    Console.WriteLine($"{solved[i].id} and {j} have {max} matches at {bestRot} and diff {sorted.First()} => {j} position: {finalDiff}");

                    for (int k = 0; k < scanners[j].pings.Count; k++)
                    {
                        scanners[j].pings[k] = scanners[j].pings[k].Rot(bestRot) + finalDiff;
                    }

                    scanners[j].position = finalDiff;
                    scanners[j].ConstructDiffs();
                    solved.Add(scanners[j]);

                }
            }
        }
    }
}

HashSet<Vector3> allPoints = new();

foreach (Scanner scanner in solved)
{
    foreach (Vector3 point in scanner.pings)
    {
        allPoints.Add(point);
    }
}

Console.WriteLine(allPoints.Count);

record Scanner
{
    public int id;
    public List<Vector3> pings;
    public Dictionary<(Vector3, Vector3), Vector3> diffs = new();
    public Vector3 position = new(0, 0, 0);

    public Scanner(int id, List<Vector3> pings)
    {
        this.id = id;
        this.pings = pings;
        ConstructDiffs();
    }

    public void ConstructDiffs()
    {
        diffs.Clear();

        for (int i = 0; i < pings.Count; i++)
        {
            for (int j = 0; j < pings.Count; j++)
            {
                if (i != j)
                {
                    diffs.Add((pings[i], pings[j]), pings[j] - pings[i]);
                }
            }
        }
    }
}

struct Vector3
{
    public int x, y, z;

    public Vector3(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3 Rot(int r) => r switch
    {
        0 => new Vector3(+x, +y, +z),
        1 => new Vector3(+x, -y, -z),
        2 => new Vector3(+x, +z, -y),
        3 => new Vector3(+x, -z, +y),
        4 => new Vector3(-x, +y, -z),
        5 => new Vector3(-x, -y, +z),
        6 => new Vector3(-x, +z, +y),
        7 => new Vector3(-x, -z, -y),
        8 => new Vector3(+y, +x, -z),
        9 => new Vector3(+y, -x, +z),
        10 => new Vector3(+y, +z, +x),
        11 => new Vector3(+y, -z, -x),
        12 => new Vector3(-y, +x, +z),
        13 => new Vector3(-y, -x, -z),
        14 => new Vector3(-y, +z, -x),
        15 => new Vector3(-y, -z, +x),
        16 => new Vector3(+z, +x, +y),
        17 => new Vector3(+z, -x, -y),
        18 => new Vector3(+z, +y, -x),
        19 => new Vector3(+z, -y, +x),
        20 => new Vector3(-z, +x, -y),
        21 => new Vector3(-z, -x, +y),
        22 => new Vector3(-z, +y, +x),
        23 => new Vector3(-z, -y, -x),
        _ => throw new Exception("impossible rotation")
    };

    public override string ToString() => $"({x}, {y}, {z})";

    public static Vector3 operator +(Vector3 a, Vector3 b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
    public static Vector3 operator -(Vector3 a, Vector3 b) => new(a.x - b.x, a.y - b.y, a.z - b.z);

    public static bool operator ==(Vector3 a, Vector3 b) => a.x == b.x && a.y == b.y && a.z == b.z;
    public static bool operator !=(Vector3 a, Vector3 b) => a.x != b.x || a.y != b.y || a.z != b.z;
}