using ReverseVendingMachine.Enums;

namespace ReverseVendingMachine.Interfaces
{
    internal interface IScannedItemFactory
    {
        ScannedItem CreateScannedItem(ItemType type);
    }
}
