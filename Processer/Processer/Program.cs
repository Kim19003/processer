using System.Diagnostics;
using Kimbrary.Printing;

while (true)
{
    Start:
    PrintHeader("v1.0");

    while (true)
    {
        Print.AsWhite(">> ");
        string command = Read.AsWhite();

        if (command == "help")
        {
            Print.AsGray($"\nCommands:\n\n");
            Print.AsGray($"'help' - display the help view\n'print [process name]' - print specific processes\n" +
                $"'kill [process name]' - kill specific processes\n'clear' - clear the screen\n'exit' - exit the program\n");

            Print.AsGray($"\nSearch formatting:\n\n");
            Print.AsGray($"'^' - use before the query to ignore case sensitivity\n'*' - use before the query to do contained search (ignores case sensitivity)\n" +
                $"'>all' - do for all\n\n");
        }
        else if (command.StartsWith("print "))
        {
            string commandAction = command[(command.IndexOf(' ') + 1)..];

            if (commandAction.Length > 1)
            {
                switch (commandAction[0])
                {
                    case '^':
                        PrintProcesses(commandAction[1..], Processer.Model.SearchOption.IgnoreCase);
                        break;
                    case '*':
                        PrintProcesses(commandAction[1..], Processer.Model.SearchOption.Contains);
                        break;
                    default:
                        if (commandAction == ">all")
                        {
                            PrintProcesses(string.Empty, Processer.Model.SearchOption.All);
                        }
                        else
                        {
                            PrintProcesses(commandAction, Processer.Model.SearchOption.FullMatch);
                        }
                        break;
                }
            }
            else if (commandAction.Length > 0)
            {
                PrintProcesses(commandAction, Processer.Model.SearchOption.FullMatch);
            }
            else
            {
                Print.AsWhite($"\nCommand action can't be empty\n\n");
            }
        }
        else if (command.StartsWith("kill "))
        {
            string commandAction = command[(command.IndexOf(' ') + 1)..];

            if (commandAction.Length > 1)
            {
                switch (commandAction[0])
                {
                    case '^':
                        KillProcesses(commandAction[1..], Processer.Model.SearchOption.IgnoreCase);
                        break;
                    case '*':
                        KillProcesses(commandAction[1..], Processer.Model.SearchOption.Contains);
                        break;
                    default:
                        if (commandAction == ">all")
                        {
                            KillProcesses(string.Empty, Processer.Model.SearchOption.All);
                        }
                        else
                        {
                            KillProcesses(commandAction);
                        }
                        break;
                }
            }
            else if (commandAction.Length > 0)
            {
                KillProcesses(commandAction);
            }
            else
            {
                Print.AsWhite($"\nCommand action can't be empty\n\n");
            }
        }
        else if (command == "clear")
        {
            goto Start;
        }
        else if (command == "exit")
        {
            Environment.Exit(0);
        }
        else
        {
            Print.AsWhite($"\nCommand '{command}' not recognized\n\n");
        }
    }

    Console.ReadLine();
}

static void PrintProcesses(string processName, Processer.Model.SearchOption searchOption = Processer.Model.SearchOption.All)
{
    int printedProcesses = 0;

    foreach (Process process in Process.GetProcesses())
    {
        bool printThisProcess = false;

        switch (searchOption)
        {
            case Processer.Model.SearchOption.FullMatch:
                if (process.ProcessName == processName)
                {
                    printThisProcess = true;
                }
                break;
            case Processer.Model.SearchOption.IgnoreCase:
                if (process.ProcessName.ToLower() == processName.ToLower())
                {
                    printThisProcess = true;
                }
                break;
            case Processer.Model.SearchOption.Contains:
                if (process.ProcessName.ToLower().Contains(processName.ToLower()))
                {
                    printThisProcess = true;
                }
                break;
            case Processer.Model.SearchOption.All:
                printThisProcess = true;
                break;
        }

        if (printThisProcess)
        {
            if (printedProcesses == 0)
            {
                Console.WriteLine("");
            }

            Print.AsWhite($"{printedProcesses + 1}. ");
            Print.AsCyan($"{process.ProcessName}");
            try
            {
                Print.AsWhite($" (started: {process.StartTime})");
            }
            catch
            {
            }
            Console.WriteLine("");

            printedProcesses++;
        }
    }
    if (printedProcesses > 0)
    {
        Print.AsWhite($"\nPrinted {printedProcesses} open processes\n\n");
    }
    else
    {
        Print.AsWhite($"\nDidn't find any open processes to print\n\n");
    }
}

static void KillProcesses(string processName, Processer.Model.SearchOption searchOption = Processer.Model.SearchOption.FullMatch)
{
    int killedProcesses = 0;
    foreach (Process process in Process.GetProcesses())
    {
        bool killThisProcess = false;

        switch (searchOption)
        {
            case Processer.Model.SearchOption.FullMatch:
                if (process.ProcessName == processName)
                {
                    killThisProcess = true;
                }
                break;
            case Processer.Model.SearchOption.IgnoreCase:
                if (process.ProcessName.ToLower() == processName.ToLower())
                {
                    killThisProcess = true;
                }
                break;
            case Processer.Model.SearchOption.Contains:
                if (process.ProcessName.ToLower().Contains(processName.ToLower()))
                {
                    killThisProcess = true;
                }
                break;
            case Processer.Model.SearchOption.All:
                killThisProcess = true;
                break;
        }

        if (killThisProcess)
        {
            try
            {
                process.Kill();
                killedProcesses++;
            }
            catch
            {
            }
        }
    }
    if (killedProcesses > 0)
    {
        Print.AsWhite($"\nKilled {killedProcesses} open processes\n\n");
    }
    else
    {
        Print.AsWhite($"\nDidn't find any open processes to kill\n\n");
    }
}

static void PrintHeader(string version)
{
    Console.Clear();

    Print.AsCyan("PROCESSER ");
    Print.AsWhite($"({version})\n\n");
}