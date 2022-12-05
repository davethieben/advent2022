using System.Diagnostics;
using System.Text.RegularExpressions;
using Essentials;

Shape rock = new("Rock", "A", "X", 1);
Shape paper = new("Paper", "B", "Y", 2);
Shape scissors = new("Scissors", "C", "Z", 3);
List<Shape> shapes = new() { rock, paper, scissors };

var beats = new Dictionary<Shape, Shape>()
{
    { rock, scissors },
    { paper, rock },
    { scissors, paper },
};


var input = File.ReadAllText("./data/input.txt");
var roundInputs = input.Split('\n').Select(line => (line?.Trim()) ?? string.Empty);
var scores = new List<int>();

var roundMatcher = new Regex(@"([ABC]) ([XYZ])");
foreach (var roundInput in roundInputs
        //.Take(50)
    )
{
    var match = roundMatcher.Match(roundInput);
    if (!match.Success)
    {
        Debug.WriteLine($"Invalid input line detected! - {roundInput}");
        continue;
    }

    var opponentChoice = match.GetGroupValue(1);
    var responseChoice = match.GetGroupValue(2);

    int score = 0;
    var opponentShape = shapes.Single(s => s.OpponentCode == opponentChoice);

    var responseShape = shapes.Single(s => s.ResponseCode == responseChoice);
    score += responseShape.ResponseScore;

    //Debug.Write($"Elf threw {opponentShape.Name}, I threw {responseShape.Name}. Result: ");
    if (beats[responseShape] == opponentShape)
    {
        score += 6;
        //Debug.WriteLine($"WIN - score: {score}");
    }
    else if (responseShape == opponentShape)
    {
        score += 3;
        //Debug.WriteLine($"DRAW - score: {score}");
    }
    else
    {
        //Debug.WriteLine($"LOSE - score: {score}");
    }

    scores.Add(score);
}

Debug.WriteLine($"Total score for {scores.Count} rounds is {scores.Sum()}");


readonly record struct Shape(string Name, string OpponentCode, string ResponseCode, int ResponseScore);
