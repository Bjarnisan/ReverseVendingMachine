namespace ReverseVendingMachine
{
    public interface IScreen : IDisposable
    {
        public void ShowWelcomeMessage();
        public void ShowScanningItem();
        public void ShowReadyToScan();
        public void UpdateRecyclingState(DepositingSession session);
        public void ScannerBusyWarning();
        public void ContactSupportMessage();
    }
}
