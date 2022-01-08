using InputHandler;

List<Instruction> instructions = Input.ListUntilEmpty(Console.ReadLine, x => new Instruction(x));

int sx = instructions.MinBy(i => i.sx).sx;
int sy = instructions.MinBy(i => i.sy).sy;
int sz = instructions.MinBy(i => i.sz).sz;

int ex = instructions.MaxBy(i => i.ex).ex;
int ey = instructions.MaxBy(i => i.ey).ey;
int ez = instructions.MaxBy(i => i.ez).ez;

bool[][][] cube = new bool[ex - sx + 1][][];
for (int x = 0; x <= ex - sx; x++)
{
	cube[x] = new bool[ey - sy + 1][];
	for (int y = 0; y <= ey - sy; y++)
	{
		cube[x][y] = new bool[ez - sz + 1];
	}
}
		

foreach (var i in instructions)
{
	Console.WriteLine(i.sx);
	for (int x = i.sx; x <= i.ex; x++)
		for (int y = i.sy; y <= i.ey; y++)
			for (int z = i.sz; z <= i.ez; z++)
			{
				cube[x - sx][y - sy][z - sz] = i.on;
			}
}

long count = 0;

for (int x = 0; x <= ex - sx; x++)
	for (int y = 0; y <= ey - sy; y++)
		for (int z = 0; z <= ez - sz; z++)
		{
			if (cube[x][y][z]) count++;
		}

Console.WriteLine(count);

struct Instruction
{
	public int sx, ex, sy, ey, sz, ez;
	public bool on;

	public Instruction(string s)
	{
		on = s[..2] == "on";

		string[] split = s.Split(new char[] { '=', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);

		sx = int.Parse(split[1]);
		ex = int.Parse(split[2]);
		sy = int.Parse(split[4]);
		ey = int.Parse(split[5]);
		sz = int.Parse(split[7]);
		ez = int.Parse(split[8]);
	}
}