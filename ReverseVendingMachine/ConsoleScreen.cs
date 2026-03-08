using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseVendingMachine
{
    internal class ConsoleScreen : IDisposable
    {
        public ConsoleScreen() { }

        public void ShowWelcomeMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Press \"b\" to deposit a bottle");
            Console.WriteLine("Press \"c\" to deposit a can");
            Console.WriteLine("Press \"p\" to print a receipt");
            Console.WriteLine();
        }

        public void ScannerBusyWarning()
        {
            Console.Clear();
            Console.WriteLine("Wait till last item finished scanning!");
            Console.WriteLine();
        }

        public void ContactSupportMessage()
        {
            Console.Clear();
            Console.WriteLine("Unknown error. Contact staff");
            Console.WriteLine("Press \r\" to re-start machine");
            Console.WriteLine();
        }

        public void UpdateRecyclingState(DepositingSession session)
        {
            Console.Clear();
            Console.WriteLine(
                $"Cans: {session.NumberOfCans}\n" +
                $"Bottles: {session.NumberOfBottles}\n" +
                $"Total: {session.TotalValue:F2}");
            Console.WriteLine();
            Console.WriteLine("Press \"p\" when finished to print a receipt");
        }

        internal void ShowReadyToScan()
        {
            // not needed for this
        }

        internal void ShowScanningItem()
        {
            Console.Clear();
            Console.Write("Scanning...");
        }

        public void Dispose()
        {
            // nothing to dispose
        }
    }
}
