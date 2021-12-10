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

while (true)
{
    string s = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(s)) break;

    for (int i = 0; i < s.Length; i++)
    {
        if (braces.ContainsKey(s[i])) stack.Push(s[i]);
        else
        {
            if (s[i] != braces[stack.Pop()])
            {
                counts[s[i]]++;
                break;
            }
        }
    }
}

Console.WriteLine(counts[')'] * 3 + counts[']'] * 57 + counts['}'] * 1197 + counts['>'] * 25137);
*/