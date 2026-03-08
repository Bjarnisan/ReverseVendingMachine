using ReverseVendingMachine.Enums;
using ReverseVendingMachine.Interfaces;

namespace ReverseVendingMachine.Scanner
{
    internal class SimulatedScanner : IScanner
    {
        private readonly SemaphoreSlim _scanningStateLock = new(1, 1);
        private ScannerState scannerState_backing = ScannerState.ReadyToScan;

        public event EventHandler<ScannerState>? ScannerStateChanged;
        public event EventHandler<ItemType>? ItemScanned;
        public event EventHandler<FailedScanReason>? ScanFailed;

        public ScannerState ScanningState
        {
            get => scannerState_backing;
            private set
            {
                scannerState_backing = value;
                ScannerStateChanged?.Invoke(this, scannerState_backing);
            }
        }

        internal async Task ScanItemAsync(ItemType itemType)
        {
            if (ScanningState == ScannerState.InErrorState)
            {
                return;
            }

            if (!await _scanningStateLock.WaitAsync(0))
            {
                ScanFailed?.Invoke(this, FailedScanReason.ScannerBusy);
                return;
            }

            try
            {
                if (ScanningState == ScannerState.InErrorState)
                {
                    return;
                }

                ScanningState = ScannerState.ScanningItem;

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
                        break;
                    case ItemType.Unknown:
                        break;
                    default:
                        // shouldn't be able to get here
                        ScanFailed?.Invoke(this, FailedScanReason.Unknown);
                        ScanningState = ScannerState.InErrorState;
                        return;
                }

                ScanningState = ScannerState.ReadyToScan;
                ItemScanned?.Invoke(this, itemType);
            }
            finally
            {
                _scanningStateLock.Release();
            }
        }

        public void Dispose()
        {
            _scanningStateLock.Dispose();
        }
    }
}
