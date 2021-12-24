/*
using InputHandler;

int nextID = 0;
List<Cuboid> instructions = Input.ListUntilEmpty(Console.ReadLine, x => new Cuboid(x, nextID++));
HashSet<Cuboid> cuboids = new(instructions);
HashSet<(Cuboid, Cuboid)> comparisons = new();

while (true)
{
	Cuboid mA, mB;
	foreach (Cuboid a in cuboids)
	{
		foreach (Cuboid b in cuboids.Where(q => q.id != a.id && !comparisons.Contains(Order(a, q))))
		{
			comparisons.Add(Order(a, b));

			if (Cuboid.Intersect(a, b))
			{
				mA = a;
				mB = b;

				goto Next;
			}
		}
	}

	break;

Next:
	Console.WriteLine($"splitting [{mA}] and [{mB}]");
	foreach (Cuboid c in Cuboid.Shatter(mA, mB))
	{
		cuboids.Add(c);
	}

	cuboids.Remove(mA);
	cuboids.Remove(mB);
}

foreach (Cuboid instruction in instructions)
{
	//Console.WriteLine($"i: {instruction}");
	foreach (Cuboid cuboid in cuboids)
	{
		if (instruction.Contains(cuboid))
		{
			cuboid.on = instruction.on;
			//Console.WriteLine($"turning {cuboid.Volume} {cuboid.on}");
		}
	}
}

Console.WriteLine($"count: {cuboids.Count}");
foreach (Cuboid cuboid in cuboids)
{
	Console.WriteLine(cuboid);
}

long count = cuboids.Where(x => x.on).Sum(x => x.Volume);
Console.WriteLine(count);

static (Cuboid, Cuboid) Order(Cuboid x, Cuboid y) => x.id < y.id ? (x, y) : (y, x);

class Cuboid
{
	public int sx, ex, sy, ey, sz, ez;
	public bool on;
	public int id = 0;
	public Cuboid parent;

	public Cuboid(string s, int id)
	{
		on = s[..2] == "on";

		string[] split = s.Split(new char[] { '=', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);

		sx = int.Parse(split[1]);
		ex = int.Parse(split[2]);
		sy = int.Parse(split[4]);
		ey = int.Parse(split[5]);
		sz = int.Parse(split[7]);
		ez = int.Parse(split[8]);

		this.id = id;
	}

	public Cuboid(int x1, int x2, int y1, int y2, int z1, int z2, Cuboid parent)
	{
		sx = Min(x1, x2);
		ex = Max(x1, x2);

		sy = Min(y1, y2);
		ey = Max(y1, y2);

		sz = Min(z1, z2);
		ez = Max(z1, z2);

		this.parent = parent;
		id = parent.id;
		on = false;
	}

	public Cuboid GetPatriarch()
	{
		if (parent is null) return this;
		else return parent.GetPatriarch();
	}

	public long Volume => (ex - sx + 1) * (ey - sy + 1) * (ez - sz + 1);

	public override string ToString() => $"({sx}..{ex}, {sy}..{ey}, {sz}..{ez}) has id {id}, is {on}, and has volume {Volume}";

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
		int ax, ay, az, bx, by, bz;
		int asx, asy, asz, aex, aey, aez;
		int bsx, bsy, bsz, bex, bey, bez;
		int dx, dy, dz;
		bool sax, say, saz, sbx, sby, sbz;

		if (a.sx <= b.sx && b.sx <= a.ex)
		{
			ax = b.sx;
			asx = a.sx;
			aex = a.ex;

			if (a.ex < b.ex)
			{
				bx = a.ex;
				bsx = b.ex;
			}
			else
			{
				bx = b.ex;
				bsx = a.ex;
			}

			bex = b.sx;

			dx = -1;
		}
		else
		{
			if (b.ex < a.ex)
			{
				ax = b.ex;
				asx = a.ex;
			}
			else
			{
				ax = a.ex;
				asx = b.ex;
			}

			aex = a.sx;

			bx = a.sx;
			bsx = b.sx;
			bex = b.ex;

			dx = +1;
		}

		if (a.sy <= b.sy && b.sy <= a.ey)
		{
			ay = b.sy;
			asy = a.sy;
			aey = a.ey;

			if (a.ey < b.ey)
			{
				by = a.ey;
				bsy = b.ey;
			}
			else
			{
				by = b.ey;
				bsy = a.ey;
			}

			bey = b.sy;

			dy = -1;
		}
		else
		{
			if (b.ey < a.ey)
			{
				ay = b.ey;
				asy = a.ey;
			}
			else
			{
				ay = a.ey;
				asy = b.ey;
			}

			aey = a.sy;

			by = a.sy;
			bsy = b.sy;
			bey = b.ey;

			dy = +1;
		}

		if (a.sz <= b.sz && b.sz <= a.ez)
		{
			az = b.sz;
			asz = a.sz;
			aez = a.ez;

			if (a.ez < b.ez)
			{
				bz = a.ez;
				bsz = b.ez;
			}
			else
			{
				bz = b.ez;
				bsz = a.ez;
			}

			bez = b.sz;

			dz = -1;
		}
		else
		{
			if (b.ez < a.ez)
			{
				az = b.ez;
				asz = a.ez;
			}
			else
			{
				az = a.ez;
				asz = b.ez;
			}

			aez = a.sz;

			bz = a.sz;
			bsz = b.sz;
			bez = b.ez;

			dz = +1;
		}

		sax = asx == ax;
		say = asy == ay;
		saz = asz == az;
		sbx = bsx == bx;
		sby = bsy == by;
		sbz = bsz == bz;

		Cuboid c;

		c = Construct(asx, ax + dx, asy, aey, asz, aez, a, sax);
		{ if (c is Cuboid q) yield return q; }
		c = Construct(ax, aex, asy, ay + dy, asz, aez, a, say);
		{ if (c is Cuboid q) yield return q; }
		c = Construct(ax, aex, ay, aey, asz, az + dz, a, saz);
		{ if (c is Cuboid q) yield return q; }

		c = Construct(ax, bx, ay, by, az, bz, a, false);
		{ if (c is Cuboid q) yield return q; }

		c = Construct(bsx, bx - dx, bsy, bey, bsz, bez, b, sbx);
		{ if (c is Cuboid q) yield return q; }
		c = Construct(bx, bex, bsy, by - dy, bsz, bez, b, sby);
		{ if (c is Cuboid q) yield return q; }
		c = Construct(bx, bex, by, bey, bsz, bz - dz, b, sbz);
		{ if (c is Cuboid q) yield return q; }

		static Cuboid Construct(int x1, int x2, int y1, int y2, int z1, int z2, Cuboid parent, bool b)
		{
			if (b) return null;

			return new Cuboid(x1, x2, y1, y2, z1, z2, parent).Log();
		}
	}

	private static int Min(int x, int y) => x < y ? x : y;
	private static int Max(int x, int y) => x > y ? x : y;
}
*/