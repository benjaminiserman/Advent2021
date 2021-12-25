using InputHandler;

List<Cuboid> instructions = Input.ListUntilEmpty(Console.ReadLine, x => new Cuboid(x));
HashSet<Cuboid> cuboids = new(new CuboidEqualityComparer());
HashSet<Cuboid> newCuboid = new(new CuboidEqualityComparer());

foreach (Cuboid instruction in instructions)
{
	Console.WriteLine($"Calculating: {instruction}");
	newCuboid.Clear();
	newCuboid.Add(instruction);

	while (true)
	{
		Cuboid mA, mB;
		foreach (Cuboid a in newCuboid)
		{
			foreach (Cuboid b in cuboids)
			{
				if (Cuboid.Intersect(a, b))
				{
					mA = a;
					mB = b;

					goto Next;
				}
			}
		}

		foreach (Cuboid c in newCuboid) cuboids.Add(c);

		break;
	
	Next:
		//Console.WriteLine($"splitting [{mA}] and [{mB}]");
		newCuboid.Remove(mA);
		cuboids.Remove(mB);
		foreach (Cuboid c in Cuboid.Shatter(mA, mB))
		{
			if (c.parent == mA)
			{
				c.on = mA.on;
				newCuboid.Add(c);
			}
			else
			{
				c.on = mB.on;
				cuboids.Add(c);
			}
			//Console.WriteLine(c);
		}
	}
}

/*Console.WriteLine($"FINAL CUBOIDS\ncount: {cuboids.Count}");
foreach (Cuboid cuboid in cuboids)
{
	Console.WriteLine(cuboid);
}*/

long count = cuboids.Where(x => x.on).Sum(x => x.Volume);
Console.WriteLine(count);

//static (Cuboid, Cuboid) Order(Cuboid x, Cuboid y) => x.id < y.id ? (x, y) : (y, x);

class Cuboid
{
	public long sx, ex, sy, ey, sz, ez;
	public bool on;
	//public int id = 0;
	public Cuboid parent;

	public Cuboid(string s)
	{
		on = s[..2] == "on";

		string[] split = s.Split(new char[] { '=', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);

		sx = int.Parse(split[1]);
		ex = int.Parse(split[2]);
		sy = int.Parse(split[4]);
		ey = int.Parse(split[5]);
		sz = int.Parse(split[7]);
		ez = int.Parse(split[8]);

		//this.id = id;
	}

	public Cuboid(long x1, long x2, long y1, long y2, long z1, long z2, Cuboid parent)
	{
		sx = Min(x1, x2);
		ex = Max(x1, x2);

		sy = Min(y1, y2);
		ey = Max(y1, y2);

		sz = Min(z1, z2);
		ez = Max(z1, z2);

		this.parent = parent;
		//id = parent.id;
		on = false;
	}

	public Cuboid GetPatriarch()
	{
		if (parent is null) return this;
		else return parent.GetPatriarch();
	}

	public long Volume => (ex - sx + 1) * (ey - sy + 1) * (ez - sz + 1);

	public override string ToString() => $"({sx}..{ex}, {sy}..{ey}, {sz}..{ez}) is {on} and has volume {Volume}";

	public Cuboid Log()
	{
		Console.WriteLine(this);
		return this;
	}

	public bool Contains(Cuboid c) =>
		sx <= c.sx && ex >= c.ex &&
		sy <= c.sy && ey >= c.ey &&
		sz <= c.sz && ez >= c.ez;

	public static bool Intersect(Cuboid a, Cuboid b)
	{
		if ((a.sx <= b.sx && b.sx <= a.ex) ||
			(b.sx <= a.sx && a.sx <= b.ex))
		{
			if ((a.sy <= b.sy && b.sy <= a.ey) ||
				(b.sy <= a.sy && a.sy <= b.ey))
			{
				if ((a.sz <= b.sz && b.sz <= a.ez) ||
					(b.sz <= a.sz && a.sz <= b.ez))
				{
					return true;
				}
			}
		}

		return false;
	}

	public static IEnumerable<Cuboid> Shatter(Cuboid a, Cuboid b)
	{
		Cuboid asx = MinBy(a, b, q => q.sx);
		Cuboid bsx = MaxBy(a, b, q => q.sx);
		Cuboid aex = MinBy(a, b, q => q.ex);
		Cuboid bex = MaxBy(a, b, q => q.ex);

		Cuboid asy = MinBy(a, b, q => q.sy);
		Cuboid bsy = MaxBy(a, b, q => q.sy);
		Cuboid aey = MinBy(a, b, q => q.ey);
		Cuboid bey = MaxBy(a, b, q => q.ey);

		Cuboid asz = MinBy(a, b, q => q.sz);
		Cuboid bsz = MaxBy(a, b, q => q.sz);
		Cuboid aez = MinBy(a, b, q => q.ez);
		Cuboid bez = MaxBy(a, b, q => q.ez);

		if (a.sx != b.sx) yield return new Cuboid(asx.sx, bsx.sx - 1, asx.sy, asx.ey, asx.sz, asx.ez, asx);
		if (a.ex != b.ex) yield return new Cuboid(aex.ex + 1, bex.ex, bex.sy, bex.ey, bex.sz, bex.ez, bex);

		if (a.sy != b.sy) yield return new Cuboid(bsx.sx, aex.ex, asy.sy, bsy.sy - 1, asy.sz, asy.ez, asy);
		if (a.ey != b.ey) yield return new Cuboid(bsx.sx, aex.ex, aey.ey + 1, bey.ey, bey.sz, bey.ez, bey);

		if (a.sz != b.sz) yield return new Cuboid(bsx.sx, aex.ex, bsy.sy, aey.ey, asz.sz, bsz.sz - 1, asz);
		if (a.ez != b.ez) yield return new Cuboid(bsx.sx, aex.ex, bsy.sy, aey.ey, aez.ez + 1, bez.ez, bez);

		yield return new Cuboid(bsx.sx, aex.ex, bsy.sy, aey.ey, bsz.sz, aez.ez, a);
	}

	private static Cuboid MinBy(Cuboid a, Cuboid b, Func<Cuboid, long> by) => by(a) < by(b) ? a : b;
	private static Cuboid MaxBy(Cuboid a, Cuboid b, Func<Cuboid, long> by) => by(a) > by(b) ? a : b;

	private static long Min(long x, long y) => x < y ? x : y;
	private static long Max(long x, long y) => x > y ? x : y;

	public static bool operator ==(Cuboid a, Cuboid b) => a.sx == b.sx && a.sy == b.sy && a.sz == b.sz && a.ex == b.ex && a.ey == b.ey && a.ez == b.ez;

	public static bool operator !=(Cuboid a, Cuboid b) => !(a == b);
}

class CuboidEqualityComparer : IEqualityComparer<Cuboid>
{
	public bool Equals(Cuboid x, Cuboid y) => x == y;
	public int GetHashCode(Cuboid q) => (int)(q.sx ^ q.ex ^ q.sy ^ q.ey ^ q.sz ^ q.ez);
}