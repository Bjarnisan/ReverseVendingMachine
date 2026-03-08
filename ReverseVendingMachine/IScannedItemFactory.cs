namespace ReverseVendingMachine
{
    public interface IScannedItemFactory
    {
        public ScannedItem CreateScannedItem(ItemType type);
    }
}
