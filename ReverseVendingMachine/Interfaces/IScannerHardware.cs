using ReverseVendingMachine.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseVendingMachine.Interfaces
{
    internal interface IScannerHardware : IDisposable
    {
        Task ScanItemAsync(ItemType itemType);
    }
}
