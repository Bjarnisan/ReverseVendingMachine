using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseVendingMachine
{
    internal class ScannedItemFactory
    {
        internal ScannedItem CreateScannedItem(ItemType type)
        {
            decimal value = type switch
            {
                ItemType.Can => 2,
                ItemType.Bottle => 3,
                ItemType.Unknown => 0,
                _ => 0
            };
            return new ScannedItem(type, value);
        }
    }
}
