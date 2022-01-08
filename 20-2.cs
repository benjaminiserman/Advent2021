// this solution is really really bad
using InputHandler;

string algorithm = Console.ReadLine();

Console.ReadLine();

List<bool[]> array = Input.ListUntilEmpty(Console.ReadLine, s => (from c in s select c == '#').ToArray());

int step = 50;
int stepPad = step + 201;

List<bool[]> a = new();
List<bool[]> b = new();

for (int i = 0; i < array.Count + stepPad * 2; i++)
{
    a.Add(new bool[array[0].Length + stepPad * 2]);
    b.Add(new bool[array[0].Length + stepPad * 2]);
}

for (int i = stepPad; i < array.Count + stepPad; i++)
{
    for (int j = stepPad; j < array[0].Length + stepPad; j++)
    {
        a[i][j] = array[i - stepPad][j - stepPad];
    }
}

for (int i = 0; i < step; i++)
{
    if (i % 2 == 0) ModifyArray(a, b, i);
    else ModifyArray(b, a, i);
}

Console.WriteLine(Print(a));

void ModifyArray(List<bool[]> a, List<bool[]> b, int s)
{
    for (int i = 0; i < array.Count + stepPad * 2; i++)
    {
        for (int j = 0; j < array.Count + stepPad * 2; j++)
        {
            b[i][j] = NewState(a, i, j, s);
        }
    } 
}

bool NewState(List<bool[]> a, int i, int j, int s)
{
    int binary = 0;

    for (int di = -1; di <= 1; di++)
    {
        for (int dj = -1; dj <= 1; dj++)
        {
            binary <<= 1;
            binary += GetState(a, i + di, j + dj, s) ? 1 : 0;
        }
    }

    return algorithm[binary] == '#';
}

bool GetState(List<bool[]> array, int i, int j, int s)
{
    if (i < 0 || j < 0 || i >= array.Count || j >= array[i].Length) return false;
    else return array[i][j];
}

int Print(List<bool[]> a)
{
    int count = 0;

    for (int i = stepPad - step; i < a.Count - stepPad + step; i++)
    {
        for (int j = stepPad - step; j < a[0].Length - stepPad + step; j++)
        {
            Console.Write(a[i][j] ? '#' : '.');
            count += a[i][j] ? 1 : 0;
        }

        Console.WriteLine();
    }

    Console.WriteLine();
    return count;
}