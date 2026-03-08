using ReverseVendingMachine.Enums;
using ReverseVendingMachine.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ReverseVendingMachine.Scanner
{
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

        internal async Task ScanItemAsync(ItemType itemType)
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
                // shouldn't be able to get here
                ScanFailed?.Invoke(this, FailedScanReason.Unknown);
                ScanningState = ScannerState.InErrorState;
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

        public void Dispose()
        {
            // nothing to dispose
        }
    }
}
