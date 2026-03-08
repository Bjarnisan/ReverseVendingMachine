namespace ReverseVendingMachine.Interfaces
{
    internal interface IReceiptPrinter : IDisposable
    {
        void PrintReceipt(DepositingSession depositingSession);
    }
}
