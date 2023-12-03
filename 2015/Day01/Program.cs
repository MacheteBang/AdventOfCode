string instructions = File.ReadAllText("./input.txt");

int currentFloor = 0;
int positionAtBasement = 0;

for (int i = 0; i < instructions.Length; i++)
{
    if (instructions[i] == '(') currentFloor++;
    if (instructions[i] == ')') currentFloor--;

    if (positionAtBasement == 0 && currentFloor < 0)
    {
        positionAtBasement = i + 1;
    }
}

Console.WriteLine($"Floor is:{currentFloor}");
Console.WriteLine($"Position at Basement:{positionAtBasement}");