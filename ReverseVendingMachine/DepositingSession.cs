using ReverseVendingMachine.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseVendingMachine
{
    public class DepositingSession
    {
        private List<ScannedItem> items = new();

        public IReadOnlyCollection<ScannedItem> ScannedItems => items;

        public decimal TotalValue => ScannedItems.Sum(item => item.Value);

        public int NumberOfCans => ScannedItems.Count(item => item.ItemType == ItemType.Can);

        public int NumberOfBottles => ScannedItems.Count(item => item.ItemType == ItemType.Bottle);

        public void AddScannedItem(ScannedItem scannedItem)
        {
            items.Add(scannedItem);
        }
    }
}
