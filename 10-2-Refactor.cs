/*
Dictionary<char, char> braces = new()
{
    { '(', ')' },
    { '[', ']' },
    { '{', '}' },
    { '<', '>' },
};

Dictionary<char, int> counts = new()
{
    { ')', 0 },
    { ']', 0 },
    { '}', 0 },
    { '>', 0 },
};

Stack<char> stack = new();
List<long> scores = new();

while (true)
{
    string s = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(s)) break;

    bool corrupt = false;
    foreach (char c in s)
    {
        if (braces.ContainsKey(c)) stack.Push(c);
        else if (c != braces[stack.Pop()])
        {
            counts[c]++;
            corrupt = true;
            break;
        }
    }

    if (!corrupt)
    {
        long score = 0;
        while (stack.TryPop(out char c))
        {
            score *= 5;
            score += c switch
            {
                '(' => 1,
                '[' => 2,
                '{' => 3,
                '<' => 4,
                _ => throw new NotImplementedException()
            };
        }

        scores.Add(score);
    }

    stack.Clear();
}

var sorted = (from x in scores orderby x select x).ToArray();
Console.WriteLine(sorted[sorted.Length / 2]);
*/