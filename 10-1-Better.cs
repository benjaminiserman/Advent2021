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

while (true)
{
    string s = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(s)) break;

    foreach (char c in s)
    {
        if (braces.ContainsKey(c)) stack.Push(c);
        else if (c != braces[stack.Pop()])
        {
            counts[c]++;
            break;
        }
    }
}

Dictionary<char, int> scores = new()
{
    { ')', 3 },
    { ']', 57 },
    { '}', 1197 },
    { '>', 25137 },
};

Console.WriteLine(counts.Values.Zip(scores.Values, (count, score) => (long)(count * score)).Sum());