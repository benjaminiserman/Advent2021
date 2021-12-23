/*
Console.WriteLine("Calculating...");
new Board(false).Enumerate();
Console.WriteLine(Board.costs.Min());

class Board
{
	Amphipod?[] hallway = new Amphipod?[11];
	Room[] rooms = new Room[4];
	public int cost = 0;
	public static HashSet<int> costs = new();

	Board(Board b, (Amphipod, Position) move)
	{
		for (int i = 0; i < hallway.Length; i++) hallway[i] = b.hallway[i] is Amphipod a ? new(a) : null;
		for (int i = 0; i < rooms.Length; i++) rooms[i] = new(b.rooms[i]);
		cost = b.cost;

		MakeMove(move);
		Enumerate();
	}

	public Board(bool example)
	{
		if (example)
		{
			rooms[0] = new('B', 'D', 'D', 'A', 0);
			rooms[1] = new('C', 'C', 'B', 'D', 1);
			rooms[2] = new('B', 'B', 'A', 'C', 2);
			rooms[3] = new('D', 'A', 'C', 'A', 3);
		}
		else
		{
			rooms[0] = new('C', 'D', 'D', 'B', 0);
			rooms[1] = new('B', 'C', 'B', 'C', 1);
			rooms[2] = new('A', 'B', 'A', 'D', 2);
			rooms[3] = new('D', 'A', 'C', 'A', 3);
		}
	}

	public override string ToString()
	{
		System.Text.StringBuilder sb = new();
		sb.Append($"Hallway: {{ ");
		foreach (Amphipod? a in hallway) sb.Append(a is null ? "empty, " : $"{a}, ");
		sb.Append($"}} Rooms: {{ ");
		foreach (Room r in rooms) sb.Append($"\n{r}");
		sb.Append(" }");

		return sb.ToString();
	}

	public void Enumerate()
	{
		foreach ((Amphipod, Position) move in PossibleMoves())
		{
			new Board(this, move);
		}

		if (Complete) costs.Add(cost);
	}

	bool Complete => !rooms.Any(x => !x.Complete);

	Board MakeMove((Amphipod a, Position p) move)
	{
		try
		{
			if (move.a.position.room != -1)
			{
				cost += rooms[move.a.position.room].Remove();
			}
			else
			{
				hallway[move.a.position.hallway] = null;
			}

			if (move.p.room != -1)
			{
				cost += rooms[move.p.room].Insert(move.a);
			}
			else
			{
				hallway[move.p.hallway] = new(move.a) { position = new (move.p.hallway, -1) };
			}

			cost += move.a.Cost(move.a.position - move.p);

			return this;
		}
		catch (Exception e)
		{
			Console.WriteLine($"Move {move.a} -> {move.p} failed.");
			Console.WriteLine(this);
			Console.WriteLine(e.Message);
			throw;
		}
	}

	IEnumerable<(Amphipod, Position)> PossibleMoves()
	{
		foreach (Amphipod a in FromRooms())
		{
			if (a.position.room == a.Index && rooms[a.Index].CanMoveIn(a)) continue;

			if (rooms[a.Index].CanMoveIn(a) && a.CanReach(new(-1, a.Index), hallway))
			{
				yield return (a, new(-1, a.Index));
				break;
			}
			else
			{
				foreach (Position p in HallWayPossible())
				{
					if (a.CanReach(p, hallway)) yield return (a, p);
				}
			}
		}

		foreach (Amphipod a in FromHallway())
		{
			if (rooms[a.Index].CanMoveIn(a) && a.CanReach(new(-1, a.Index), hallway))
			{
				yield return (a, new(-1, a.Index));
				break;
			}
		}
	}

	IEnumerable<Amphipod> FromRooms()
	{
		Amphipod? a;

		foreach (Room room in rooms)
		{
			a = room.Top;
			if (a is Amphipod b)
			{
				yield return b;
			}
		}
	}

	IEnumerable<Amphipod> FromHallway()
	{
		if (hallway[1] is Amphipod a) yield return a;
		else if (hallway[0] is Amphipod b) yield return b;

		if (hallway[9] is Amphipod c) yield return c;
		else if (hallway[10] is Amphipod d) yield return d;

		if (hallway[3] is Amphipod e) yield return e;
		if (hallway[5] is Amphipod f) yield return f;
		if (hallway[7] is Amphipod g) yield return g;
	}

	IEnumerable<Position> HallWayPossible()
	{
		if (hallway[0] == null) yield return new(0, -1);
		if (hallway[1] == null) yield return new(1, -1);
		if (hallway[3] == null) yield return new(3, -1);
		if (hallway[5] == null) yield return new(5, -1);
		if (hallway[7] == null) yield return new(7, -1);
		if (hallway[9] == null) yield return new(9, -1);
		if (hallway[10] == null) yield return new(10, -1);
	}

	class Room
	{
		public Amphipod?[] occupants = new Amphipod?[4];
		public int index;

		public Room(char a, char b, char c, char d, int index)
		{
			occupants = new Amphipod?[] { new(a), new(b), new(c), new(d) };
			for (int i = 0; i < occupants.Length; i++) occupants[i] = new Amphipod((Amphipod)occupants[i]) { position = new(-1, index) };
			this.index = index;
		}

		public Room(Room r)
		{
			for (int i = 0; i < occupants.Length; i++) occupants[i] = r.occupants[i] is Amphipod a ? new(a) : null;
			index = r.index;
		}

		public override string ToString() => $"[{index}] => {{ {occupants[0]}, {occupants[1]}, {occupants[2]}, {occupants[3]} }}";

		public bool Complete => !occupants.Any(x => x is not Amphipod a || a.Index != index);

		public bool CanMoveIn(Amphipod a)
		{
			if (a.Index != index) return false;

			return Ready;
		}

		private bool Ready
		{
			get
			{
				for (int i = occupants.Length - 1; i >= 0; i--)
				{
					if (occupants[i] is Amphipod a && a.Index != index)
					{
						return false;
					}
					else if (occupants[i] is null) return true;
				}

				return true;
			}
		}

		public Amphipod? Top
		{
			get
			{
				for (int i = 0; i < occupants.Length; i++)
				{
					if (occupants[i] != null) return occupants[i];
				}

				return null;
			}
		}

		public int Remove()
		{
			for (int i = 0; i < occupants.Length; i++)
			{
				if (occupants[i] != null)
				{
					Amphipod a = (Amphipod)occupants[i];
					occupants[i] = null;
					return a.Cost(i + 1);
				}
			}

			throw new Exception($"Impossible remove: {this}");
		}

		public int Insert(Amphipod a)
		{
			for (int i = occupants.Length - 1; i >= 0; i--)
			{
				if (occupants[i] == null)
				{
					occupants[i] = new (a) { position = new(-1, index) };
					return a.Cost(i + 1);
				}
			}

			throw new Exception($"Impossible insert: {a} @ {this}");
		}

		public static int Outlet(int index) => index switch
		{
			0 => 2,
			1 => 4,
			2 => 6,
			3 => 8,
			_ => throw new Exception("Impossible index.")
		};
	}

	struct Amphipod
	{
		public char type;
		public Position position;

		public Amphipod(char c)
		{
			type = c;
			position = new(-1, -1);
		}

		public Amphipod(Amphipod a)
		{
			type = a.type;
			position = a.position;
		}

		public override string ToString() => $"{type} @ {position}";

		public bool CanReach(Position move, Amphipod?[] hallway)
		{
			if (hallway[move.HallPosition] != null) return false;

			for (int i = position.HallPosition + (position.HallPosition < move.HallPosition ? 1 : -1);
				i != move.HallPosition;
				Towards(ref i, move.HallPosition))
			{
				if (hallway[i] != null) return false;
			}

			return true;

			int Towards(ref int i, int p)
			{
				if (i < p) return i++;
				else return i--;
			}
		}

		public int Cost(int x) => x * type switch
		{
			'A' => 1,
			'B' => 10,
			'C' => 100,
			'D' => 1000,
			_ => throw new Exception("Impossible type.")
		};

		public int Index => type switch
		{
			'A' => 0,
			'B' => 1,
			'C' => 2,
			'D' => 3,
			_ => throw new Exception("Impossible type.")
		};

		public static bool operator ==(Amphipod a, Amphipod b) => a.type == b.type && a.position == b.position;

		public static bool operator !=(Amphipod a, Amphipod b) => !(a == b);
	}

	struct Position
	{
		public int hallway = -1;
		public int room = -1;

		public override string ToString() => room == -1 ? $"hall {hallway}" : $"room {room}";

		public Position(int hallway, int room)
		{
			this.hallway = hallway;
			this.room = room;
		}

		public Position(Position p) : this(p.hallway, p.room) { }

		public bool Stuck => hallway != -1;

		public int HallPosition => room == -1 ? hallway : Room.Outlet(room);

		public static int operator -(Position a, Position b) => Math.Abs(a.HallPosition - b.HallPosition);

		public static bool operator ==(Position a, Position b) => a.hallway == b.hallway && a.room == b.room;

		public static bool operator !=(Position a, Position b) => !(a == b);
	}
}
*/