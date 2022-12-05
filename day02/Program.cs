using System.Diagnostics;
using System.Text.RegularExpressions;
using Essentials;

string loseCode = "X";
string drawCode = "Y";
string winCode = "Z";

Shape rock = new("Rock", "A", "X", 1);
Shape paper = new("Paper", "B", "Y", 2);
Shape scissors = new("Scissors", "C", "Z", 3);
List<Shape> shapes = new() { rock, paper, scissors };

var roundMatcher = new Regex(@"([ABC]) ([XYZ])");
var input = File.ReadAllText("./data/input.txt");
var roundInputs = input.Split('\n').Select(line => (line?.Trim()) ?? string.Empty)
    //.Take(50)
    ;

Debug.WriteLine("--------- PART ONE ---------");
ProcessPart1(roundInputs);

Debug.WriteLine("--------- PART TWO ---------");
ProcessPart2(roundInputs);

void ProcessPart1(IEnumerable<string> roundInputs)
{
    var beats = new Dictionary<Shape, Shape>()
    {
        { rock, scissors },
        { paper, rock },
        { scissors, paper },
    };

    var scores = new List<int>();
    foreach (var roundInput in roundInputs)
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
}

void ProcessPart2(IEnumerable<string> roundInputs)
{
    var beats = new List<(Shape, Shape)>()
    {
        ( rock, scissors ),
        ( paper, rock ),
        ( scissors, paper ),
    };

    var scores = new List<int>();

    foreach (var roundInput in roundInputs)
    {
        var match = roundMatcher.Match(roundInput);
        if (!match.Success)
        {
            Debug.WriteLine($"Invalid input line detected! - {roundInput}");
            continue;
        }

        var opponentChoice = match.GetGroupValue(1);
        var resultCode = match.GetGroupValue(2);

        int score = 0;
        var opponentShape = shapes.Single(s => s.OpponentCode == opponentChoice);

        Shape responseShape;
        if (resultCode == winCode)
        {
            responseShape = beats.Single(b => b.Item2 == opponentShape).Item1;
        }
        else if (resultCode == loseCode)
        {
            responseShape = beats.Single(b => b.Item1 == opponentShape).Item2;
        }
        else
        {
            responseShape = opponentShape;
        }
        score += responseShape.ResponseScore;

        Debug.Write($"Elf threw {opponentShape.Name}, I threw {responseShape.Name}. Result: ");

        if (resultCode == winCode)
        {
            score += 6;
            Debug.WriteLine($"WIN - score: {score}");
        }
        else if (resultCode == drawCode)
        {
            score += 3;
            Debug.WriteLine($"DRAW - score: {score}");
        }
        else
        {
            Debug.WriteLine($"LOSE - score: {score}");
        }

        scores.Add(score);
    }

    Debug.WriteLine($"Total score for {scores.Count} rounds is {scores.Sum()}");
}

readonly record struct Shape(string Name, string OpponentCode, string ResponseCode, int ResponseScore);
