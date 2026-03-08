using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseVendingMachine
{
    enum ItemType
    {
        Can,
        Bottle,
        InvalidItem,
        Unknown
    }

    internal record ScannedItem(ItemType ItemType, decimal Value);
}
