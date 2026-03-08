using ReverseVendingMachine.enums;

namespace ReverseVendingMachine.interfaces
{
    public interface IScannedItemFactory
    {
        public ScannedItem CreateScannedItem(ItemType type);
    }
}
