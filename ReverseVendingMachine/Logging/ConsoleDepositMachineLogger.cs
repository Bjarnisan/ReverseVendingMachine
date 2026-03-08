using ReverseVendingMachine.Interfaces;
using ReverseVendingMachine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseVendingMachine.Logging
{
    internal class ConsoleDepositMachineLogger : IDepositMachineLogger
    {

        public Task LogItemDeposited(ScannedItem item)
        {
            // delay to simulate server delay
            Task.Delay(30 + Random.Shared.Next() % 30);
            Console.WriteLine(
                $"Logged to server:\n" +
                $"Item: {item.ItemType} scanned at {item.time.ToString()}");
            return Task.CompletedTask;
        }

        public Task LogReceptPrinted(DepositingSession depositingSession)
        {
            // delay to simulate server delay
            Task.Delay(30 + Random.Shared.Next() % 30);

            Console.WriteLine(
                $"Server log:\n" +
                $"Recept given for: {depositingSession.NumberOfCans + depositingSession.NumberOfBottles} items" +
                    $" valued at NOK {depositingSession.TotalValue}");

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // nothing to dispose
        }
    }
}
