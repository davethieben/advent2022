using System.Diagnostics;
using Essentials;

const string priorities = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

int GetPriority(char x)
{
    for (int index = 0; index < priorities.Length; index++)
    {
        if (priorities[index] == x) return index + 1;
    }
    return 0;
}

var inputs = File.ReadAllText("./data/input.txt")
                 .Split('\n')
                 .Select(line => (line?.Trim()) ?? string.Empty);

Debug.WriteLine($"Found {inputs.Count()} inputs");

int totalSum = 0;

foreach (var input in inputs)
{
    var items = input.ToArray();
    var itemCount = items.Length / 2;

    var itemsA = input.Take(itemCount).ToArray();
    var itemsB = input.Skip(itemCount).Take(itemCount).ToArray();

    var commonItems = itemsA.Where(i => itemsB.Contains(i)).Distinct().ToArray();
    Debug.WriteLine($" - Common items: {commonItems.ToDisplayString()}");

    int inputSum = 0;
    foreach (var item in commonItems)
        inputSum += GetPriority(item);

    totalSum += inputSum;
    Debug.WriteLine($"Current Sum: {inputSum}; Total Sum: {totalSum}");
}


