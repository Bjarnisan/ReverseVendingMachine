using ReverseVendingMachine.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseVendingMachine.Models
{
    internal record ScannedItem(ItemType ItemType, decimal Value);
}
