// I solved both parts of this one by hand, but here's a cute checker program:
using InstructionList = System.Collections.Generic.List<(int, char, CharOrInt)>;
using InputHandler;

Console.WriteLine("Enter instructions:");
InstructionList list = Input.ListUntilEmpty(Console.ReadLine, Ctor);
Console.WriteLine("Enter input:");
long input = long.Parse(Input.Get(Console.ReadLine, s => long.TryParse(s, out _)));

ALU alu = new(list);
alu.SetInputs(Enumerate(input).Reverse());
alu.Execute();

Console.WriteLine($"Output Z = {alu.Z}");


IEnumerable<int> Enumerate(long x)
{
	while (x > 0)
	{
		yield return (int)(x % 10);
		x /= 10;
	}
}

(int, char, CharOrInt) Ctor(string s)
{
	string[] split = s.Split();

	int type = Map(split[0]);

	char a = split[1][0];

	CharOrInt b = new(0);

	if (split[0] != "inp")
	{
		if (int.TryParse(split[2], out int q))
		{
			b = new(q);
		}
		else b = new(split[2][0]);
	}

	return (type, a, b);
}

int Map(string s) => s switch
{
	"inp" => 0,
	"add" => 1,
	"mul" => 2,
	"div" => 3,
	"mod" => 4,
	"eql" => 5,
	_ => throw new NotImplementedException(s)
};

public class ALU
{
	int x = 0, y = 0, z = 0, w = 0;
	Queue<int> inputs;
	public InstructionList Instructions { get; set; }

	public ALU() { }

	public ALU(InstructionList instructions)
	{
		Instructions = instructions;
	}

	public override string ToString() => $"w = {w}, x = {x}, y = {y}, z = {z}";

	public void Reset()
	{
		x = y = z = w = 0;
	}

	public void SetInputs(IEnumerable<int> inputs)
	{
		this.inputs ??= new();
		this.inputs.Clear();

		foreach (int i in inputs) this.inputs.Enqueue(i);
	}

	public void Execute(int hack = -1)
	{
		foreach (var s in Instructions) Run(s, hack);
	}

	private void Run((int type, char a, CharOrInt b) t, int hack = -1)
	{
		int a = Variable(t.a);
		int b = 0;
		if (t.type != 0)
		{
			if (t.b.isInt) b = t.b.x;
			else b = Variable(t.b.c);
		}

		switch (t.type)
		{
			case 0:
				//Variable(t.a) = inputs.Dequeue();
				if (hack == -1) Variable(t.a) = inputs.Dequeue();
				else Variable(t.a) = hack;
				break;
			case 1:
				Variable(t.a) = a + b;
				break;
			case 2:
				Variable(t.a) = a * b;
				break;
			case 3:
				Variable(t.a) = a / b;
				break;
			case 4:
				Variable(t.a) = a % b;
				break;
			case 5:
				Variable(t.a) = a == b ? 1 : 0;
				break;
		}
	}

	ref int Variable(char a)
	{
		switch (a)
		{
			case 'x':
				return ref x;
			case 'y':
				return ref y;
			case 'z':
				return ref z;
			case 'w':
				return ref w;
			default:
				throw new Exception($"Impossible variable {a}");
		}
	}

	public bool ValidMONAD => z == 0;

	public int Z { get => z; set => z = value; }
}

public struct CharOrInt
{
	public bool isInt;
	public int x = 0;
	public char c = '?';

	public CharOrInt(int x)
	{
		this.x = x;
		isInt = true;
	}
	
	public CharOrInt(char c)
	{
		this.c = c;
		isInt = false;
	}


}