using ReverseVendingMachine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseVendingMachine.Interfaces
{
    internal interface IDepositMachineLogger : IDisposable
    {
        Task LogItemDepositedAsync(ScannedItem item);
        Task LogReceptPrintedAsync(DepositingSession depositingSession);
    }
}
