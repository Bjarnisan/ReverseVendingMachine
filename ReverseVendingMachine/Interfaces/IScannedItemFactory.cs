using ReverseVendingMachine.Enums;
using ReverseVendingMachine.Models;

namespace ReverseVendingMachine.Interfaces
{
    internal interface IScannedItemFactory
    {
        ScannedItem CreateScannedItem(ItemType type);
    }
}
