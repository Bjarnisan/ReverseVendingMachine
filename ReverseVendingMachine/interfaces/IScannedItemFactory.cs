using ReverseVendingMachine.enums;

namespace ReverseVendingMachine.interfaces
{
    internal interface IScannedItemFactory
    {
        ScannedItem CreateScannedItem(ItemType type);
    }
}
