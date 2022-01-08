List<int[]> startingMap = new();

while (true)
{
    string s = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(s)) break;
    startingMap.Add((from x in s select int.Parse(x.ToString())).ToArray());
}

int[][] map = new int[startingMap.Count * 5][];
bool[][] marks = new bool[startingMap.Count * 5][];
int[][] score = new int[startingMap.Count * 5][];

for (int i = 0; i < map.Length; i++)
{
    map[i] = new int[startingMap[0].Length * 5];
    marks[i] = new bool[startingMap[0].Length * 5];
    score[i] = new int[startingMap[0].Length * 5];
    for (int j = 0; j < map[0].Length; j++) score[i][j] = int.MaxValue;
}

for (int x = 0; x < 5; x++)
{
    for (int y = 0; y < 5; y++)
    {
        for (int i = 0; i < startingMap.Count; i++)
        {
            for (int j = 0; j < startingMap[0].Length; j++)
            {
                int newRisk = startingMap[i][j] + x + y;
                newRisk %= 9;
                if (newRisk == 0) newRisk = 9;
                map[x * startingMap.Count + i][y * startingMap[0].Length + j] = newRisk;
            }
        }
    }
}

int risk = 0;

PriorityQueue<(int, int), int> queue = new();

queue.Enqueue((0, 0), 0);

while (queue.TryDequeue(out (int x, int y) t, out int p))
{
    marks[t.x][t.y] = true;
    if (t.x == map.Length - 1 && t.y == map[0].Length - 1)
    {
        risk = p;
        break;
    }

    if (t.x != 0 && !marks[t.x - 1][t.y])
    {
        if (p + map[t.x - 1][t.y] < score[t.x - 1][t.y])
        {
            queue.Enqueue((t.x - 1, t.y), p + map[t.x - 1][t.y]);
            score[t.x - 1][t.y] = p + map[t.x - 1][t.y];
        }
    }

    if (t.x != map.Length - 1 && !marks[t.x + 1][t.y])
    {
        if (p + map[t.x + 1][t.y] < score[t.x + 1][t.y])
        {
            queue.Enqueue((t.x + 1, t.y), p + map[t.x + 1][t.y]);
            score[t.x + 1][t.y] = p + map[t.x + 1][t.y];
        }
    }

    if (t.y != 0 && !marks[t.x][t.y - 1])
    {
        if (p + map[t.x][t.y - 1] < score[t.x][t.y - 1])
        {
            queue.Enqueue((t.x, t.y - 1), p + map[t.x][t.y - 1]);
            score[t.x][t.y - 1] = p + map[t.x][t.y - 1];
        }
    }

    if (t.y != map[0].Length - 1 && !marks[t.x][t.y + 1])
    {
        if (p + map[t.x][t.y + 1] < score[t.x][t.y + 1])
        {
            queue.Enqueue((t.x, t.y + 1), p + map[t.x][t.y + 1]);
            score[t.x][t.y + 1] = p + map[t.x][t.y + 1];
        }
    }
}

Console.WriteLine(risk);