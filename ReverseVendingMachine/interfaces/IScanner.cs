namespace ReverseVendingMachine.interfaces
{
    public interface IScanner : IDisposable
    {
        public event EventHandler<ScannerState>? ScannerStateChanged;
        public event EventHandler<ItemType>? ItemScanned;
        public event EventHandler<FailedScanReason>? ScanFailed;

        public ScannerState ScanningState { get; }
    }
}
