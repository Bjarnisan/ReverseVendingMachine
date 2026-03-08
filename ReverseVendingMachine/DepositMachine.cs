using ReverseVendingMachine.Enums;
using ReverseVendingMachine.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ReverseVendingMachine
{
    internal class DepositMachine : IDisposable
    {
        private bool disposed = false;
        private IScanner scanner;
        private IReceiptPrinter receiptPrinter;
        private DepositingSession? depositingSession;
        private IScannedItemFactory scannedItemFactory;
        private IScreen screen;

        internal bool SessionInProgress => depositingSession is not null;

        internal DepositMachine(IScanner scanner, IReceiptPrinter receiptPrinter, IScannedItemFactory scannedItemFactory, IScreen screen)
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

        internal void PrintReceipt()
        {
            if (depositingSession is null)
            {
                return;
            }

            receiptPrinter.PrintReceipt(depositingSession);
        }

        internal void EndCurrentSession()
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


        private void SubscribeToScanner(IScanner scanner)
        {
            scanner.ScannerStateChanged += OnScanner_ScannerStateChanged;
            scanner.ItemScanned += OnScanner_ItemScanned;
            scanner.ScanFailed += OnScanner_ScanFailed;
        }

        private void UnsubscribeToScanner(IScanner scanner)
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
