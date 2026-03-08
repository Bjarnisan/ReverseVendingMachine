using ReverseVendingMachine.Models;

namespace ReverseVendingMachine.Interfaces
{
    internal interface IDepositMachineLogger : IDisposable
    {
        Task LogItemDepositedAsync(ScannedItem item);
        Task LogReceiptPrintedAsync(DepositingSession depositingSession);
    }
}
