/*
List<int[]> map = new();

while (true) // get inputs
{
    string s = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(s)) break;
    int[] array = new int[s.Length];

    for (int i = 0; i < array.Length; i++)
    {
        array[i] = int.Parse(s[i].ToString());
    }

    map.Add(array);
}

Dictionary<(int, int), int> basins = new();

for (int i = 0; i < map.Count; i++) // find basins
{
    for (int j = 0; j < map[i].Length; j++)
    {
        if (i != 0 && map[i - 1][j] <= map[i][j]) continue;
        if (i != map.Count - 1 && map[i + 1][j] <= map[i][j]) continue;

        if (j != 0 && map[i][j - 1] <= map[i][j]) continue;
        if (j != map[i].Length - 1 && map[i][j + 1] <= map[i][j]) continue;

        basins.Add((i, j), 0);
    }
}

for (int i = 0; i < map.Count; i++) // map basins for all points that != 9
{
    for (int j = 0; j < map[i].Length; j++)
    {
        if (map[i][j] != 9) basins[Basin(i, j)]++;
    }
}

(int, int) Basin(int i, int j) // recursively move to lowest adjacent point
{
    List<(int, int, int)> moves = new();

    if (i != 0 && map[i - 1][j] < map[i][j]) 
        moves.Add((i - 1, j, map[i - 1][j]));

    if (i != map.Count - 1 && map[i + 1][j] < map[i][j]) 
        moves.Add((i + 1, j, map[i + 1][j]));

    if (j != 0 && map[i][j - 1] < map[i][j]) 
        moves.Add((i, j - 1, map[i][j - 1]));

    if (j != map[i].Length - 1 && map[i][j + 1] < map[i][j]) 
        moves.Add((i, j + 1, map[i][j + 1]));

    if (moves.Count == 0) return (i, j);
    else
    {
        (int x, int y, _) = moves.MinBy(x => x.Item3);
        return Basin(x, y);
    }
}

var sorted = (from int x in basins.Values orderby -x select x).ToArray();

Console.WriteLine(sorted[0] * sorted[1] * sorted[2]);
*/