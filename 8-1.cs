/*
int count = 0;

while (true)
{
    string s = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(s)) break;
    string[] split = s.Split('|');
    string[] two = split[1].Split();

    count += two.Count(x => x.Length is 2 or 4 or 3 or 7);
}

Console.WriteLine(count);
*/