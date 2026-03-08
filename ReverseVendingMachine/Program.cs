using ReverseVendingMachine.Enums;
using ReverseVendingMachine.Factories;
using ReverseVendingMachine.Interfaces;
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
                var command = Console.ReadKey();

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
                            machine.PrintReceipt();
                            machine.EndCurrentSession();
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
                    default:
                        await Task.Run(() => scanner.ScanItemAsync(ItemType.Unknown));
                        break;
                }
            }
        }

        private static (SimulatedScanner scanner, DepositMachine machine) InitiateMachine()
        {
            var scanner = new SimulatedScanner();
            var printer = new ReceiptPrinterConsole();
            var itemFactory = new ScannedItemFactory();
            var screen = new ConsoleScreen();

            var machine = new DepositMachine(scanner, printer, itemFactory, screen);
            return (scanner, machine);
        }
    }
}

