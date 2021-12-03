/*int count = 0;
long test = 111111111111;// 111111111111;
int length = test.ToString().Length;
int[] oneCounts = new int[length];

while (true)
{
    string s = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(s)) break;

    for (int i = 0; i < length; i++)
    {
        oneCounts[i] += s[length - i - 1] == '1' ? 1 : 0;
    }

    count++;
}

foreach (int x in oneCounts) Console.WriteLine(x);

long gamma = 0;
long mask = 0;

for (int i = 0; i < length; i++)
{
    gamma += Convert.ToInt32(oneCounts[i] > (count - oneCounts[i])) << i;
    mask += 1 << i;
}

long epsilon = ~gamma & mask;

Console.WriteLine("Answer:");
Console.WriteLine($"{gamma}: {Convert.ToString(gamma, 2)}");
Console.WriteLine($"{epsilon}: {Convert.ToString(epsilon, 2)}");
Console.WriteLine(gamma * epsilon);
*/