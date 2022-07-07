using System.Diagnostics;
using Kimbrary.Printing;

while (true)
{
    PrintHeader();

    Print.AsWhite("(1) Print open processes with name\n(2) Kill open processes with name\n");

    switch (Console.ReadKey().Key)
    {
        case ConsoleKey.D1:
            PrintHeader();

            Print.AsWhite("Enter process name: ");
            string printableProcessName = Read.AsDarkYellow("\n");
            int printedProcesses = 0;
            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName.ToLower().Contains(printableProcessName.ToLower()))
                {
                    Print.AsDarkYellow($"{printedProcesses + 1}. {process.ProcessName} ({process.MachineName})\n");
                    printedProcesses++;
                }
            }
            if (printedProcesses > 0)
            {
                Print.AsWhite($"\nPrinted {printedProcesses} open processes with name ");
                Print.AsDarkYellow($"{printableProcessName}\n");
            }
            else
            {
                Print.AsWhite($"Didn't find any open processes with name ");
                Print.AsDarkYellow($"{printableProcessName}\n");
            }

            Console.ReadLine();
            break;
        case ConsoleKey.D2:
            PrintHeader();

            Print.AsWhite("Enter process name: ");
            string killableProcessName = Read.AsDarkYellow("\n");
            int killedProcesses = 0;
            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName.ToLower().Contains(killableProcessName.ToLower()))
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
                Print.AsWhite($"Killed {killedProcesses} open processes with name ");
                Print.AsDarkYellow($"{killableProcessName}\n");
            }
            else
            {
                Print.AsWhite($"Didn't find any open processes with name ");
                Print.AsDarkYellow($"{killableProcessName}\n");
            }

            Console.ReadLine();
            break;
    }
}

void PrintHeader()
{
    Console.Clear();

    Print.AsWhite("≡≡≡≡≡");
    Print.AsDarkYellow(" PROCESSER ");
    Print.AsWhite("≡≡≡≡≡\n\n");
}