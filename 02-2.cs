long location = 0, depth = 0, aim = 0;
while (true)
{
    string got = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(got)) break;

    string[] instruction = got.Split();
    int x = int.Parse(instruction[1]);

    switch (instruction[0])
    {
        case "forward":
            location += x;
            depth += aim * x;
            break;
        case "up":
            aim -= x;
            break;
        case "down":
            aim += x;
            break;
    }
}

Console.WriteLine($"Key: {location * depth}");