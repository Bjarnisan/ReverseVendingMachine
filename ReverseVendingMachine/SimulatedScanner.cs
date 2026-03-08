using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ReverseVendingMachine
{
    public enum FailedScanReason
    {
        ScannerBusy,
        Unknown
    }

    public enum ScannerState
    {
        ReadyToScan,
        ScanningItem,
        InErrorState
    }

    internal class SimulatedScanner : IScanner
    {
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

        public async Task ScanItemAsync(ItemType itemType)
        {
            if (ScanningState == ScannerState.InErrorState)
            {
                return;
            }
            else if (ScanningState == ScannerState.ScanningItem)
            {
                ScanFailed?.Invoke(this, FailedScanReason.ScannerBusy);
                return;
            }
            else if (ScanningState != ScannerState.ReadyToScan)
            {
                ScanFailed?.Invoke(this, FailedScanReason.Unknown);
                ScanningState = ScannerState.InErrorState;
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
                    if (Random.Shared.Next() % 10 == 0)
                    {
                        // There's a 10% chance of failing the machine when scanning non-complient item
                        ScanFailed?.Invoke(this, FailedScanReason.Unknown);
                        ScanningState = ScannerState.InErrorState;
                        return;
                    }
                    break;
                default:
                    break;
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
