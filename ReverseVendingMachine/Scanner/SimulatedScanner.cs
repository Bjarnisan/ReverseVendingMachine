using ReverseVendingMachine.Enums;
using ReverseVendingMachine.Interfaces;

namespace ReverseVendingMachine.Scanner
{
    internal class SimulatedScanner : IScanner, IScannerHardware
    {
        private readonly Lock scanningStateLock = new();
        private ScannerState scannerState_backing = ScannerState.ReadyToScan;

        public event EventHandler<ScannerState>? ScannerStateChanged;
        public event EventHandler<ItemType>? ItemScanned;
        public event EventHandler<FailedScanReason>? ScanFailed;

        public ScannerState ScanningState
        {
            get
            {
                return scannerState_backing;
            }
            private set
            {
                lock (scanningStateLock)
                {
                    scannerState_backing = value;
                }
                ScannerStateChanged?.Invoke(this, value);
            }
        }

        public async Task ScanItemAsync(ItemType itemType)
        {
            lock (scanningStateLock)
            {
                if (scannerState_backing == ScannerState.InErrorState)
                {
                    return;
                }

                if (scannerState_backing == ScannerState.ScanningItem)
                {
                    ScanFailed?.Invoke(this, FailedScanReason.ScannerBusy);
                    return;
                }

                scannerState_backing = ScannerState.ScanningItem;
            }

            ScannerStateChanged?.Invoke(this, ScannerState.ScanningItem);

            switch (itemType)
            {
                case ItemType.Can:
                    await Task.Delay(500);
                    break;
                case ItemType.Bottle:
                    await Task.Delay(1000);
                    break;
                case ItemType.InvalidItem:
                    await Task.Delay(2000);
                    ScanFailed?.Invoke(this, FailedScanReason.InvalidItem);
                    ScanningState = ScannerState.ReadyToScan;
                    return;
                case ItemType.Unknown:
                    break;
                default:
                    // should not be reachable
                    ScanFailed?.Invoke(this, FailedScanReason.Unknown);
                    ScanningState = ScannerState.InErrorState;
                    return;
            }

            ScanningState = ScannerState.ReadyToScan;
            ItemScanned?.Invoke(this, itemType);
        }

        public void Dispose()
        {
            // nothing to dispose
        }
    }
}
