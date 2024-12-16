using System;
using Microsoft.VisualBasic;

public class HelloDomino
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("|._.| Welcome to Dominos |._.|");
            Console.WriteLine(" Please select the approach from the list below");
            Console.Write(" 1.Brute Force \n 2.Brute Force with Dictionaries\n 3.Graph with DFS search \nPlease press enter after input \n");
            string inputChar = Console.ReadLine();
            var input = int.Parse(inputChar);
            if (input < 0)
            {
                Console.WriteLine("Invalid Input");
                Console.Read();
                return;
            }
            switch (input)
            {
                case 1: solveByBruteForce(); break;
                case 2: solveByDictionaries(); break;
                case 3: solveByGraphs(); break;
                default: Console.WriteLine("Not Implemented yet"); break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occured while running the app. Please try again.");
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine($"Debug Info: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

    }

    private static void solveByGraphs()
    {
        var input = getInput();
        if (!string.IsNullOrEmpty(input))
        {
            var dominos = parseInputToGraph(input);
            if (dominos == null)
            {
                Console.WriteLine("Unable to parse input, please try with valid input again");
            }
            else
            {
                //condition 1: Every node has an even degree
                bool isValidDomino = validateEvenDegrees(dominos);
                if (isValidDomino)
                {
                    //condition 2: Every node is connected to one single cycle
                    var cycle = verifyEulerianCycle(dominos);
                    if (cycle == null)
                    {
                        Console.WriteLine("Domino Circle is not possible for the given input.");
                    }
                    else
                    {
                        var pairs = createDominoPairs(cycle);
                        printPairs(pairs);
                    }

                }
            }
        }

    }

    private static int[,] createDominoPairs(List<int> eulerianCycle)
    {
        int[,] pairs = new int[eulerianCycle.Count, 2]; ;
        for (var i = 0; i < eulerianCycle.Count; i++)
        {
            var next = i == eulerianCycle.Count - 1 ? 0 : i + 1;
            pairs[i, 0] = eulerianCycle[i];
            pairs[i, 1] = eulerianCycle[next];
        }
        return pairs;
    }

    private static bool validateEvenDegrees(Dictionary<int, List<int>> dominos)
    {
        bool isValidDomino = true;
        foreach (var domino in dominos)
        {
            if (domino.Value.Count % 2 != 0)
            {
                isValidDomino = false;
                Console.WriteLine("Domino Circle is not possible for the given input.");
                break;
            }
        }

        return isValidDomino;
    }

    private static List<int> verifyEulerianCycle(Dictionary<int, List<int>> dominos)
    {
        int startNode = findStartingNode(dominos);
        if (startNode == -1)
        {
            return null;
        }
        Stack<int> stack = new Stack<int>();
        HashSet<int> visited = new HashSet<int>();
        List<int> dominoChain = new List<int>();

        stack.Push(startNode);

        while (stack.Count > 0)
        {
            int top = stack.Pop();
            if (!visited.Contains(top))
            {
                visited.Add(top);
                dominoChain.Add(top);
                foreach (var neighbor in dominos[top])
                {
                    if (!visited.Contains(neighbor))
                    {
                        stack.Push(neighbor);
                    }
                }
            }
        }
        foreach (var node in dominos)
        {
            if (node.Value.Count > 0 && !visited.Contains(node.Key))
                return null;
        }
        return dominoChain;
    }

    private static int findStartingNode(Dictionary<int, List<int>> dominos)
    {
        int startNode = -1;
        foreach (var node in dominos)
        {
            if (node.Value.Count > 0)
            {
                startNode = node.Key;
                break;
            }
        }

        return startNode;
    }

    private static Dictionary<int, List<int>> parseInputToGraph(string input)
    {
        string[] pairs = input.Split(' ');
        if (pairs.Length == 0)
        {
            return null;
        }
        Dictionary<int, List<int>> values = new Dictionary<int, List<int>>();

        for (int i = 0; i < pairs.Length; i++)
        {
            // getting the element between the pipes i.e. [2|3] -> 2,3
            string cleanPair = pairs[i].Trim('[', ']');

            string[] numbers = cleanPair.Split('|');

            //handling invalid input case
            if (numbers.Length < 2)
            {
                return null;
            }
            int key, adjacentVal;
            int.TryParse(numbers[0], out key);
            int.TryParse(numbers[1], out adjacentVal);
            if (key < 1 || adjacentVal < 1)
            {
                return null;
            }

            if (!values.ContainsKey(key))
            {
                values[key] = new List<int>();
            }
            if (!values.ContainsKey(adjacentVal))
            {
                values[adjacentVal] = new List<int>();
            }
            values[key].Add(adjacentVal);
            values[adjacentVal].Add(key);
        }
        return values;
    }

    private static void solveByDictionaries()
    {
        int[,] dominos;
        var input = getInput();
        if (!string.IsNullOrEmpty(input))
        {
            dominos = parseInputToMatrix(input);
            if (dominos == null)
            {
                Console.WriteLine("Unable to parse input, please try with valid input again");
            }
            else
            {
                Dictionary<int, int> frequencyDict = new Dictionary<int, int>();

                for (var i = 0; i < dominos.GetLength(0); i++)
                {
                    if (!frequencyDict.ContainsKey(dominos[i, 0]))
                    {
                        frequencyDict[dominos[i, 0]] = 0;
                    }
                    if (!frequencyDict.ContainsKey(dominos[i, 1]))
                    {
                        frequencyDict[dominos[i, 1]] = 0;
                    }
                    frequencyDict[dominos[i, 0]]++;
                    frequencyDict[dominos[i, 1]]++;
                }
                bool isValidDomino = true;
                foreach (var key in frequencyDict)
                {
                    if (key.Value % 2 != 0)
                    {
                        isValidDomino = false;
                        Console.WriteLine("Domino Circle is not possible for the given input.");
                        break;
                    }
                }
                if (isValidDomino)
                {
                    dominos = bruteForceApproach(dominos);
                    printPairs(dominos);
                }
            }
        }

    }

    private static void solveByBruteForce()
    {
        int[,] dominos;
        var input = getInput();
        if (!string.IsNullOrEmpty(input))
        {
            dominos = parseInputToMatrix(input);
            if (dominos == null)
            {
                Console.WriteLine("Unable to parse input, please try with valid input again");
            }
            else
            {
                dominos = bruteForceApproach(dominos);
                if (dominos == null)
                {
                    Console.WriteLine("Domino Circle is not possible for the given input.");
                }
                else
                {
                    printPairs(dominos);
                }
            }
        }

    }

    private static string getInput()
    {
        Console.WriteLine("Please enter the input string in this format: [1|2] [2|3] [3|1] ... and then press ENTER");
        string input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Input cannot be empty. Please try again.");
            return "";
        }
        else
        {
            return input;
        }

    }

    private static int[,] bruteForceApproach(int[,] pairs)
    {
        if (pairs == null || pairs.GetLength(0) == 0 || pairs.GetLength(0) == 1)
        {
            return null;
        }

        for (var i = 0; i < pairs.GetLength(0) - 1; i++)
        {
            int current = i;
            int next = i + 1;
            bool hasMatch = false;
            for (var j = i + 1; j < pairs.GetLength(0); j++)
            {
                //checking if current element matches either first or second element of next Domino
                hasMatch = findMatch(pairs, current, next, j);
                if (hasMatch)
                {
                    break;
                }

            }
            if (!hasMatch)
            {
                return null;
            }
        }
        return pairs;
    }

    private static bool findMatch(int[,] pairs, int current, int next, int j)
    {
        bool hasMatch = false;
        if (pairs[current, 1] == pairs[j, 0] || pairs[current, 1] == pairs[j, 1])
        {
            if (pairs[current, 1] == pairs[j, 1])
            {
                flipPairs(pairs, j);
            }
            if (j != next)
            {
                swapPairs(pairs, next, j);
            }
            if (j == pairs.GetLength(0) - 1)
            {
                // check if last element matches the first starting element
                if (pairs[0, 0] != pairs[j, 1])
                {
                    return false;
                }
            }
            hasMatch = true;
        }
        return hasMatch;
    }

    private static void swapPairs(int[,] pairs, int next, int j)
    {
        var tempFirst = pairs[j, 0];
        var tempSecond = pairs[j, 1];
        pairs[j, 0] = pairs[next, 0];
        pairs[j, 1] = pairs[next, 1];
        pairs[next, 0] = tempFirst;
        pairs[next, 1] = tempSecond;
    }

    private static void flipPairs(int[,] pairs, int j)
    {
        var temp = pairs[j, 0];
        pairs[j, 0] = pairs[j, 1];
        pairs[j, 1] = temp;
    }

    private static void printPairs(int[,] pairs)
    {
        Console.WriteLine("Domino Circle for the given input:");
        var size = pairs.GetLength(0);
        for (int i = 0; i < size; i++)
        {
            Console.Write("[" + pairs[i, 0] + "|" + pairs[i, 1] + "] ");
        }
        Console.WriteLine("");
    }

    public static int[,] parseInputToMatrix(string input)
    {
        try
        {
            string[] pairs = input.Split(' ');
            if (pairs.Length == 0)
            {
                return null;
            }
            int[,] values = new int[pairs.Length, 2];

            for (int i = 0; i < pairs.Length; i++)
            {
                // getting the element i.e. [2|3] -> 2,3
                string cleanPair = pairs[i].Trim('[', ']');

                string[] numbers = cleanPair.Split('|');
                //handling invalid input case
                if (numbers.Length < 2)
                {
                    return null;
                }
                int.TryParse(numbers[0], out values[i, 0]);
                int.TryParse(numbers[1], out values[i, 1]);
                //handling invalid input case
                if (values[i, 0] < 1 || values[i, 1] < 1)
                {
                    return null;
                }
            }
            return values;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occured while parsing input. Please try again.");
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine($"Debug Info: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
            return null;
        }

    }
}