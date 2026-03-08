namespace ReverseVendingMachine.interfaces
{
    internal interface IScreen : IDisposable
    {
        void ShowWelcomeMessage();
        void ShowScanningItem();
        void ShowReadyToScan();
        void UpdateRecyclingState(DepositingSession session);
        void ScannerBusyWarning();
        void ContactSupportMessage();
    }
}
