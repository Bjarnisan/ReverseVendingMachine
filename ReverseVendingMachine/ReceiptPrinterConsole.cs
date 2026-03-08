using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseVendingMachine
{
    public class ReceiptPrinterConsole : IReceiptPrinter, IDisposable
    {
        public void PrintReceipt(DepositingSession depositingSession)
        {
            string receiptString = 
                $"\n" +
                $"\n" +
                $"Receipt:\n" +
                $"\n" +
                $"Cans: {depositingSession.NumberOfCans}\n" +
                $"Bottles: {depositingSession.NumberOfBottles}\n" +
                $"Total: {depositingSession.TotalValue:F2}";

            Console.WriteLine(receiptString);
        }

        public void Dispose()
        {
            // nothing to dispose
        }
    }
}
