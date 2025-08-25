namespace AdventOfCode2015
{
    internal class Day13_Original
    {
        public Dictionary<string, Person> people = new Dictionary<string, Person>();
        public Dictionary<UnorderedPair<string>, HappinessPair> HappinessPairs = new Dictionary<UnorderedPair<string>, HappinessPair>();
        public Day13_Original(bool newRules = false, bool isTest = false)
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

                    var key = new UnorderedPair<string>(personName, otherName);

                    //add people if they don't exist
                    if (!people.ContainsKey(personName))
                    {
                        people.Add(personName, new Person(personName));
                    }
                    if (!people.ContainsKey(otherName))
                    {
                        people.Add(otherName, new Person(otherName));
                    }


                    //make a happiness pair if it doesn't exist
                    if (!HappinessPairs.ContainsKey(key))
                    {
                        HappinessPairs.Add(key, new HappinessPair(people[personName], people[otherName]);
                    }

                    //set the happiness value for the correct person in the pair
                    HappinessPairs[key].SetPersonHappiness(people[personName], direction == "gain" ? value : -value);
                }
            }


        }

    }

    public class Person
    {
        public string Name;
        public Person(string name)
        {
            Name = name;
        }
    }

    public class HappinessPair
    {
        public Person A;
        public int AHappiness;
        public Person B;
        public int BHappiness;
        public int Total { get { return AHappiness + BHappiness; } }
        public HappinessPair(Person a, Person b)
        {
            A = a;
            B = b;
        }

        public void SetPersonHappiness(Person person, int happiness)
        {
            if (person == A)
            {
                AHappiness = happiness;
            }
            else if (person == B)
            {
                BHappiness = happiness;
            }
            else
            {
                throw new ArgumentException("Person not in pair");
            }
        }
    }
}
