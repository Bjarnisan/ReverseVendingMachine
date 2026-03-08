namespace ReverseVendingMachine
{
    internal class Program
    {
        private static ReverseVendingMachine? machine;
        private static SimulatedScanner? scanner;

        static async Task Main(string[] args)
        {
            InitiateMachine();

            while (true)
            {
                if (scanner is null || machine is null)
                {
                    throw new InvalidOperationException("Machine failed to initialize.");
                }

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
                        InitiateMachine();
                        break;
                    default:
                        await Task.Run(() => scanner.ScanItemAsync(ItemType.Unknown));
                        break;
                }

            }
        }

        private static void InitiateMachine()
        {
            scanner = new SimulatedScanner();
            var printer = new ReceiptPrinterConsole();
            var itemFactory = new ScannedItemFactory();
            var screen = new ConsoleScreen();

            machine = new ReverseVendingMachine(scanner, printer, itemFactory, screen);
        }
    }
}
