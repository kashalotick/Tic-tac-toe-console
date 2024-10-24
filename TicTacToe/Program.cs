namespace TicTacToe;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        
        Console.WriteLine("Tic Tac Toe\n" +
                          "\nControls (set English):" +
                          "\n wasd - select cell" +
                          "\n h - hide selection (press any key to show)" +
                          "\n e - place figure" +
                          "\n q - quit" +
                          "\n\nPress anu key to continue...");
        Console.ReadKey();
        
        start:
        
        // small part of code made for future if I will decide to make Player vs AI
        
        Console.CursorVisible = false;
        //Console.WriteLine("Circle or Cross?");
        string choice;
        //string input = Console.ReadLine();
        string input = "cross";
        int y = 0;
        int x = 0;
        bool hide = false;
        int actCount = 0;
        
        switch (input)
        {
            case "circle":
            case "1":
                choice = "circle";
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            case "cross":
            case "2":
                choice = "cross";
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            default:
                Console.WriteLine("Invalid input");
                goto start;
        }
        // 3d array
        // 1st matrix-layer has positions of figures
        // 2nd matrix-layer has position of current selection
        int[,,] field = new int[3, 3, 2];
        field[y, x, 1] = 3;
        
        // game 
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"You are {choice}!");
            Console.WriteLine(MergeMatrixLayers(field));
            
            int win = WinCheck(field);
            if (win == 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cross is a winner");
                break;
            }
            if (win == 2)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Circle is a winner");
                break;
            }
            if (actCount == 9)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Draw");
                break;
            }
            bool quit = false;
            var action = Console.ReadKey();
            field[y, x, 1] = 0;
            
            // controls 
            switch (action.KeyChar)
            {
                // quit
                case 'q':
                    Console.SetCursorPosition(0, 4);
                    Console.WriteLine("Sure to quit? (q to quit)");
                    
                    Console.CursorVisible = true;
                    string sureQuit = Console.ReadLine();
                    Console.CursorVisible = false;
                    
                    if (sureQuit == "q")
                        quit = true;
                    break;
                case 'w':
                    y--;
                    break;
                case 'a':
                    x--;
                    break;
                case 's':
                    y++;
                    break;
                case 'd':
                    x++;
                    break;
                case 'h':
                    if (!hide)
                    {
                        hide = true;
                        continue;
                    }
                        

                    break;
                // placement
                case 'e':
                    Console.WriteLine();
                    if (field[y, x, 0] != 2 && field[y, x, 0] != 1 && hide == false)
                    {
                        if (choice == "cross")
                        {
                            field[y, x, 0] = 1;
                            choice = "circle";
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        else
                        {
                            field[y, x, 0] = 2;
                            choice = "cross";
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        actCount++;
                        hide = true;
                        continue;
                    }
                    break;
            }
            // out of bounds check
            hide = false;
            if (x < 0)
                x = 2;
            else if (x > 2)
                x = 0;
            else if (y < 0)
                y = 2;
            else if (y > 2)
                y = 0;
            if (field[y, x, 0] == 0)
                field[y, x, 1] = 3;
            else
                field[y, x, 1] = 4;
            
            Console.WriteLine();
            if (quit)
            {
                break;
            }
        }
        // ask for rematch
        reansw:
        Console.CursorVisible = true;
        
        Console.WriteLine("Want to rematch?" +
                          "\nyes - rematch" +
                          "\nno - quit");
        string rematchAnswer = Console.ReadLine();
        
        Console.CursorVisible = false;
        
        switch (rematchAnswer)
        {
            case "y":
            case "yes":
                goto start;
            case "n":
            case "no":
            case "not":
                break;
            default:
                Console.Clear();
                Console.WriteLine("Invalid input");
                goto reansw;
        }
    }
    // algorithm for c if there are 3 in row/col/diagonal, just check for win
    static int WinCheck(int[,,] matrix)
    {
        int win = 0;
        int k = 0;
        int[] arr = new int[24];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                arr[k] = matrix[i, j, 0];
                k++;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                arr[k] = matrix[j, i, 0];
                k++;
            }
        }
        int ii = 0, jj = 0;
        for (k = k; k < 21; k++)
        {
            arr[k] = matrix[ii, jj, 0];
            ii++;
            jj++;
        }

        ii = 2;
        jj = 0;
        for (k = k; k < 24; k++)
        {
            arr[k] = matrix[ii, jj, 0];
            ii--;
            jj++;
        }
        
        for (int i = 0; i < arr.Length; i += 3)
        {
            // Console.WriteLine($"({arr[i]}, {arr[i + 1]}, {arr[i + 2]})");
            if (arr[i] == arr[i + 1] && arr[i] == arr[i + 2] && arr[i] != 0)
            {
                win = arr[i];
                break;
            }
        }
        
        return win;
    }
    
    // merge matrix-layers
    static string MergeMatrixLayers(int[,,] matrix)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int element = matrix[i, j, 1];
                if (element == 3 || element == 4)
                    matrix[i, j, 1] = matrix[i, j, 1];
                else
                    matrix[i, j, 1] = matrix[i, j, 0];
            }
        }
        return PrintMatrix(matrix);
    }
    
    // Visualisation of field
    static string PrintMatrix(int[,,] matrix)
    {
        string result = "";
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // ⎕ █ ✕ ⊗ ◯ ●
                int element = matrix[i, j, 1];
                string interpratate = "";
                if (element == 0)
                    interpratate = "\u22c5";
                else if (element == 1)
                    interpratate = "\u2715";
                else if (element == 2)
                    interpratate = "\u25ef";
                else if (element == 3)
                    interpratate = "\u2588";
                else if (element == 4)
                    interpratate = "\u2395";
                
                result += interpratate + " ";
            }
            result += "\n";
        }

        result = result.TrimEnd();
        return result;
    }
}