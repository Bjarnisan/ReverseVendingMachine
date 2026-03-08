using ReverseVendingMachine.Interfaces;
using ReverseVendingMachine.Models;

namespace ReverseVendingMachine.Logging
{
    internal class ConsoleDepositMachineLogger : IDepositMachineLogger
    {

        public async Task LogItemDepositedAsync(ScannedItem item)
        {
            // delay to simulate server delay
            await Task.Delay(Random.Shared.Next(30, 60));
            Console.WriteLine(
                $"Logged to server:\n" +
                $"Item: {item.ItemType} scanned at {item.time:O}");
        }

        public async Task LogReceiptPrintedAsync(DepositingSession depositingSession)
        {
            // delay to simulate server delay
            await Task.Delay(Random.Shared.Next(30, 60));
            Console.WriteLine(
                $"Server log:\n" +
                $"Recept given for: {depositingSession.NumberOfCans + depositingSession.NumberOfBottles} items" +
                    $" valued at NOK {depositingSession.TotalValue}");
        }

        public void Dispose()
        {
            // nothing to dispose
        }
    }
}
