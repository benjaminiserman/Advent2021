/*
List<int[]> lines = new();

while (true)
{
    string s = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(s)) break;
    lines.Add((from string q in s.Split(new char[] { ' ', ',', '-', '>' }, StringSplitOptions.RemoveEmptyEntries) select int.Parse(q)).ToArray());
}

Console.WriteLine("got:");

foreach (var line in lines)
{
    foreach (var x in line)
    {
        Console.Write($"{x} ");
    }
    Console.WriteLine();
}

List<int[]> useLines = (from line in lines where line[0] == line[2] || line[1] == line[3] select line).ToList();
int max = 0;
foreach (var line in useLines)
{
    int guess = line.Max();
    if (guess > max) max = guess;
}
max++;

int[][] map = new int[max][];
for (int x = 0; x < max; x++) map[x] = new int[max];

foreach (var line in useLines)
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
    else
    {
        int less = line[0] < line[2] ? line[0] : line[2];
        int great = line[0] >= line[2] ? line[0] : line[2];
        for (int i = less; i <= great; i++)
        {
            map[i][line[1]]++;
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
*/