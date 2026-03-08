using ReverseVendingMachine.Enums;
using ReverseVendingMachine.Interfaces;
using ReverseVendingMachine.Models;

namespace ReverseVendingMachine
{
    internal class DepositMachine : IDisposable
    {
        private bool disposed = false;
        private readonly IScanner scanner;
        private readonly IReceiptPrinter receiptPrinter;
        private readonly IScannedItemFactory scannedItemFactory;
        private readonly IScreen screen;
        private readonly IDepositMachineLogger logger;
        private DepositingSession? depositingSession;
        private Lock depositSessionLock = new();

        internal bool SessionInProgress => depositingSession is not null;

        internal DepositMachine(
            IScanner scanner,
            IReceiptPrinter receiptPrinter,
            IScannedItemFactory scannedItemFactory,
            IScreen screen,
            IDepositMachineLogger logger)
        {
            this.scanner = scanner;
            this.receiptPrinter = receiptPrinter;
            this.scannedItemFactory = scannedItemFactory;
            this.screen = screen;
            this.logger = logger;

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

        internal void EndSessionAndPrintReceipt()
        {
            DepositingSession depositSessionForPrint;

            lock (depositSessionLock)
            {
                if (depositingSession is null)
                {
                    return;
                }

                depositSessionForPrint = depositingSession;
                depositingSession = null;
            }

            receiptPrinter.PrintReceipt(depositSessionForPrint);
            logger.LogReceiptPrintedAsync(depositSessionForPrint);

            screen.ShowWelcomeMessage();
        }

        private void OnScanner_ItemScanned(object? sender, ItemType itemType)
        {
            ScannedItem scannedItem;
            lock (depositSessionLock)
            {
                depositingSession ??= new();

                scannedItem = scannedItemFactory.CreateScannedItem(itemType);

                depositingSession.AddScannedItem(scannedItem);
            }

            logger.LogItemDepositedAsync(scannedItem);
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
            logger.Dispose();

            disposed = true;
        }
    }
}
