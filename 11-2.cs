List<int[]> array = new();
List<bool[]> marks = new();

while (true)
{
    string s = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(s)) break;
    array.Add((from char c in s select c - '0').ToArray());
    marks.Add(new bool[s.Length]);
}

long flashes = 0;

for (int x = 0;; x++)
{
    int count = 0;
    for (int i = 0; i < array.Count; i++)
    {
        for (int j = 0; j < array[i].Length; j++)
        {
            array[i][j]++;
        }
    }

    do
    {
        for (int i = 0; i < array.Count; i++)
        {
            for (int j = 0; j < array[i].Length; j++)
            {
                if (array[i][j] > 9 && !marks[i][j])
                {
                    marks[i][j] = true;
                    flashes++;
                    count++;
                    for (int di = -1; di <= +1; di++)
                    {
                        for (int dj = -1; dj <= +1; dj++)
                        {
                            if (di != 0 || dj != 0) Mark(i + di, j + dj);
                        }
                    }
                }
            }
        }
    }
    while (!Done());

    if (count == array.Count * array[0].Length)
    {
        Console.WriteLine(x + 1);
        return;
    }

    for (int i = 0; i < array.Count; i++)
    {
        for (int j = 0; j < array[i].Length; j++)
        {
            if (marks[i][j]) array[i][j] = 0;

            marks[i][j] = false;
        }
    }
}

bool Done()
{
    for (int i = 0; i < array.Count; i++)
    {
        for (int j = 0; j < array[i].Length; j++)
        {
            if (array[i][j] > 9 && !marks[i][j]) return false;
        }
    }

    return true;
}

void Mark(int i, int j)
{
    if (i < 0 || j < 0 || i >= array.Count || j >= array[0].Length) return;

    array[i][j]++;
}