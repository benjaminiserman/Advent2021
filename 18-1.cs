/*
using InputHandler;

List<Pair> list = Input.ListUntilEmpty<Pair>(Console.ReadLine, s => new Pair(s));

Pair sum = list.First();
foreach (var x in list.Skip(1))
{
    sum += x;
    //Console.WriteLine(sum);
}

Console.WriteLine(sum);
Console.WriteLine(sum.Magnitude());

class Pair
{
    int? val = null;
    Pair left = null;
    Pair right = null;

    public Pair(int x)
    {
        val = x;
    }

    public Pair(Pair left, Pair right)
    {
        this.left = left;
        this.right = right;
    }

    public Pair(string s)
    {
        if (int.TryParse(s, out int x))
        {
            val = x;
            return;
        }

        int open = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '[') open++;
            if (s[i] == ']') open--;
            if (s[i] == ',' && open == 1)
            {
                left = new Pair(s[1..i]);
                right = new Pair(s[(i + 1)..^1]);
            }
        }
    }

    public override string ToString() => val is null ? $"[{left},{right}]" : $"{val}";

    public long Magnitude()
    {
        if (val is not null) return val.Value;

        return left.Magnitude() * 3 + right.Magnitude() * 2;
    }

    public int MaxDepth(int x)
    {
        if (val == null)
        {
            return new int[] { left.MaxDepth(x + 1), right.MaxDepth(x + 1) }.Max();
        }
        else return x;
    }

    public bool CanSplit()
    { 
        foreach (var p in Enumerate())
        {
            if (p.val >= 10) return true;
        }

        return false;
    }

    private void Reduce()
    {
        while (MaxDepth(0) > 4 || CanSplit())
        {
            if (Explode(0, this))
            {
                //Console.WriteLine($"{this} => (depth: {MaxDepth(0)}, canSplit: {CanSplit()})");
                continue;
            }

            if (Split()) ;// Console.WriteLine($"{this} => (depth: {MaxDepth(0)}, canSplit: {CanSplit()})");
        }
    }

    private bool Explode(int depth, Pair head)
    {
        if (val is not null) return false;

        if (depth == 4)
        {
            int carryL = left.val.Value;
            int carryR = right.val.Value;
            left = null;
            right = null;
            val = 0;

            Pair before = head.Before(this);
            if (before is not null) before.val += carryL;

            Pair after = head.After(this);
            if (after is not null) after.val += carryR;

            return true;
        }
        else
        {
            bool b = left.Explode(depth + 1, head);
            if (b) return true;
            b = right.Explode(depth + 1, head);
            if (b) return true;
        }

        return false;
    }

    private bool Split()
    {
        if (val is not null)
        {
            if (val >= 10)
            {
                left = new Pair((int)Math.Floor((double)val / 2));
                right = new Pair((int)Math.Ceiling((double)val / 2));
                val = null;
                return true;
            }
        }
        else
        {
            bool b = left.Split();
            if (b) return true;
            b = right.Split();
            if (b) return true;
        }

        return false;
    }

    public IEnumerable<Pair> Enumerate()
    {
        if (val is not null) yield return this;
        else
        {
            foreach (var x in left.Enumerate()) yield return x;
            foreach (var x in right.Enumerate()) yield return x;
        }
    }

    private Pair Before(Pair x)
    {
        Pair carry = null;
        foreach (Pair p in Enumerate())
        {
            if (p == x)
            {
                return carry;
            }
            else carry = p;
        }

        return null;
    }

    private Pair After(Pair x)
    {
        bool found = false;
        foreach (Pair p in Enumerate())
        {
            if (p == x)
            {
                found = true;
            }
            else if (found) return p;
        }

        return null;
    }

    public static Pair operator +(Pair a, Pair b)
    {
        Pair x = new(a, b);

        x.Reduce();

        return x;
    }
}
*/