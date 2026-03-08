namespace ReverseVendingMachine
{
    public interface IReceiptPrinter : IDisposable
    {
        public void PrintReceipt(DepositingSession depositingSession);
    }
}
