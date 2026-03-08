using ReverseVendingMachine.Enums;

namespace ReverseVendingMachine.Interfaces
{
    internal interface IScanner : IDisposable
    {
        internal event EventHandler<ScannerState>? ScannerStateChanged;
        internal event EventHandler<ItemType>? ItemScanned;
        internal event EventHandler<FailedScanReason>? ScanFailed;

        internal ScannerState ScanningState { get; }
    }
}
