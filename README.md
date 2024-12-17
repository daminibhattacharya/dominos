# Dominos

Solving dominos using brute force, dictionary, and Eulerian Cycle with DFS in .NET 9.

---

## Prerequisites

- **.NET SDK 9.0 or higher**
- **VS Code** or any compatible IDE

---

## Project Structure
```
Dominos/
├── Dominos.sln 
├── Dominos/
│ ├── Dominos.csproj 
│ ├── Program.cs # Main program file
│ └── obj/
├── Dominos.Tests/
│ ├── Dominos.Tests.csproj 
│ ├── DominoTest.cs # Main test file
│ └── obj/
```
---
## How to Run
- Clone the Repository:
```
git clone https://github.com/daminibhattacharya/dominos.git
cd dominos
```
- Build the Project:
```
dotnet build 
```
Run the Program:
```
dotnet run --project Dominos/Dominos.csproj
```
Run the Tests:
```
dotnet test
```
---
## Input Format

- Enter dominos in the format `[a|b] [c|d] ...` where each pair represents a domino.
- Example inputs :
  1. [1|2] [2|3] [3|1]
  2. [2|7] [9|6] [6|5] [7|9] [5|2]
  3. [2|3] [3|4] [4|5] [5|3] [3|7] [7|2]
  4. [25|26] [60|61] [20|21] [15|16] [61|62] [62|63] [65|66] [78|79] [87|88] [29|30] [39|40] [90|91] [99|100] [6|7] [66|67] [45|46] [97|98] [19|20] [74|75] [79|80] [28|29] [83|84] [84|85] [68|69] [77|78] [38|39] [42|43] [46|47] [53|54] [3|4] [43|44] [93|94] [4|5] [59|60] [32|33] [100|1] [23|24] [69|70] [8|9] [2|3] [80|81] [47|48] [55|56] [7|8] [96|97] [73|74] [22|23] [67|68] [72|73] [27|28] [10|11] [92|93] [30|31] [56|57] [52|53] [98|99] [89|90] [64|65] [1|2] [17|18] [88|89] [76|77] [33|34] [13|14] [85|86] [70|71] [81|82] [14|15] [41|42] [54|55] [58|59] [48|49] [26|27] [82|83] [31|32] [75|76] [95|96] [57|58] [63|64] [50|51] [9|10] [11|12] [35|36] [94|95] [24|25] [36|37] [71|72] [21|22] [18|19] [44|45] [37|38] [40|41] [34|35] [51|52] [16|17] [5|6] [49|50] [12|13] [86|87] [91|92]
  5. [2|7] [9|6] [6|5] [7|9] [9|8] [5|1]
## Sample Output
```
|._.| Welcome to Dominos |._.|
 Please select the approach from the list below
 1.Brute Force 
 2.Brute Force with Dictionaries
 3.Graph with DFS search 
Please press enter after input 
1
Please enter the input string in this format: [1|2] [2|3] [3|1] ... and then press ENTER
[2|7] [9|6] [6|5] [7|9] [9|8] [5|1]
Domino Circle is not possible for the given input.
```
### Sample Random Input Generator
- To generate a random input, just run the following C# code and copy the output by providing your length in the argument in the code, as mentioned in the comment
```
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<string> dominos = new List<string>();
        var length = 100;// Provide Length of input here
        for (int i = 1; i <= length; i++)
        {
            if(i!=length){
                dominos.Add($"[{i}|{i + 1}]");
            }else{
                dominos.Add($"[{i}|1]");
            }
            
            
        }

        Random random = new Random();
        for (int i = dominos.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            string temp = dominos[i];
            dominos[i] = dominos[j];
            dominos[j] = temp;
        }
        Console.Write(string.Join(" ", dominos));
    }
}
```
