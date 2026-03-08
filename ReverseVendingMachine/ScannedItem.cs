using ReverseVendingMachine.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseVendingMachine
{
    internal record ScannedItem(ItemType ItemType, decimal Value);
}
