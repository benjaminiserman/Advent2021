List<int[]> lines = new();

while (true)
{
    string s = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(s)) break;
    lines.Add((from string q in s.Split(new char[] { ' ', ',', '-', '>' }, StringSplitOptions.RemoveEmptyEntries) select int.Parse(q)).ToArray());
}

int max = 0;
foreach (var line in lines)
{
    int guess = line.Max();
    if (guess > max) max = guess;
}
max++;

int[][] map = new int[max][];
for (int x = 0; x < max; x++) map[x] = new int[max];

foreach (var line in lines)
{
    if (line[0] == line[2])
    {
        int less = line[1] < line[3] ? line[1] : line[3];
        int great = line[1] >= line[3] ? line[1] : line[3];
        for (int i = less; i <= great; i++)
        {
            map[line[0]][i]++;
        }
    }
    else if (line[1] == line[3])
    {
        int less = line[0] < line[2] ? line[0] : line[2];
        int great = line[0] >= line[2] ? line[0] : line[2];
        for (int i = less; i <= great; i++)
        {
            map[i][line[1]]++;
        }
    }
    else
    {
        int dx = line[2] > line[0] ? 1 : -1;
        int dy = line[3] > line[1] ? 1 : -1;

        for (int x = line[0], y = line[1]; x != line[2] + dx; x += dx, y += dy)
        {
            map[x][y]++;
        }
    }
}

int count = 0;
for (int y = 0; y < max; y++)
{
    for (int x = 0; x < max; x++)
    {
        if (map[x][y] > 1) count++;
        //Console.Write($"{map[x][y]} ");
    }
    //Console.WriteLine();
}

Console.WriteLine($"Key: {count}");