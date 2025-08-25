namespace AdventOfCode2015
{

    public class Day13
    {
        public Day13(bool newRules = false, bool isTest = false)
        {
            string[] input;
            if (isTest)
            {
                input = File.ReadAllLines("Data/Day13/day13_test.txt"); ;
            }
            else
            {
                input = File.ReadAllLines("Data/Day13/day13.txt");
            }

            //parse input
            Dictionary<string, Dictionary<string, int>> peopleScores = new Dictionary<string, Dictionary<string, int>>();
            for (int i = 0; i < input.Length; i++)
            {
                if (ParseHelper.TryParse(
                    input[i],
                    "{name} would {direction} {value} happiness units by sitting next to {otherName}.",
                    out Dictionary<string, string> output)
                )
                {
                    var personName = output["name"];
                    var otherName = output["otherName"];

                    var direction = output["direction"];
                    var value = int.Parse(output["value"]);

                    if (!peopleScores.ContainsKey(personName))
                    {
                        peopleScores.Add(personName, new Dictionary<string, int>());
                    }
                    if (!peopleScores[personName].ContainsKey(otherName))
                    {
                        switch (direction)
                        {
                            case "gain":
                                peopleScores[personName].Add(otherName, value);
                                break;
                            case "lose":
                                peopleScores[personName].Add(otherName, -value);
                                break;
                        }
                    }
                }
            }


            //Part2: add you into the peopleScores with 0 happiness for everyone
            if (newRules)
            {
                peopleScores.Add("You", new Dictionary<string, int>());
                foreach (var person in peopleScores.Keys)
                {
                    if (person != "You")
                    {
                        peopleScores["You"].Add(person, 0);
                        peopleScores[person].Add("You", 0);
                    }
                }
            }

            //get all permutations of seating arrangements

            var allNames = peopleScores.Keys.GetPermutations(peopleScores.Keys.Count()).ToList();

            //rank the highest permutation
            int highestPermutation = -1;
            string[]? highestArrangement = null;
            int highestScore = int.MinValue;
            for (int i = 0; i < allNames.Count; i++)
            {
                int score = 0;
                var permutation = allNames[i].ToArray();
                //add in each pairing to the score
                for (int c = 0; c < permutation.Length; c++)
                {

                    var person = permutation[c];
                    var neighbor = permutation[(c + 1) % permutation.Length];
                    score += peopleScores[person][neighbor];
                    score += peopleScores[neighbor][person];
                }

                //if it's higher than the current high score
                //set a new high score
                if (score > highestScore)
                {
                    highestScore = score;
                    highestPermutation = i;
                    highestArrangement = permutation;
                }
            }

            ConsoleEx.Cyan($"Best Permutation: {highestPermutation}");
            for (int c = 0; c < highestArrangement.Length; c++)
            {
                ConsoleEx.Blue($"{highestArrangement[c]} ", false);
            }
            ConsoleEx.Green($" has a total happiness of {highestScore}");
        }
    }

}