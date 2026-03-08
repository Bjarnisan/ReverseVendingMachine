using ReverseVendingMachine.Enums;

namespace ReverseVendingMachine.interfaces
{
    internal interface IScannedItemFactory
    {
        ScannedItem CreateScannedItem(ItemType type);
    }
}
