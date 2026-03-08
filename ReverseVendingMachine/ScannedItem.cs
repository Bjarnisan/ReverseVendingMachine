using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseVendingMachine
{
    public enum ItemType
    {
        Can,
        Bottle,
        InvalidItem,
        Unknown
    }

    public record ScannedItem(ItemType ItemType, decimal Value);
}
