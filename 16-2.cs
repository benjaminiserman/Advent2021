/*
using InputHandler; // hehe I brought in my input library (check it out on my github!)
using System.Numerics;

string s = Console.ReadLine();

List<int> binary = new();

foreach (char c in s)
{
    binary.AddRange(c switch
    {
        '0' => new List<int>() { 0, 0, 0, 0 },
        '1' => new List<int>() { 0, 0, 0, 1 },
        '2' => new List<int>() { 0, 0, 1, 0 },
        '3' => new List<int>() { 0, 0, 1, 1 },
        '4' => new List<int>() { 0, 1, 0, 0 },
        '5' => new List<int>() { 0, 1, 0, 1 },
        '6' => new List<int>() { 0, 1, 1, 0 },
        '7' => new List<int>() { 0, 1, 1, 1 },
        '8' => new List<int>() { 1, 0, 0, 0 },
        '9' => new List<int>() { 1, 0, 0, 1 },
        'A' => new List<int>() { 1, 0, 1, 0 },
        'B' => new List<int>() { 1, 0, 1, 1 },
        'C' => new List<int>() { 1, 1, 0, 0 },
        'D' => new List<int>() { 1, 1, 0, 1 },
        'E' => new List<int>() { 1, 1, 1, 0 },
        'F' => new List<int>() { 1, 1, 1, 1 },
        _ => throw new InvalidOperationException()
    });
}

int i = 0;
Packet packet = Parse(ref i);

Console.WriteLine(packet.Calculate());


int GrabX(ref int i, int x)
{
    int v = 0;
    for (int j = 0; j < x; j++)
    {
        v += binary[i + j] << x - j - 1;
    }

    i += x;

    return v;
}

Packet Parse(ref int i)
{
    Packet packet = new();

    packet.version += GrabX(ref i, 3); // why is this += ? the world may never know.
    packet.type = GrabX(ref i, 3);

    switch (packet.type)
    {
        case 4:
            while (true)
            {
                int grab = GrabX(ref i, 5);
                packet.val <<= 4;
                packet.val += grab & 0b1111;
                if ((grab & 0b10000) == 0) break;
            }

            break;
        default:
            packet.mode = binary[i++];
            if (packet.mode == 1)
            {
                packet.length = GrabX(ref i, 11);

                for (int j = 0; j < packet.length; j++)
                {
                    packet.subPackets.Add(Parse(ref i));
                }
            }
            else
            {
                packet.length = GrabX(ref i, 15);

                int ci = i;
                while(i < ci + packet.length)
                {
                    packet.subPackets.Add(Parse(ref i));
                }    
            }

            break;
    }

    return packet;
}

struct Packet
{
    public int version, type, mode, length;
    public BigInteger val;

    public List<Packet> subPackets = new();

    public BigInteger Calculate() => type switch
    {
        0 => Add(),
        1 => Mult(),
        2 => subPackets.Min(x => x.Calculate()),
        3 => subPackets.Max(x => x.Calculate()),
        4 => val,
        5 => subPackets[0].Calculate() > subPackets[1].Calculate() ? 1 : 0,
        6 => subPackets[0].Calculate() < subPackets[1].Calculate() ? 1 : 0,
        7 => subPackets[0].Calculate() == subPackets[1].Calculate() ? 1 : 0,
        _ => throw new Exception(),
    };

    private BigInteger Add()
    {
        BigInteger x = 0;
        foreach (Packet packet in subPackets)
        {
            x += packet.Calculate();
        }

        return x;
    }

    private BigInteger Mult()
    {
        BigInteger x = 1;
        foreach (Packet packet in subPackets)
        {
            x *= packet.Calculate();
        }

        return x;
    }
}
*/