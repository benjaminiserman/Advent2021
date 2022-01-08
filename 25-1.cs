using InputHandler;

List<List<Cucumber>> list = Input.ListUntilEmpty(Console.ReadLine, s => (from c in s select Map(c)).ToList());


List<List<Cucumber>> next = new(list.Count);
for (int i = 0; i < list.Count; i++) next.Add(new(list[i]));

int step;
bool moved = true;
for (step = 0; moved; step++)
{
	Console.WriteLine(step);
	moved = false;
	for (int i = 0; i < list.Count; i++)
	{
		for (int j = 0; j < list[0].Count; j++)
		{
			if (list[i][j] == Cucumber.East)
			{
				int nj = WrapJ(j + 1);

				if (list[i][nj] == Cucumber.None)
				{
					next[i][nj] = Cucumber.East;
					next[i][j] = Cucumber.None;
					moved = true;
				}
			}
		}
	}

	list = next;
	next = new(list.Count);
	for (int i = 0; i < list.Count; i++) next.Add(new(list[i]));

	for (int i = 0; i < list.Count; i++)
	{
		for (int j = 0; j < list[0].Count; j++)
		{
			if (list[i][j] == Cucumber.South)
			{
				int ni = WrapI(i + 1);

				if (list[ni][j] == Cucumber.None)
				{
					next[ni][j] = Cucumber.South;
					next[i][j] = Cucumber.None;
					moved = true;
				}
			}
		}
	}

	list = next;
	next = new(list.Count);
	for (int i = 0; i < list.Count; i++) next.Add(new(list[i]));
}

Console.WriteLine(step);

int WrapI(int i) => i < list.Count ? i : 0;
int WrapJ(int j) => j < list[0].Count ? j : 0;

static Cucumber Map(char c) => c switch
{
	'>' => Cucumber.East,
	'v' => Cucumber.South,
	'.' => Cucumber.None,
	_ => throw new NotImplementedException()
};

enum Cucumber
{
	None, East, South, 
}