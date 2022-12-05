using System.Diagnostics;
using Essentials;

var input = File.ReadAllText("./data/input.txt");
var elves = Elf.ReadElves(input);

var sorted = elves.OrderByDescending(e => e.Snacks.Sum());
var part1 = sorted.First().Snacks.Sum();
Debug.WriteLine($"Part 1: Elf with the most calorie snacks has {part1} calories");

var part2 = sorted.Take(3).SelectMany(e => e.Snacks).Sum();
Debug.WriteLine($"Part 2: Top 3 elves with the most calorie snacks have {part2} calories");

/* Output:

Part 1: Elf with the most calorie snacks has 69206 calories
Part 2: Top 3 elves with the most calorie snacks have 197400 calories
The program '[64772] day01.exe' has exited with code 0 (0x0).

*/

class Elf
{
    public List<int> Snacks { get; } = new();

    public static IReadOnlyList<Elf> ReadElves(string lineSeparatedValues)
    {
        var elves = new List<Elf>();

        var groups = lineSeparatedValues.Split("\n\n");
        foreach (var group in groups)
        {
            Elf elf = new();
            elf.Snacks.AddRange(group.ToIntArray('\n'));
            elves.Add(elf);
            //Debug.WriteLine($"Elf {elves.Count} has {elf.Snacks.Count} snacks");
        }

        return elves;
    }

}
