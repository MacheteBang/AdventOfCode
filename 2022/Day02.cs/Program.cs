var lines = System.IO.File.ReadAllLines("input.txt").ToList();

// First Index = Opponent, Second Index = Me
int[,] mapToTool = { { 4, 8, 3 }, { 1, 5, 9 }, { 7, 2, 6 } };
int[,] mapToResult = { { 3, 4, 8 }, { 1, 5, 9 }, { 2, 6, 7 } };



Console.WriteLine(lines.Sum(l => CalcScoreToTool(l)));
Console.WriteLine(lines.Sum(l => CalcScoreToResult(l)));

/////
int CalcScoreToTool(string match) => mapToTool[
        ToIndex(match.Split(" ")[0].Trim()[0]),
        ToIndex(match.Split(" ")[1].Trim()[0])
    ];

int CalcScoreToResult(string match) => mapToResult[
        ToIndex(match.Split(" ")[0].Trim()[0]),
        ToIndex(match.Split(" ")[1].Trim()[0])
    ];

int ToIndex(char action) => action switch
{
    'A' or 'X' => 0,
    'B' or 'Y' => 1,
    'C' or 'Z' => 2,
    _ => -1
};