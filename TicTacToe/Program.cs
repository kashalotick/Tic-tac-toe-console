namespace TicTacToe;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Tic-");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("tac-");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("toe\n");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("" +
                          "\nControls (set English language):" +
                          "\n   wasd - select cell" +
                          "\n   e - place figure" +
                          "\n   q - quit" +
                          "\n\nPress any key to continue...");
        Console.ReadKey();
        
        start:
        Console.ForegroundColor = ConsoleColor.Red;
        
        // basic variables 
        int y = 0;
        int x = 0;
        int actCount = 0;
        string choice = "cross";
        int[,] field = new int[3, 3];
        
        // winner variables
        ConsoleColor color = ConsoleColor.White;
        string endGameText = "Draw";
        

        
        // game 
        while (true)
        {
            Console.Clear();
            
            // win check
            var response = WinCheck(field);
            int win = response.win;
            
            switch (win)
            {
                case 1:
                    color = ConsoleColor.Red;
                    endGameText = "Cross is a winner!";
                    break;
                case 2:
                    color = ConsoleColor.Blue;
                    endGameText = "Circle is a winner!";
                    break;
                default:
                    if (actCount == 9)
                    {
                        win = 3;
                        color = ConsoleColor.White;
                        endGameText = "Draw!";
                    }
                    break;
            }
            if (win != 0)
            {
                //Console.WriteLine(winCheckResult);
                if (win != 3)
                {
                    field[response.pos1.y, response.pos1.x] = win + 2;
                    field[response.pos2.y, response.pos2.x] = win + 2;
                    field[response.pos3.y, response.pos3.x] = win + 2;
                }
                Console.ForegroundColor = color;
                Console.WriteLine($"End game!");
                Console.WriteLine(PrintMatrix(field));
                Console.SetCursorPosition(0, 4);
                Console.WriteLine(endGameText);
                Console.WriteLine();
                break;
            }
            // filed print
            Console.WriteLine($"You are {choice}!");
            Console.WriteLine(PrintMatrix(field));
            if (x == 0)
            {
                Console.SetCursorPosition(x, y + 1);
            }
            else
            {
                Console.SetCursorPosition(2*x, y + 1);
            }

            

            
            bool quit = false;
            var action = Console.ReadKey();
            
            // controls 
            switch (action.KeyChar)
            {
                // quit
                case 'q':
                    Console.SetCursorPosition(0, 4);
                    Console.WriteLine("Sure to quit? (q to quit)");
                    
                    string sureQuit = Console.ReadLine();
                    
                    if (sureQuit == "q")
                        quit = true;
                    break;
                // moving
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
                // placement
                case 'e':
                    Console.WriteLine();
                    if (field[y, x] != 2 && field[y, x] != 1)
                    {
                        if (choice == "cross")
                        {
                            field[y, x] = 1;
                            choice = "circle";
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        else
                        {
                            field[y, x] = 2;
                            choice = "cross";
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        actCount++;
                        continue;
                    }
                    break;
            }
            // out of bounds check
            if (x < 0)
                x = 2;
            else if (x > 2)
                x = 0;
            else if (y < 0)
                y = 2;
            else if (y > 2)
                y = 0;
            
            Console.WriteLine();
            if (quit)
            {
                break;
            }
        }
        // ask for rematch
        Console.ForegroundColor = ConsoleColor.White;
        reansw:
        Console.WriteLine("Want to rematch?" +
                          "\nyes - rematch" +
                          "\nno - quit");
        string rematchAnswer = Console.ReadLine();
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
    
    // algorithm of checking if there are 3 in line
    static ((int y, int x) pos1, (int y, int x) pos2, (int y, int x) pos3, int win) WinCheck(int[,] matrix) 
    {
        int win = 0;
        // rows check
        for (int i = 0; i < 3; i++)
        {
            if (matrix[i, 0] == matrix[i, 1] && matrix[i, 0] == matrix[i, 2] &&
                matrix[i, 0] != 0)
            {
                win = matrix[i, 0];
                return ((i, 0), (i, 1), (i, 2), win);
            }
        }
        // columns check
        for (int i = 0; i < 3; i++)
        {
            if (matrix[0, i] == matrix[1, i] && matrix[0, i] == matrix[2, i] &&
                matrix[0, i] != 0)
            {
                win = matrix[0, i];
                return ((0, i), (1, i), (2, i), win);
            }
        }
        // main diagonal check
        if (matrix[0, 0] == matrix[1, 1] && matrix[0, 0] == matrix[2, 2] &&
            matrix[0, 0] != 0)
        {
            win = matrix[0, 0];
            return ((0, 0), (1, 1), (2, 2), win);
        }
        // off diagonal check
        if (matrix[2, 0] == matrix[1, 1] && matrix[2, 0] == matrix[0, 2] &&
            matrix[2, 0] != 0)
        {
            win = matrix[2, 0];
            return ((2, 0), (1, 1), (0, 2), win);
        }
        
        return ((0, 0), (0, 0), (0, 0), win);
    }
   
    
    // Visualisation of field
    static string PrintMatrix(int[,] matrix)
    {
        string result = "";
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // ∙  ✕|✗ ⊠|⊞   ◯ ● 
                int element = matrix[i, j];
                string interpreter = "";
                if (element == 0)
                    interpreter = "\u2219";
                else if (element == 1)
                    interpreter = "\u2715";
                else if (element == 2)
                    interpreter = "\u25ef";
                else if (element == 3)
                    interpreter = "\u22a0";
                else if (element == 4)
                    interpreter = "\u25cf";
                
                result += interpreter + " ";
            }
            result += "\n";
        }

        result = result.TrimEnd();
        return result;
    }
}