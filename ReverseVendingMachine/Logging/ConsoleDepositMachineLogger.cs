using ReverseVendingMachine.Interfaces;
using ReverseVendingMachine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseVendingMachine.Logging
{
    internal class ConsoleDepositMachineLogger : IDepositMachineLogger
    {

        public async Task LogItemDepositedAsync(ScannedItem item)
        {
            // delay to simulate server delay
            await Task.Delay(30 + Random.Shared.Next() % 30);
            Console.WriteLine(
                $"Logged to server:\n" +
                $"Item: {item.ItemType} scanned at {item.time.ToString()}");
        }

        public async Task LogReceptPrintedAsync(DepositingSession depositingSession)
        {
            // delay to simulate server delay
            await Task.Delay(30 + Random.Shared.Next() % 30);

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
