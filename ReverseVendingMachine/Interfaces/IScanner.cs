using ReverseVendingMachine.Enums;

namespace ReverseVendingMachine.Interfaces
{
    internal interface IScanner : IDisposable
    {
        event EventHandler<ScannerState>? ScannerStateChanged;
        event EventHandler<ItemType>? ItemScanned;
        event EventHandler<FailedScanReason>? ScanFailed;

        ScannerState ScanningState { get; }
    }
}
