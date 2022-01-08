// the solutions for today are bad and grossly overfit.
string input = Console.ReadLine();

string[] split = (from x in input["target area: ".Length..].Split(new char[] { '=', '.', ',' }, StringSplitOptions.RemoveEmptyEntries) select x).ToArray();

List<int> targetArea = new();
foreach (string s in split)
{
    if (int.TryParse(s, out int x)) targetArea.Add(x);
}

int sx = targetArea[0];
int ex = targetArea[1];
int sy = targetArea[2];
int ey = targetArea[3];

Console.WriteLine($"{sx} {ex} {sy} {ey}");

List<int> heights = new();

for (int i = 1; i <= ex; i++)
{
    for (int j = sy; j < 5000; j++)
    {
        (bool b, int hy) = Probe(i, j);
        if (b) heights.Add(hy);
    }
}

Console.WriteLine(heights.Count);

(bool, int) Probe(int vx, int vy)
{
    int ivx = vx, ivy = vy;
    int x = 0, y = 0;

    while (x <= ex && y >= sy)
    {
        x += vx;
        y += vy--;

        if (vx < 0) vx++;
        else if (vx > 0) vx--;

        if (vx == 0 && ((x < sx) || (x > ex)))
        {
            break;
        }

        if (sx <= x && x <= ex && sy <= y && y <= ey)
        {
            return (true, 0);
        }
    }

    return (false, int.MinValue);
}