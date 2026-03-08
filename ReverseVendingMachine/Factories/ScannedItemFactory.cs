using ReverseVendingMachine.Enums;
using ReverseVendingMachine.Interfaces;
using ReverseVendingMachine.Models;

namespace ReverseVendingMachine.Factories
{
    internal class ScannedItemFactory : IScannedItemFactory
    {
        public ScannedItem CreateScannedItem(ItemType type)
        {
            decimal value = type switch
            {
                ItemType.Can => 2,
                ItemType.Bottle => 3,
                ItemType.Unknown => 0,
                _ => 0
            };
            return new ScannedItem(type, value);
        }
    }
}
