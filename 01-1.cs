﻿List<int> depths = new();

while (true)
{
    string s = Console.ReadLine();
    if (string.IsNullOrEmpty(s)) break;
    depths.Add(int.Parse(s));
}

int count = 0;
for (int i = 0; i < depths.Count - 1; i++)
{
    if (depths[i + 1] > depths[i]) count++;
}

Console.WriteLine($"Solution is: {count}");