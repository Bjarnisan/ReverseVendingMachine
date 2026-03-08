using ReverseVendingMachine.Models;

namespace ReverseVendingMachine.Interfaces
{
    internal interface IScreen : IDisposable
    {
        void ShowWelcomeScreen();
        void ShowScanningItem();
        void ShowReadyToScan();
        void UpdateRecyclingState(DepositingSession session);
        void ScannerBusyWarning();
        void ContactSupportMessage();
        void InvalidItemScreen(DepositingSession? session);
    }
}
