namespace AdventOfCode2015
{

    class Day3
    {
        enum CurrentSanta
        {
            Santa,
            RoboSanta
        }
        Dictionary<Vector2I, int> presentLocations = new Dictionary<Vector2I, int>();
        Vector2I currentPosition = new Vector2I(0, 0);
        public Day3(bool useRoboSanta, bool isTest = false)
        {
            string input;
            if (isTest)
            {
                input = System.IO.File.ReadAllText("Data/Day3/day3_test.txt"); ;
            }
            else
            {
                input = System.IO.File.ReadAllText("Data/Day3/day3.txt");
            }
            Console.WriteLine($"Number of Moves: {input.Length}");

            Day3Santa santa = new Day3Santa();
            Day3Santa santaRobo = new Day3Santa();
            CurrentSanta currentSanta = CurrentSanta.Santa;

            //we deposit a present at the starting position
            santa.DeliverPresent(ref presentLocations);
            if (useRoboSanta)
            {
                santaRobo.DeliverPresent(ref presentLocations);
            }

            //switch between Santa and RoboSanta and move them based on the input
            for (int i = 0; i < input.Length; i++)
            {
                switch (currentSanta)
                {
                    case CurrentSanta.Santa:
                        santa.MoveAndDeliver(input[i], ref presentLocations);
                        if (useRoboSanta)
                        {
                            currentSanta = CurrentSanta.RoboSanta; // switch to RoboSanta
                        }
                        break;
                    case CurrentSanta.RoboSanta:
                        if (useRoboSanta)
                        {
                            santaRobo.MoveAndDeliver(input[i], ref presentLocations);
                            currentSanta = CurrentSanta.Santa; // switch back to Santa
                        }
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine($"Total unique houses visited: {presentLocations.Count}");
        }
    }
    class Day3Santa
    {
        Vector2I Position = new Vector2I(0, 0);
        public void MoveAndDeliver(char inputChar, ref Dictionary<Vector2I, int> presentLocations)
        {
            if (inputChar == '^')
            {
                Position.Y++;
            }
            else if (inputChar == 'v')
            {
                Position.Y--;
            }
            else if (inputChar == '>')
            {
                Position.X++;
            }
            else if (inputChar == '<')
            {
                Position.X--;
            }
            DeliverPresent(ref presentLocations);
        }

        public void DeliverPresent(ref Dictionary<Vector2I, int> presentLocations)
        {
            if (!presentLocations.ContainsKey(Position))
            {
                Console.WriteLine($"New House! {Position}");
                presentLocations.Add(Position, 0);
            }
            presentLocations[Position]++;
        }
    }

}
