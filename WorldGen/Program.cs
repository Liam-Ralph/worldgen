static class WorldGen{

    // VARIABLES
    static readonly Random random = new();
    static readonly List<int> tiles = [];
    static int width;
    static int height;
    static int islandSize;

    // MAIN METHOD
    static void Main(){

        // -INTRODUCTION-
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Welcome to WorldGen v1.2!");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("ENTER");
        Console.ForegroundColor = ConsoleColor.White;
        Console.ReadLine();
        Console.Clear();

        // -MAP GENERATION SETUP-
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("The suggested map size is 100-500 pixels wide,");
        Console.WriteLine("and half as tall, though larger maps are fine.");
        Console.Write("Map Width: ");
        Console.ForegroundColor = ConsoleColor.White;
        width = GetInt(2, 40_000);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("Map Height: ");
        Console.ForegroundColor = ConsoleColor.White;
        height = GetInt(1, 40_000);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Island size can range from -30 to 10, with 0");
        Console.WriteLine("being the default.");
        Console.Write("Island Size: ");
        Console.ForegroundColor = ConsoleColor.White;
        islandSize = GetInt(-30, 10);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Island number can be changed by multiplying by a");
        Console.WriteLine("given number. 1 is the default and has no");
        Console.WriteLine("effect.");
        Console.Write("Island Number Multiplier: ");
        Console.ForegroundColor = ConsoleColor.White;
        double islandNumMultiplier;
        while(true){
            try{
                string? input = Console.ReadLine();
                input ??= "";
                islandNumMultiplier = double.Parse(input);
                if(0 > islandNumMultiplier || islandNumMultiplier > 100){
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Answer must be between 0 and 100.");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
            }catch(FormatException){
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Answer must be a number.");
                Console.ForegroundColor = ConsoleColor.White;
                continue;
            }
            break;
        }

        // -MAP GENERATION-
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Map Generation...");
        Console.WriteLine();
        LoadingBar(0);
        Console.ForegroundColor = ConsoleColor.White;

        // --Generation Setup--
        for(int i = 0; i < width * height; i++){
            tiles.Add(-1);
        }
        LoadingBar(2);

        // --Point Generation--
        int points = width * height / 10_000;
        if(random.Next(100) + 1 <=
        (width * height - points * 10_000) / 100){
            points++;
        }
        points = (int) Math.Round(points * islandNumMultiplier);
        if(points == 0){
            points++;
        }
        for(int i = 0; i < points; i++){
            tiles[random.Next(width * height)] = 0;
        }
        LoadingBar(4);

        // --Expansions--
        // Mountain
        Expand(1, 0);
        LoadingBar(6);
        Expand(1, 1);
        LoadingBar(8);
        Expand(2, 2);
        LoadingBar(10);
        Expand(3, 3);
        LoadingBar(12);
        Expand(3, 4);
        LoadingBar(14);
        // Land
        Expand(2, 10);
        LoadingBar(16);
        Expand(3, 11);
        LoadingBar(18);
        Expand(10, 12);
        LoadingBar(20);
        Expand(3, 13);
        LoadingBar(22);
        Expand(2, 14);
        LoadingBar(24);
        // Water
        Expand(3, 20);
        LoadingBar(26);
        Expand(5, 21);
        LoadingBar(28);
        Expand(10, 22);
        LoadingBar(30);
        Expand(20, 23);
        LoadingBar(32);
        for(int i = 0; i < width * height; i++){
            if(tiles[i] == -1){
                tiles[i] = 24;
            }
        }
        LoadingBar(34);

        // --Border Smoothing--
        List<int> colors = new(){
            0, 1, 2, 3, 4, 10, 11, 12, 13, 14, 20, 21, 22, 23, 24
        };
        List<int> highestList;
        int highest;
        for(int i = 0; i < width * height; i++){
            if(CheckAdjacentExact(i, tiles[i]) == 0 ||
            (CheckAdjacentExact(i, tiles[i]) == 1 && random.Next(4) == 1)){
                highestList = new();
                highest = 1;
                foreach(int color in colors){
                    if(CheckAdjacentExact(i, color) == highest){
                        highestList.Add(color);
                    }else if(CheckAdjacentExact(i, color) > highest){
                        highestList = new List<int>{color};
                    }
                }
                tiles[i] = highestList[random.Next(highestList.Count)];
            }
        }
        LoadingBar(36);

        // --Map Generation Complete--
        ClearLine(2, "Map Generation Complete");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("It is recommended that you zoom out as far as");
        Console.WriteLine("possible now, as you can zoom in later without");
        Console.WriteLine("ruining the map, but not being zoomed out enough");
        Console.WriteLine("will ruin the map.");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("ENTER");
        Console.ForegroundColor = ConsoleColor.White;
        Console.ReadLine();
        Console.Clear();
        
        // -MAP PRINTING-
        for(int y = 0; y < Math.Ceiling((double) height / 2); y++){
            for(int x = 0; x < width; x++){
                int i = y * 2 * width + x;
                int ii = (y * 2 + 1) * width + x;
                int color1 = 0;
                int color2 = 0;
                try{
                    switch(tiles[i]){
                        case 0:
                            color1 = 255;
                            break;
                        case 1:
                            color1 = 246;
                            break;
                        case 2:
                            color1 = 242;
                            break;
                        case 3:
                            color1 = 238;
                            break;
                        case 4:
                            color1 = 234;
                            break;
                        case 10:
                            color1 = 22;
                            break;
                        case 11:
                            color1 = 28;
                            break;
                        case 12:
                            color1 = 34;
                            break;
                        case 13:
                            color1 = 40;
                            break;
                        case 14:
                            color1 = 215;
                            break;
                        case 20:
                            color1 = 21;
                            break;
                        case 21:
                            color1 = 20;
                            break;
                        case 22:
                            color1 = 19;
                            break;
                        case 23:
                            color1 = 18;
                            break;
                        case 24:
                            color1 = 17;
                            break;
                    }
                }catch(Exception){}
                try{
                    switch(tiles[ii]){
                        case 0:
                            color2 = 255;
                            break;
                        case 1:
                            color2 = 246;
                            break;
                        case 2:
                            color2 = 242;
                            break;
                        case 3:
                            color2 = 238;
                            break;
                        case 4:
                            color2 = 234;
                            break;
                        case 10:
                            color2 = 22;
                            break;
                        case 11:
                            color2 = 28;
                            break;
                        case 12:
                            color2 = 34;
                            break;
                        case 13:
                            color2 = 40;
                            break;
                        case 14:
                            color2 = 215;
                            break;
                        case 20:
                            color2 = 21;
                            break;
                        case 21:
                            color2 = 20;
                            break;
                        case 22:
                            color2 = 19;
                            break;
                        case 23:
                            color2 = 18;
                            break;
                        case 24:
                            color2 = 17;
                            break;
                    }
                }catch(Exception){}
                if(color1 == color2){
                    Console.Write("\u001b[48;5;" + color1 + "m ");
                }else{
                    Console.Write("\u001b[48;5;" + color1 + "m\u001b[38;5;" +
                        color2 + "m▄");
                }
            }
            Console.WriteLine("\u001b[0m");
        }
    }

    // OTHER METHODS

    /// <summary>
    /// Counts the number of adjacent tiles equal to or
    /// below in value (which is higher in elevation)
    /// when compared to a passed value.
    /// </summary>
    /// <param name="tile">the tile to check</param>
    /// <param name="type">the maximum tile value to
    /// include in the total count</param>
    /// <returns>the number of adjacent tiles equal to or
    /// below in value to type</returns>
    static int CheckAdjacent(int tile, int type){
        int num = 0;
        try{
            if(0 <= tiles[tile - 1] && tiles[tile - 1] <= type){
                num++;
            }
        }catch(ArgumentOutOfRangeException){}
        try{
            if(0 <= tiles[tile + 1] && tiles[tile + 1] <= type){
                num++;
            }
        }catch(ArgumentOutOfRangeException){}
        try{
            if(0 <= tiles[tile - width] && tiles[tile - width] <= type){
                num++;
            }
        }catch(ArgumentOutOfRangeException){}
        try{
            if(0 <= tiles[tile + width] && tiles[tile + width] <= type){
                num++;
            }
        }catch(ArgumentOutOfRangeException){}
        return num;
    }

    /// <summary>
    /// Checks the number of adjacent tiles equal to a
    /// passed value.
    /// </summary>
    /// <param name="tile">the tile to check</param>
    /// <param name="type">the value to check for</param>
    /// <returns>the number of adjacent tiles equal in
    /// value to type</returns>
    static int CheckAdjacentExact(int tile, int type){
        int num = 0;
        try{
            if(tiles[tile - 1] == type){
                num++;
            }
        }catch(ArgumentOutOfRangeException){}
        try{
            if(tiles[tile + 1] == type){
                num++;
            }
        }catch(ArgumentOutOfRangeException){}
        try{
            if(tiles[tile - width] == type){
                num++;
            }
        }catch(ArgumentOutOfRangeException){}
        try{
            if(tiles[tile + width] == type){
                num++;
            }
        }catch(ArgumentOutOfRangeException){}
        return num;
    }

    /// <summary>
    /// Clears and replaces a line.
    /// </summary>
    /// <param name="num">number of lines backward to go
    /// (e.g. 1 would mean the last line printed, while
    /// 2 would mean the second last)</param>
    /// <param name="line">the line of text to replace
    /// the cleared one with</param>
    static void ClearLine(int num, string line){
        int currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop - num);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(line);
        Console.SetCursorPosition(0, currentLineCursor);
        Console.ForegroundColor = ConsoleColor.White;
    }

    static int Coord(int x, int y){
        return y * width + x;
    }

    /// <summary>
    /// Expands part of the map with a certain color.
    /// </summary>
    /// <param name="rounds">the number of expansions
    /// to perform</param>
    /// <param name="expandingColor">
    /// the color being expanded</param>
    static void Expand(int rounds, int expandingColor){
        for(int i = 0; i < rounds; i++){
            foreach(int x in RandomList("x")){
                foreach(int y in RandomList("y")){
                    int num = CheckAdjacent(Coord(x, y), expandingColor);
                    if(num != 0 && tiles[Coord(x, y)] == -1 && random
                    .Next(100) + 1 <= num * 5 +
                    (50 + islandSize - i * ((70 + islandSize) / rounds))){
                        tiles[Coord(x, y)] = expandingColor;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Gets an integer input value between min and max
    /// (both inclusive).
    /// </summary>
    /// <param name="min">minimum integer value</param>
    /// <param name="max">maximum integer value</param>
    /// <returns>an integer input value</returns>
    static int GetInt(int min, int max){
        int value;
        while(true){
            try{
                #pragma warning disable CS8604
                value = int.Parse(Console.ReadLine());
                if(min > value || value > max){
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Answer must be between "
                        + min + " and " + max + ".");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
            }catch(FormatException){
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Answer must be an integer.");
                Console.ForegroundColor = ConsoleColor.White;
                continue;
            }
            break;
        }
        return value;
    }

    /// <summary>
    /// A loading bar. Creates a green and red loading bar
    /// to show how far a task has progressed.
    /// </summary>
    /// <param name="count">The amount of the bar
    /// that is green/completed.</param>
    static void LoadingBar(int count){
        int currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(new string('█', count));
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(new string('█', 36 - count));
        Console.SetCursorPosition(0, currentLineCursor);
        Console.ForegroundColor = ConsoleColor.White;
    }

    /// <summary>
    /// Creates a list of integers in a random order.
    /// Used to go through every possible x and y coordinate
    /// in random order.
    /// </summary>
    /// <param name="maxType">Sets list length to map
    /// height or width.</param>
    /// <returns>A list of randomly-ordered integers.</returns>
    static List<int> RandomList(string maxType){
        List<int> randomList = new();
        int max;
        if(maxType == "x"){
            max = width;
        }else{
            max = height;
        }
        if(random.Next(2) == 1){
            for(int i  = 0; i < max; i++){
                randomList.Add(i);
            }
        }else{
            for(int i  = max - 1; i >= 0; i--){
                randomList.Add(i);
            }
        }
        return randomList;
    }
}
