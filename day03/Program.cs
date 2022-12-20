using System.Diagnostics;
using Essentials;

const string priorities = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

int GetPriority(char x)
{
    for (int i = 0; i < priorities.Length; i++)
    {
        if (priorities[i] == x) return i + 1;
    }
    return 0;
}

var inputLines = File.ReadAllText("./data/input.txt")
                 .Split('\n')
                 .Select(line => (line?.Trim()) ?? string.Empty)
                 .ToArray();

Debug.WriteLine($"Found {inputLines.Count()} inputs");

Debug.WriteLine($"Part One Total Sum: {PartOne()}");
Debug.WriteLine($"Part Two Total Sum: {PartTwo()}");


int PartOne()
{
    int totalSum = 0;

    foreach (var input in inputLines)
    {
        var items = input.ToArray();
        var itemCount = items.Length / 2;

        var itemsA = input.Take(itemCount).ToArray();
        var itemsB = input.Skip(itemCount).Take(itemCount).ToArray();

        var commonItems = itemsA.Where(i => itemsB.Contains(i)).Distinct().ToArray();

        int inputSum = 0;
        foreach (var item in commonItems)
            inputSum += GetPriority(item);

        totalSum += inputSum;
    }

    return totalSum;
}

int PartTwo()
{
    int totalSum = 0;

    for (int index = 0; index + 3 < inputLines.Length; index += 3)
    {
        char[] itemsA = inputLines[index].ToArray();
        char[] itemsB = inputLines[index + 1].ToArray();
        char[] itemsC = inputLines[index + 2].ToArray();

        var commonItems = itemsA.Intersect(itemsB).Intersect(itemsC);

        int inputSum = 0;
        foreach (var item in commonItems)
            inputSum += GetPriority(item);

        totalSum += inputSum;
    }

    return totalSum;
}