namespace ReverseVendingMachine.interfaces
{
    internal interface IReceiptPrinter : IDisposable
    {
        void PrintReceipt(DepositingSession depositingSession);
    }
}
