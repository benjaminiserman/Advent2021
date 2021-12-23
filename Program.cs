using System.Linq;
using InputHandler;

int nextID = 0;
List<Cuboid> instructions = Input.ListUntilEmpty(Console.ReadLine, x => new Cuboid(x, nextID++));
List<Cuboid> cuboids = new(instructions);
HashSet<(Cuboid, Cuboid)> comparisons = new();

while (true)
{
	for (int i = 0; i < cuboids.Count - 1; i++)
	{
		for (int j = i + 1; j < cuboids.Count; j++)
		{
			if (cuboids[i].id != cuboids[j].id)
			{
				Cuboid a = cuboids[i];
				Cuboid b = cuboids[j];

				var ordered = Order(a, b);
				if (!comparisons.Contains(ordered))
				{
					//Console.WriteLine($"compare [{a}] & [{b}]");
					if (Cuboid.Intersect(a, b) && !a.Contains(b) && !b.Contains(a))
					{
						Console.WriteLine($"splitting [{a}] and [{b}]");
						foreach (Cuboid c in Cuboid.Shatter(a, b))
						{
							cuboids.Add(c);
						}

						cuboids.Remove(a);
						cuboids.Remove(b);

						comparisons.Add(ordered);
						goto Next;
					}
					else comparisons.Add(ordered);
				}
			}
		}
	}

	break;

	Next:;
}

foreach (Cuboid instruction in instructions)
{
	foreach (Cuboid cuboid in cuboids)
	{
		if (instruction.Contains(cuboid))
		{
			cuboid.on = instruction.on;
		}
	}
}

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

	private Cuboid(int x1, int x2, int y1, int y2, int z1, int z2, Cuboid parent)
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
		if (a.sx <= b.sx && b.sx <= a.ex ||
			b.sx <= a.sx && a.sx <= b.ex)
		{
			if (a.sy <= b.sy && b.sy <= a.ey ||
				b.sy <= a.sy && a.sy <= b.ey)
			{
				if (a.sz <= b.sz && b.sz <= a.ez ||
					b.sz <= a.sz && a.sz <= b.ez)
				{
					return true;
				}
			}
		}

		return false;
	}

	/*public static bool Aligned(Cuboid a, Cuboid b) // bad name
	{
		if (a.sx < b.sx && b.sx < a.ex ||
			b.sx < a.sx && a.sx < b.ex) return false;
		if (a.sy < b.sy && b.sy < a.ey ||
			b.sy < a.sy && a.sy < b.ey) return false;
		if (a.sz < b.sz && b.sz < a.ez ||
			b.sz < a.sz && a.sz < b.ez) return false;

		return true;
	}*/

	public static IEnumerable<Cuboid> Shatter(Cuboid a, Cuboid b)
	{
		int ax, ay, az, bx, by, bz;
		int asx, asy, asz, aex, aey, aez;
		int bsx, bsy, bsz, bex, bey, bez;
		int dx, dy, dz;
		bool sax, say, saz, sbx, sby, sbz;

		sax = a.sx == a.ex;
		say = a.sy == a.ey;
		saz = a.sz == a.ez;
		sbx = b.sx == b.ex;
		sby = b.sy == b.ey;
		sbz = b.sz == b.ez;

		if (a.sx <= b.sx && b.sx <= a.ex)
		{
			ax = b.sx;
			asx = a.sx;
			aex = a.ex;

			bx = a.ex;
			bsx = b.ex;
			bex = b.sx;

			dx = -1;
		}
		else
		{
			ax = b.ex;
			asx = a.ex;
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

			by = a.ey;
			bsy = b.ey;
			bey = b.sy;

			dy = -1;
		}
		else
		{
			ay = b.ey;
			asy = a.ey;
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

			bz = a.ez;
			bsz = b.ez;
			bez = b.sz;

			dz = -1;
		}
		else
		{
			az = b.ez;
			asz = a.ez;
			aez = a.sz;

			bz = a.sz;
			bsz = b.sz;
			bez = b.ez;

			dz = +1;
		}

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

			return new Cuboid(x1, x2, y1, y2, z1, z2, parent);//.Log();
		}
	}

	private static int Min(int x, int y) => x < y ? x : y;
	private static int Max(int x, int y) => x > y ? x : y;
}