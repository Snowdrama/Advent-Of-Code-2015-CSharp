namespace AdventOfCode2015
{
    internal class Day2
    {
        public Day2(bool findRibbon = false, bool isTest = false)
        {
            string[] input;
            if (isTest)
            {
                input = System.IO.File.ReadAllLines("Data/Day2/day2_test.txt"); ;
            }
            else
            {
                input = System.IO.File.ReadAllLines("Data/Day2/day2.txt");
            }
            Console.WriteLine($"Number of Presents: {input.Length}");
            double totalSquareFeet = 0;
            double totalRibbonLength = 0;
            foreach (string line in input)
            {
                //split into the number of feet by splitting by x
                string[] dimensions = line.Split('x');

                double l = 0.0f;
                double w = 0.0f;
                double h = 0.0f;
                bool canParse = dimensions.Length == 3 &&
                    double.TryParse(dimensions[0], out l) &&
                    double.TryParse(dimensions[1], out w) &&
                    double.TryParse(dimensions[2], out h);

                if (canParse)
                {
                    //Area of each side
                    double lw = l * w;
                    double wh = w * h;
                    double hl = h * l;
                    double totalPresentArea = 2 * (lw + wh + hl);
                    double smallestSide = Math.Min(lw, Math.Min(wh, hl));


                    // Calculate the total square feet needed
                    double finalSize = totalPresentArea + smallestSide;
                    totalSquareFeet += finalSize;
                    Console.WriteLine($"Present sized: {line} needs: {finalSize} total square feet of wrappiing paper");

                    if (findRibbon)
                    {
                        double[] sides = new double[] { l, w, h };

                        //sort in order of size
                        sides = sides.OrderBy(x => x).ToArray();

                        //we can use 0 and 1 to get the smallest sides
                        double ribbonLength = 2 * (sides[0] + sides[1]) + (l * w * h);

                        Console.WriteLine($"Present Sized: {line} needs: {ribbonLength} total feet of ribbon.");
                        totalRibbonLength += ribbonLength;
                    }
                }
            }
            Console.WriteLine($"Total square feet of wrapping paper needed for all presents is: {totalSquareFeet}");
            if (findRibbon)
            {
                Console.WriteLine($"Total square feet of ribbon needed for all presents is: {totalRibbonLength}");
            }
        }
    }
}
