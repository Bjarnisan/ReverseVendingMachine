using ReverseVendingMachine.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace ReverseVendingMachine.Models
{
    internal class DepositingSession
    {
        private ConcurrentBag<ScannedItem> items = new();

        internal IReadOnlyCollection<ScannedItem> ScannedItems => items;

        internal decimal TotalValue => ScannedItems.Sum(item => item.Value);

        internal int NumberOfCans => ScannedItems.Count(item => item.ItemType == ItemType.Can);

        internal int NumberOfBottles => ScannedItems.Count(item => item.ItemType == ItemType.Bottle);

        internal void AddScannedItem(ScannedItem scannedItem)
        {
            items.Add(scannedItem);
        }
    }
}
