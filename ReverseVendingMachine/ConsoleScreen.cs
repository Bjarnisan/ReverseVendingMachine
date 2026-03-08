using ReverseVendingMachine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseVendingMachine
{
    internal class ConsoleScreen : IScreen, IDisposable
    {
        internal ConsoleScreen() { }

        public void ShowWelcomeMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Press \"B\" to deposit a bottle");
            Console.WriteLine("Press \"C\" to deposit a can");
            Console.WriteLine("Press \"P\" to print a receipt");
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
            Console.WriteLine("Press \"R\" to re-start machine");
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
            Console.WriteLine("Press \"P\" when finished to print a receipt");
        }

        public void ShowReadyToScan()
        {
            // not needed for this
        }

        public void ShowScanningItem()
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
