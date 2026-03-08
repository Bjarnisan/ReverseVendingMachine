using ReverseVendingMachine.Interfaces;
using ReverseVendingMachine.Models;

namespace ReverseVendingMachine.UI
{
    internal class ReceiptPrinterConsole : IReceiptPrinter
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
            GC.SuppressFinalize(this);
        }
    }
}
