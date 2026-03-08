using ReverseVendingMachine.Enums;
using ReverseVendingMachine.Factories;
using ReverseVendingMachine.Interfaces;
using ReverseVendingMachine.Logging;
using ReverseVendingMachine.Scanner;
using ReverseVendingMachine.UI;

namespace ReverseVendingMachine
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var (scanner, machine) = InitiateMachine();

            while (true)
            {
                var command = Console.ReadKey(true);

                switch (command.Key)
                {
                    case ConsoleKey.B:
                        _ = Task.Run(() => scanner.ScanItemAsync(ItemType.Bottle))
                            .ContinueWith(t => Console.WriteLine(t.Exception), TaskContinuationOptions.OnlyOnFaulted);
                        break;
                    case ConsoleKey.C:
                        _ = Task.Run(() => scanner.ScanItemAsync(ItemType.Can))
                            .ContinueWith(t => Console.WriteLine(t.Exception), TaskContinuationOptions.OnlyOnFaulted);
                        break;
                    case ConsoleKey.P:
                        if (machine.SessionInProgress)
                        {
                            machine.EndSessionAndPrintReceipt();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("No session to print");
                            Console.WriteLine();
                        }
                        break;
                    case ConsoleKey.R:
                        machine.Dispose();
                        (scanner, machine) = InitiateMachine();
                        break;
                    case ConsoleKey.Q:
                        return;
                    case ConsoleKey.I:
                        _ = Task.Run(() => scanner.ScanItemAsync(ItemType.InvalidItem));
                        break;
                    default:
                        break;
                }
            }
        }

        private static (IScannerHardware scanner, DepositMachine machine) InitiateMachine()
        {
            var scanner = new SimulatedScanner();
            var printer = new ReceiptPrinterConsole();
            var itemFactory = new ScannedItemFactory();
            var screen = new ConsoleScreen();
            var logger = new ConsoleDepositMachineLogger();

            var machine = new DepositMachine(scanner, printer, itemFactory, screen, logger);
            return (scanner, machine);
        }
    }
}

