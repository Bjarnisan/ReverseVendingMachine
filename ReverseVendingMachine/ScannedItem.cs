using ReverseVendingMachine.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseVendingMachine
{
    public record ScannedItem(ItemType ItemType, decimal Value);
}
