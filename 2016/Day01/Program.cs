string filePath = "./input-test.txt";
string[] instructions = File.ReadAllText(filePath).Split(", ");
Direction direction = Direction.North;
Coordinate location = new();

for (int i = 0; i < instructions.Length; i++)
{
    direction = Turn(direction, instructions[i][0]);
    location = Move(location, direction, int.Parse(instructions[i][1].ToString()));
}



Console.WriteLine("End of Program");

Direction Turn(Direction currentDirection, char way) => way switch
{
    'R' => currentDirection switch
    {
        Direction.North => Direction.East,
        Direction.East => Direction.South,
        Direction.South => Direction.West,
        /*West*/
        _ => Direction.North
    },
    /*'L'*/
    _ => direction switch
    {
        Direction.North => Direction.West,
        Direction.East => Direction.North,
        Direction.South => Direction.East,
        /*West*/
        _ => Direction.South
    }
};
Coordinate Move(Coordinate currentLocation, Direction currentDirection, int distance) => currentDirection switch
{
    Direction.North => new Coordinate { X = currentLocation.X, Y = currentLocation.Y - distance },
    Direction.East => new Coordinate { X = currentLocation.X + distance, Y = currentLocation.Y },
    Direction.South => new Coordinate { X = currentLocation.X, Y = currentLocation.Y + distance },
    /*West*/
    _ => new Coordinate { X = currentLocation.X - distance, Y = currentLocation.Y },
};