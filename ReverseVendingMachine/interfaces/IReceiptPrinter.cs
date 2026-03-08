namespace ReverseVendingMachine.interfaces
{
    public interface IReceiptPrinter : IDisposable
    {
        public void PrintReceipt(DepositingSession depositingSession);
    }
}
