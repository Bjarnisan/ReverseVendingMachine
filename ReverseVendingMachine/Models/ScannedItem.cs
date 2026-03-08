using ReverseVendingMachine.Enums;

namespace ReverseVendingMachine.Models
{
    internal record ScannedItem(ItemType ItemType, DateTime Time, decimal Value);
}
