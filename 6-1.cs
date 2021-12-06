/*
List<int> fish = (from x in Console.ReadLine().Split(',') select int.Parse(x)).ToList();

for (int i = 0; i < 80; i++)
{
    int count = fish.Count;
    for (int j = 0; j < count; j++)
    {
        fish[j]--;
        if (fish[j] == -1)
        {
            fish[j] = 6;
            fish.Add(8);
        }
    }
}

Console.WriteLine(fish.Count);
*/