/*
List<int[]> map = new();

while (true)
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

int sum = 0;
for (int i = 0; i < map.Count; i++)
{
    for (int j = 0; j < map[i].Length; j++)
    {
        if (i != 0 && map[i - 1][j] <= map[i][j]) continue;
        if (i != map.Count - 1 && map[i + 1][j] <= map[i][j]) continue;

        if (j != 0 && map[i][j - 1] <= map[i][j]) continue;
        if (j != map[i].Length - 1 && map[i][j + 1] <= map[i][j]) continue;

        sum += map[i][j] + 1;
    }
}

Console.WriteLine(sum);
*/