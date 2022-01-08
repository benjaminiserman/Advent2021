List<int[]> map = new();
List<bool[]> marks = new();

while (true)
{
    string s = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(s)) break;
    map.Add((from x in s select int.Parse(x.ToString())).ToArray());
    marks.Add(new bool[s.Length]);
}

int risk = 0;

PriorityQueue<(int, int), int> queue = new PriorityQueue<(int, int), int>();

queue.Enqueue((0, 0), 0);

while (queue.TryDequeue(out (int x, int y) t, out int p))
{
    marks[t.x][t.y] = true;
    if (t.x == map.Count - 1 && t.y == map[0].Length - 1)
    {
        risk = p;
        break;
    }

    if (t.x != 0 && !marks[t.x - 1][t.y]) queue.Enqueue((t.x - 1, t.y), p + map[t.x - 1][t.y]);
    if (t.x != map.Count - 1 && !marks[t.x + 1][t.y]) queue.Enqueue((t.x + 1, t.y), p + map[t.x + 1][t.y]);
    if (t.y != 0 && !marks[t.x][t.y - 1]) queue.Enqueue((t.x, t.y - 1), p + map[t.x][t.y - 1]);
    if (t.y != map[0].Length - 1 && !marks[t.x][t.y + 1]) queue.Enqueue((t.x, t.y + 1), p + map[t.x][t.y + 1]);
}

Console.WriteLine(risk);