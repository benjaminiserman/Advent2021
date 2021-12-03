/* 
// Editor's note: there's a better solution here by using a sliding window but I didn't do that
List<int> depths = new();

while (true)
{
    string s = Console.ReadLine();
    if (string.IsNullOrEmpty(s)) break;
    depths.Add(int.Parse(s));
}

int count = 0;
int prevSum = int.MaxValue, sum;
for (int i = 0; i < depths.Count - 2; i++)
{
    sum = depths[i] + depths[i + 1] + depths[i + 2];
    if (sum > prevSum) count++;
    prevSum = sum;
}

Console.WriteLine($"Solution is: {count}");
*/