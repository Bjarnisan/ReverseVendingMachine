using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ReverseVendingMachine
{
    internal class ReverseVendingMachine : IDisposable
    {
        private bool disposed = false;
        private SimulatedScanner scanner;
        private ReceiptPrinterConsole receiptPrinter;
        private DepositingSession? depositingSession;
        private ScannedItemFactory scannedItemFactory;
        private ConsoleScreen screen;

        public bool SessionInProgress => depositingSession is not null;

        internal ReverseVendingMachine(SimulatedScanner scanner, ReceiptPrinterConsole receiptPrinter, ScannedItemFactory scannedItemFactory, ConsoleScreen screen)
        {
            this.scanner = scanner;
            this.receiptPrinter = receiptPrinter;
            this.scannedItemFactory = scannedItemFactory;
            this.screen = screen;

            SubscribeToScanner(scanner);
            screen.ShowWelcomeMessage();
        }

        private void OnScanner_ScanFailed(object? sender, FailedScanReason failedReason)
        {
            switch (failedReason)
            {
                case FailedScanReason.ScannerBusy:
                    screen.ScannerBusyWarning();
                    break;
                case FailedScanReason.Unknown:
                    screen.ContactSupportMessage();
                    break;
                default:
                    break;
            }
        }

        public void PrintReceipt()
        {
            if (depositingSession is null)
            {
                return;
            }

            receiptPrinter.PrintReceipt(depositingSession);
        }

        public void EndCurrentSession()
        {
            depositingSession = null;
            screen.ShowWelcomeMessage();
        }

        private void OnScanner_ItemScanned(object? sender, ItemType itemType)
        {
            depositingSession ??= new();

            var scannedItem = scannedItemFactory.CreateScannedItem(itemType);
            depositingSession.AddScannedItem(scannedItem);
            screen.UpdateRecyclingState(depositingSession);
        }

        private void OnScanner_ScannerStateChanged(object? sender, ScannerState scannerState)
        {
            switch (scannerState)
            {
                case ScannerState.ReadyToScan:
                    screen.ShowReadyToScan();
                    break;
                case ScannerState.ScanningItem:
                    screen.ShowScanningItem();
                    break;
                default:
                    break;
            }
        }


        private void SubscribeToScanner(SimulatedScanner scanner)
        {
            scanner.ScannerStateChanged += OnScanner_ScannerStateChanged;
            scanner.ItemScanned += OnScanner_ItemScanned;
            scanner.ScanFailed += OnScanner_ScanFailed;
        }

        private void UnsubscribeToScanner(SimulatedScanner scanner)
        {
            scanner.ScannerStateChanged -= OnScanner_ScannerStateChanged;
            scanner.ItemScanned -= OnScanner_ItemScanned;
            scanner.ScanFailed -= OnScanner_ScanFailed;
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            UnsubscribeToScanner(scanner);
            scanner.Dispose();
            receiptPrinter.Dispose();
            screen.Dispose();

            disposed = true;
        }
    }
}
