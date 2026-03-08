using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseVendingMachine
{
    internal class DepositingSession
    {
        private List<ScannedItem> items = new();

        internal IReadOnlyCollection<ScannedItem> ScannedItems => items;

        public decimal TotalValue => ScannedItems.Sum(item => item.Value);

        public int NumberOfCans => ScannedItems.Count(item => item.ItemType == ItemType.Can);

        public int NumberOfBottles => ScannedItems.Count(item => item.ItemType == ItemType.Bottle);

        internal void AddScannedItem(ScannedItem scannedItem)
        {
            items.Add(scannedItem);
        }
    }
}
