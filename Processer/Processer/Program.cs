using System.Diagnostics;
using Kimbrary.Printing;
using Newtonsoft.Json;
using Processer.Model;

if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json")))
{
    File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json"), "{\n\t\"FilesFolderPath\":\"\"\n}");
}

Settings settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json"))) ?? new();

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
            try
            {
                Print.AsGray($"\n{File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Help.txt"))}\n\n");
            }
            catch
            {
                Print.AsGray("\nCan't find the Help.txt file...\n\n");
            }
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
                        else if (commandAction == ">files")
                        {
                            PrintFiles();
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
        else if (command.StartsWith("open "))
        {
            string commandAction = command[(command.IndexOf(' ') + 1)..];

            if (commandAction.Length > 1)
            {
                switch (commandAction[0])
                {
                    case '^':
                        OpenFile(commandAction[1..], Processer.Model.SearchOption.IgnoreCase);
                        break;
                    case '*':
                        OpenFile(commandAction[1..], Processer.Model.SearchOption.Contains);
                        break;
                    default:
                        OpenFile(commandAction);
                        break;
                }
            }
            else if (commandAction.Length > 0)
            {
                OpenFile(commandAction);
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

void OpenFile(string fileName, Processer.Model.SearchOption searchOption = Processer.Model.SearchOption.FullMatch)
{
    string foundFilePath = string.Empty;

    foreach (string file in Directory.GetFiles(settings.FilesFolderPath))
    {
        if (!string.IsNullOrEmpty(foundFilePath))
        {
            break;
        }

        string currentFileName = Path.GetFileName(file);
        //string currentFileNameWithoutExtension = currentFileName[..currentFileName.LastIndexOf('.')];
        
        switch (searchOption)
        {
            case Processer.Model.SearchOption.FullMatch:
                if (currentFileName == fileName)
                {
                    foundFilePath = Path.Combine(settings.FilesFolderPath, currentFileName);
                }
                break;
            case Processer.Model.SearchOption.IgnoreCase:
                if (currentFileName.ToLower() == fileName.ToLower())
                {
                    foundFilePath = Path.Combine(settings.FilesFolderPath, currentFileName);
                }
                break;
            case Processer.Model.SearchOption.Contains:
                if (currentFileName.ToLower().Contains(fileName.ToLower()))
                {
                    foundFilePath = Path.Combine(settings.FilesFolderPath, currentFileName);
                }
                break;
        }
    }

    if (!string.IsNullOrEmpty(foundFilePath))
    {
        Process process = new()
        {
            StartInfo = new ProcessStartInfo(foundFilePath)
            {
                UseShellExecute = true
            }
        };
        process.Start();

        Print.AsWhite($"\nOpened '{Path.GetFileName(foundFilePath)}'\n\n");
    }
    else
    {
        Print.AsWhite($"\nDidn't find any file to open\n\n");
    }
}

void PrintFiles()
{
    int printedFiles = 0;

    foreach (string file in Directory.GetFiles(settings.FilesFolderPath))
    {
        string currentFileName = Path.GetFileName(file);

        if (printedFiles == 0)
        {
            Console.WriteLine("");
        }

        Print.AsWhite($"{printedFiles + 1}. ");
        Print.AsCyan($"{currentFileName}\n");

        printedFiles++;
    }
    
    if (printedFiles > 0)
    {
        Print.AsWhite($"\nPrinted {printedFiles} files in the specified files folder\n\n");
    }
    else
    {
        Print.AsWhite($"\nDidn't find any files to print in the specified files folder\n\n");
    }
}

static void PrintHeader(string version)
{
    Console.Clear();

    Print.AsCyan("PROCESSER ");
    Print.AsWhite($"({version})\n\n");
}